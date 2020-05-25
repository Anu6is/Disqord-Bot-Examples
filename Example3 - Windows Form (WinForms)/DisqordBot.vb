Imports System.Configuration
Imports System.Threading
Imports System.Windows.Forms
Imports Disqord.Bot
Imports Disqord.Bot.Prefixes
Imports Disqord.Events
Imports Disqord.Logging
Imports Microsoft.Extensions.DependencyInjection

Public Class DisqordBot
    Inherits DiscordBot

    Private ReadOnly Property UI As MainForm
    Private Property Watch As Stopwatch
    Private Property UpTime As Threading.Timer

    Private _guilds As Integer  'Counter for total guilds
    Private _users As Integer   'Counter for total users
    Private _messages As Integer 'Counter for total messages

    ''' <summary>
    '''     Constructor: Creates a new instance of <see cref="DisqordBot"/> which inherits from <see cref="DiscordBot"/>.
    ''' </summary>
    Public Sub New(form As Form)
        MyBase.New(TokenType.Bot, DirectCast(form, MainForm).txtInput.GetProperty(Function(box) box.Text),
                   New DefaultPrefixProvider().AddMentionPrefix().AddPrefix("!"),
                   New DiscordBotConfiguration() With {
                        .Activity = New LocalActivity("Minesweeper", ActivityType.Playing),
                        .ProviderFactory = Function(bot) New ServiceCollection().AddSingleton(bot).BuildServiceProvider()
                   })

        UI = DirectCast(form, MainForm)
        AddModules(GetType(DisqordBot).Assembly)

        AddHandler Logger.MessageLogged, AddressOf OnMessageLogged
        AddHandler GuildAvailable, AddressOf OnGuildAvailable
        AddHandler GuildUnavailable, AddressOf OnGuildUnavailable
        AddHandler JoinedGuild, AddressOf OnJoinedGuild
        AddHandler LeftGuild, AddressOf OnLeftGuild
        AddHandler MemberJoined, AddressOf OnMemberJoined
        AddHandler MemberLeft, AddressOf OnMemberLeft
        AddHandler MessageReceived, AddressOf OnMessageReceived
        AddHandler Ready, AddressOf OnReady
    End Sub

    ''' <summary>
    '''     Start the bot. Connect and login to Discord
    ''' </summary>
    Public Async Function StartAsync() As Task
        Watch = Stopwatch.StartNew()
        UpTime = New Threading.Timer(Sub() UI.lblUptime.SetProperty(Function(time) time.Text, $"Uptime: {Watch.Elapsed:d\d\:hh\h\:mm\m\:ss\s}"),
                                     Nothing,
                                     TimeSpan.FromSeconds(1),
                                     TimeSpan.FromSeconds(1))

        UI.lblGuildCount.SetProperty(Function(label) label.Text, "0")
        UI.lblUserCount.SetProperty(Function(label) label.Text, "0")
        UI.lblMessageCount.SetProperty(Function(label) label.Text, "0")

        UI.ControlCenter.SetProperty(Function(c) c.SelectedIndex, 1)
        UI.lblStatus.SetProperty(Function(s) s.Text, "Connecting")
        UI.lblStatus.SetProperty(Function(s) s.ForeColor, Drawing.Color.Orange)

        Await RunAsync(New CancellationTokenSource().Token)
    End Function

    ''' <summary>
    '''     Disconnect from Discord
    ''' </summary>
    Public Async Function LogoutAsync() As Task
        RemoveHandler Logger.MessageLogged, AddressOf OnMessageLogged
        Await StopAsync()
    End Function

    ''' <summary>
    '''     Log any messages/logs received from <see cref="DiscordClientBase"/> to the Console tab
    ''' </summary>
    Private Sub OnMessageLogged(sender As Object, e As MessageLoggedEventArgs)
        Dim index = UI.InvokeFunc(Of Integer)(Function() UI.lstConsole.Items.Add(e.ToString))

        If UI.lstConsole.GetProperty(Function(lstBox) lstBox.SelectedIndex) < 0 Then 'If no lines are selected
            UI.lstConsole.SetProperty(Function(lstBox) lstBox.TopIndex, index) 'ensure the newly added line is in view (auto-scroll)
        End If
    End Sub

    ''' <summary>
    '''     Executed when the bot has successfully connected and downloaded all guild data
    ''' </summary>
    Private Function OnReady(e As ReadyEventArgs) As Task
        UI.InvokeFunc(Of Integer)(Function() UI.lstConsole.Items.Add($"Ready after {Watch.Elapsed:mm\m\:ss\.fff\s}"))
        SaveToken()

        'Switch over from the Console tab to the Discord tab
        UI.ControlCenter.SetProperty(Function(tabControl) tabControl.SelectedIndex, 0)

        'Set the status label to connected
        UI.lblStatus.SetProperty(Function(status) status.Text, "Connected")
        UI.lblStatus.SetProperty(Function(status) status.ForeColor, Drawing.Color.Green)

        'Set user count
        _users = Users.Count
        UI.lblUserCount.SetProperty(Function(label) label.Text, _users.ToString)

        'Set form name (bot name) and bot profile picture
        UI.SetProperty(Function(form) form.Text, CurrentUser.Name)
        UI.InvokeAction(Sub() UI.picProfile.Load(CurrentUser.GetAvatarUrl))

        'Enable input controls (repurpose token entry controls)
        UI.lblInput.SetProperty(Function(label) label.Text, "Chat")
        UI.btnSubmit.SetProperty(Function(button) button.Text, "Send")
        UI.btnSubmit.SetProperty(Function(button) button.Enabled, True)
        UI.txtInput.SetProperty(Function(txtbox) txtbox.Enabled, True)
        UI.txtInput.SetProperty(Function(txtbox) txtbox.Multiline, True)
        UI.txtInput.SetProperty(Function(txtbox) txtbox.Height, 24)
        UI.InvokeAction(Sub() UI.txtInput.Clear())

        'Load guilds list box
        UI.InvokeAction(Sub() UI.lstGuilds.Items.AddRange(Guilds.Values.ToArray()))
        UI.lstGuilds.SetProperty(Function(list) list.SelectedIndex, 0)

        RemoveHandler Ready, AddressOf OnReady
        Return Task.CompletedTask
    End Function

    ''' <summary>
    '''     Store the bot token in application settings
    ''' </summary>
    Private Sub SaveToken()
        Dim configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        Dim settings = configFile.AppSettings.Settings
        Dim token = UI.txtInput.GetProperty(Function(box) box.Text)

        If settings("DisqordBotToken") Is Nothing Then 'If no token exists, save it
            settings.Add("DisqordBotToken", token)
            configFile.Save(ConfigurationSaveMode.Modified)
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name)
        ElseIf Not settings("DisqordBotToken").Value.Equals(token) Then 'If the token is different, save new token
            settings("DisqordBotToken").Value = token
            configFile.Save(ConfigurationSaveMode.Modified)
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name)
        End If
    End Sub

    ''' <summary>
    '''     Adds messages received in the <see cref="MainForm.SelectedChannel"/> to the chat <see cref="ListBox"/>.
    '''     Also keeps track of the number of messages received, excluding messages from the bot (includes messages from other bots)
    ''' </summary>
    Private Function OnMessageReceived(e As MessageReceivedEventArgs) As Task
        If TypeOf e.Message IsNot CachedUserMessage Then Return Task.CompletedTask

        If e.Message.Channel.Id.Equals(UI.SelectedChannel.Id) Then
            Dim index = UI.InvokeFunc(Of Integer)(Sub() UI.lstChat.Items.Add(e.Message))

            If UI.lstChat.GetProperty(Function(lstBox) lstBox.SelectedIndex) < 0 Then
                UI.lstChat.SetProperty(Function(lstBox) lstBox.TopIndex, index)
            End If
        End If

        If e.Message.Author.Id.Equals(CurrentUser.Id) Then Return Task.CompletedTask

        _messages += 1
        UI.lblMessageCount.SetProperty(Function(label) label.Text, _messages.ToString)
        Return Task.CompletedTask
    End Function

    Private Function OnGuildAvailable(e As GuildAvailableEventArgs) As Task
        _guilds += 1
        UI.lblGuildCount.SetProperty(Function(label) label.Text, _guilds.ToString)
        Return Task.CompletedTask
    End Function

    Private Function OnGuildUnavailable(e As GuildUnavailableEventArgs) As Task
        _guilds -= 1
        UI.lblGuildCount.SetProperty(Function(label) label.Text, _guilds.ToString)
        Return Task.CompletedTask
    End Function

    Private Function OnJoinedGuild(e As JoinedGuildEventArgs) As Task
        _guilds += 1
        UI.lblGuildCount.SetProperty(Function(label) label.Text, _guilds.ToString)
        Return Task.CompletedTask
    End Function

    Private Function OnLeftGuild(e As LeftGuildEventArgs) As Task
        _guilds -= 1
        UI.lblGuildCount.SetProperty(Function(label) label.Text, _guilds.ToString)
        Return Task.CompletedTask
    End Function

    Private Function OnMemberJoined(e As MemberJoinedEventArgs) As Task
        _users += 1
        UI.lblUserCount.SetProperty(Function(label) label.Text, _users.ToString)
        Return Task.CompletedTask
    End Function

    Private Function OnMemberLeft(e As MemberLeftEventArgs) As Task
        _users -= 1
        UI.lblUserCount.SetProperty(Function(label) label.Text, _users.ToString)
        Return Task.CompletedTask
    End Function

    Public Sub StopTimer()
        UpTime.Change(Timeout.Infinite, Timeout.Infinite)
    End Sub
End Class