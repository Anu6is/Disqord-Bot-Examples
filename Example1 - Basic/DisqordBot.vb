Imports Disqord.Bot
Imports Disqord.Bot.Prefixes
Imports Disqord.Logging
Imports Microsoft.Extensions.DependencyInjection

Public Class DisqordBot
    Inherits DiscordBot

    Public Sub New(token As String)
        MyBase.New(TokenType.Bot, token,
                   New DefaultPrefixProvider().AddMentionPrefix().AddPrefix("!"),
                   New DiscordBotConfiguration() With {
                        .Activity = New LocalActivity("!help", ActivityType.Playing),
                        .ProviderFactory = Function(bot) New ServiceCollection().AddSingleton(bot).BuildServiceProvider()
                   })
        AddHandler Logger.MessageLogged, AddressOf OnMessageLogged
        AddModules(GetType(DisqordBot).Assembly)
    End Sub

    Private Sub OnMessageLogged(sender As Object, e As MessageLoggedEventArgs)
        If e.Severity < LogMessageSeverity.Information Then Return
        Console.WriteLine(e.ToString)
    End Sub
End Class
