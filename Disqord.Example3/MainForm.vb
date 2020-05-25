Imports System.ComponentModel
Imports System.Configuration
Imports System.Windows.Forms
Imports Disqord.Rest

Public Class MainForm
    Private Property Bot As DisqordBot
    ''' <summary>
    '''     True if the user selects 'Exit' from <see cref="ctxIconMenu"/>
    ''' </summary>
    Private Property ShutdownRequested As Boolean
    ''' <summary>
    '''     The channel used to filter incoming messages and send outgoing messages
    ''' </summary>
    ''' <returns></returns>
    Public Property SelectedChannel As CachedTextChannel

    Public Sub New()
        InitializeComponent()
    End Sub

    ''' <summary>
    '''     Load token if stored in settings
    ''' </summary>
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        Dim settings = configFile.AppSettings.Settings

        If settings("DisqordBotToken") IsNot Nothing Then txtInput.Text = ConfigurationManager.AppSettings.Item("DisqordBotToken")
    End Sub

    ''' <summary>
    '''     Bring the application to the forefront if the tray icon is clicked
    ''' </summary>
    Private Sub NotifyIcon_Click(sender As Object, e As EventArgs) Handles NotifyIcon.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then Return

        Show()
        Activate()
    End Sub

    ''' <summary>
    '''     Hide the application if the tray icon is double clicked
    ''' </summary>
    Private Sub NotifyIcon_DoubleClick(sender As Object, e As EventArgs) Handles NotifyIcon.DoubleClick
        Hide()
    End Sub

    ''' <summary>
    '''     Log out of the current bot account without exiting the application
    ''' </summary>
    Private Async Sub ToolStripMenuLogout_Click(sender As Object, e As EventArgs) Handles tsmLogout.Click
        Show()
        Activate()
        Bot.StopTimer()
        Await Bot.LogoutAsync()
        ResetForm()
        Bot = Nothing
    End Sub

    ''' <summary>
    '''     Log out of the current bot accaount and exit the application
    ''' </summary>
    Private Async Sub ToolStripMenuExit_Click(sender As Object, e As EventArgs) Handles tsmExit.Click
        Hide()
        ShutdownRequested = True
        If Bot IsNot Nothing Then Await Bot.LogoutAsync()
        Await Task.Delay(2000)
        NotifyIcon.Dispose()
        Close()
    End Sub

    ''' <summary>
    '''     Prevent the form from closing unless "Exit" is selected via the tray icon
    ''' </summary>
    Private Sub MainForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Not ShutdownRequested Then
            e.Cancel = True
            Hide()
        End If
    End Sub

    ''' <summary>
    '''     Reload channel list when the selected guild changes
    ''' </summary>
    Private Sub LstGuilds_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstGuilds.SelectedIndexChanged
        If DirectCast(sender, ListBox).SelectedIndex = -1 Then Return

        Dim guild As CachedGuild = DirectCast(lstGuilds.SelectedItem, CachedGuild)

        lstChannels.ClearSelected()
        lstChannels.Items.Clear()
        lstChat.ClearSelected()
        lstChat.Items.Clear()

        lstChannels.Items.AddRange(guild.TextChannels.Values.OrderBy(Function(c) c.Position).ToArray)
        lstChannels.SelectedIndex = 0
    End Sub

    ''' <summary>
    '''     Clear the chat box when the selected channel changes
    ''' </summary>
    Private Sub LstChannels_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstChannels.SelectedIndexChanged
        If DirectCast(sender, ListBox).SelectedIndex = -1 Then SelectedChannel = Nothing : Return

        SelectedChannel = DirectCast(lstChannels.SelectedItem, CachedTextChannel)

        lstChat.ClearSelected()
        lstChat.Items.Clear()
    End Sub

    ''' <summary>
    '''     Add user mention to input box when a message is double clicked
    ''' </summary>
    Private Sub LstChat_DoubleClick(sender As Object, e As EventArgs) Handles lstChat.DoubleClick
        If lstChat.SelectedIndex = -1 Then Return

        Dim message As CachedUserMessage = DirectCast(DirectCast(sender, ListBox).SelectedItem, CachedUserMessage)

        txtInput.AppendText(message.Author.Mention)

        lstChat.Items.Clear()
    End Sub

    ''' <summary>
    '''     Respond to button click based on context
    ''' </summary>
    Private Async Sub BtnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Select Case DirectCast(sender, Button).Text
            Case "Start Bot" : Await StartBotAsync()
            Case "Send" : Await SendMessageAsync()
        End Select
    End Sub

    ''' <summary>
    '''     Login and connect to Discord
    ''' </summary>
    Private Async Function StartBotAsync() As Task
        If String.IsNullOrWhiteSpace(txtInput.Text) Then
            MessageBox.Show("Please enter the Bot Token", "Token Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        btnSubmit.Enabled = False
        txtInput.Enabled = False

        Bot = New DisqordBot(Me)

        Try
            Await Bot.StartAsync()
        Catch ex As DiscordHttpException
            MessageBox.Show("Invalid Bot Token", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            lblStatus.Text = "Disconnected"
            lblStatus.ForeColor = Drawing.Color.Orange
            btnSubmit.Enabled = True
            txtInput.Enabled = True
            txtInput.Clear()
            Return
        End Try
    End Function

    ''' <summary>
    '''     Reset all control values to original form load state
    ''' </summary>
    Private Sub ResetForm()
        lblInput.Text = "Token:"
        btnSubmit.Text = "Start Bot"
        lblStatus.Text = "Disconnected"
        Text = "Disqord Winforms Example"
        txtInput.Multiline = False
        txtInput.UseSystemPasswordChar = True
        lblStatus.ForeColor = Drawing.Color.Black
        picProfile.Image = picProfile.InitialImage
        lblGuildCount.Text = "0"
        lblUserCount.Text = "0"
        lblMessageCount.Text = "0"
        lstConsole.Items.Clear()
        lstChat.Items.Clear()
        lstChannels.Items.Clear()
        lstGuilds.Items.Clear()
    End Sub

    ''' <summary>
    '''     Send a message to the <see cref="SelectedChannel"/>
    ''' </summary>
    Private Async Function SendMessageAsync() As Task
        If SelectedChannel Is Nothing Then Return
        If String.IsNullOrWhiteSpace(txtInput.Text) Then Return

        Await SendMessageAsync(txtInput.Text).ContinueWith(Sub() InvokeAction(Sub() txtInput.Clear()))
    End Function

    ''' <summary>
    '''     Send a message to the <see cref="SelectedChannel"/>
    ''' </summary>
    Private Function SendMessageAsync(text As String) As Task
        If SelectedChannel Is Nothing Then Return Task.CompletedTask Else Return SelectedChannel.SendMessageAsync(text)
    End Function

    ''' <summary>
    '''     Submit the input box text on 'Enter' or add a new line on 'Shift & Enter'
    ''' </summary>
    Private Async Sub TxtInput_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtInput.KeyPress
        If e.KeyChar.Equals(ChrW(Keys.Return)) Then
            e.Handled = True

            If btnSubmit.Text.Equals("Send") Then
                If ModifierKeys.HasFlag(Keys.Shift) Then txtInput.AppendText(Environment.NewLine) Else Await SendMessageAsync()
            Else
                Await StartBotAsync()
            End If
        End If
    End Sub

    ''' <summary>
    '''     Stop the console auto-scroll by clearing any selected items
    ''' </summary>
    Private Sub LstConsole_DoubleClick(sender As Object, e As EventArgs) Handles lstConsole.DoubleClick
        lstConsole.ClearSelected()
    End Sub
End Class