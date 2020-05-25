Imports Disqord.Bot
Imports Disqord.Logging
Imports Qmmands

Public Class LogService

    Public Sub New(bot As DiscordBotBase)
        AddHandler bot.CommandExecuted, AddressOf OnCommandExecuted
        AddHandler bot.CommandExecutionFailed, AddressOf OnComandExecutionFailed
    End Sub

    Private Function OnCommandExecuted(e As CommandExecutedEventArgs) As Task
        Dim context = DirectCast(e.Context, DiscordCommandContext)
        Console.WriteLine($"{context.User} executed {e.Context.Command.FullAliases.First} in {If(context.Guild?.Name, context.Channel.Name)} ({If(context.Guild?.Id, context.Channel.Id)})")
        Return Task.CompletedTask
    End Function

    Private Function OnComandExecutionFailed(e As CommandExecutionFailedEventArgs) As Task
        Console.WriteLine(e.Result.Exception.ToString)
        Return Task.CompletedTask
    End Function

    Public Sub OnMessgeLogged(sender As Object, e As MessageLoggedEventArgs)
        If e.Severity < LogMessageSeverity.Information Then Return
        Console.WriteLine(e.ToString)
    End Sub
End Class
