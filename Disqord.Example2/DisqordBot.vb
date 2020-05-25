Imports Disqord.Bot
Imports Disqord.Bot.Prefixes
Imports Disqord.Extensions.Interactivity
Imports Microsoft.Extensions.DependencyInjection

Public Class DisqordBot
    Inherits DiscordBot

    Public Sub New(token As String)
        MyBase.New(TokenType.Bot, token,
                   New DefaultPrefixProvider().AddMentionPrefix().AddPrefix("!"),
                   New DiscordBotConfiguration() With {
                        .Activity = New LocalActivity("for !help", ActivityType.Watching),
                        .ProviderFactory = Function(bot) New ServiceCollection() _
                                                                .AddSingleton(bot) _
                                                                .AddSingleton(Of LogService) _
                                                                .AddSingleton(New Random()) _
                                                                .BuildServiceProvider()
                   })
        AddHandler Logger.MessageLogged, AddressOf GetService(Of LogService).OnMessgeLogged
        AddExtensionAsync(New InteractivityExtension()).GetAwaiter.GetResult()
        AddModules(GetType(DisqordBot).Assembly)
    End Sub
End Class
