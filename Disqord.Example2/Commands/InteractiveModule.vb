Imports Disqord.Extensions.Interactivity
Imports Disqord.Bot
Imports Qmmands
Imports Disqord.Extensions.Interactivity.Menus
Imports Disqord.Extensions.Interactivity.Menus.Paged

<Name("Interactive")>
Public Class InteractiveModule
    Inherits DiscordModuleBase(Of DiscordCommandContext)

    Public Property Commands As CommandService

    Public Property Rng As Random

    <Command("guess")>
    <Description("Guess if the next number is higher or lower")>
    <RunMode(RunMode.Parallel)>
    Public Async Function HigherOrLower() As Task
        Dim number = Rng.Next(1, 8)
        Dim emojiNumber = New LocalEmoji(ChrW(&H30 + number) & ChrW(&H20E3)).Name
        Dim message = Await ReplyAsync("Is the next number **higher** or **lower**?", embed:=New LocalEmbedBuilder() With {.Description = emojiNumber}.Build)
        Dim response = Await Context.Channel.WaitForMessageAsync(Function(x) x.Message.Author.Id.Equals(Context.User.Id), TimeSpan.FromSeconds(30))

        If response.Message Is Nothing Then
            Await ReplyAsync("¯\_(ツ)_/¯")
        Else
            Dim answer = "same"
            Dim emote As New Dictionary(Of String, String) From {{"higher", "⬆️"}, {"lower", "⬇️"}, {"same", "↔️"}}
            Dim nextNumber = Rng.Next(10)
            Dim emojiNextNumber = New LocalEmoji(ChrW(&H30 + nextNumber) & ChrW(&H20E3)).Name

            If nextNumber > number Then answer = "higher"
            If nextNumber < number Then answer = "lower"

            Dim builder As New LocalEmbedBuilder() With {
                .Description = $"{emojiNumber} {emote(answer)} {emojiNextNumber}",
                .Footer = New LocalEmbedFooterBuilder() With {.Text = $"You guessed {response.Message.Content}"}
            }
            Await message.ModifyAsync(Sub(msg) msg.Embed = builder.Build)
            If response.Message.Content.Equals(answer, StringComparison.OrdinalIgnoreCase) Then
                Await message.AddReactionAsync(New LocalEmoji("🎉"))
            Else
                Await message.AddReactionAsync(New LocalEmoji("❌"))
            End If

            Await response.Message.DeleteAsync()
        End If
    End Function

    <Command("pick")>
    <Description("Pick a number between 1 and 5")>
    <RunMode(RunMode.Parallel)>
    Public Async Function GuessNumber() As Task
        Dim emoji = New LocalEmoji(ChrW(&H30 + Rng.Next(1, 6)) & ChrW(&H20E3))
        Dim message = Await ReplyAsync("Pick the mystery number -> ⏹️")

        For number = 1 To 5
            Await message.AddReactionAsync(New LocalEmoji(ChrW(&H30 + number) & ChrW(&H20E3)))
        Next

        Dim response = Await Context.Channel.WaitForReactionAsync(Function(x) x.User.Id.Equals(Context.User.Id), TimeSpan.FromSeconds(30))

        If response.Reaction.HasValue Then
            Await message.ModifyAsync(Sub(msg) msg.Content = $"The mystery number is -> {emoji}")

            If response.Reaction.Value.Emoji.Equals(emoji) Then emoji = New LocalEmoji("🎉") Else emoji = New LocalEmoji("☹️")

            Await message.AddReactionAsync(emoji)
        Else
            Await message.AddReactionAsync(New LocalEmoji("🤷"))
        End If
    End Function

    <Command("commands")>
    <Description("Display commands using a paginator")>
    <RunMode(RunMode.Parallel)>
    Public Async Function PagedHelp() As Task
        Dim pages = New List(Of Page)

        For Each cmd In Commands.GetAllCommands
            Dim builder As New LocalEmbedBuilder With {.Title = cmd.Name, .Description = cmd.Description, .Color = Color.SteelBlue}
            For Each param In cmd.Parameters
                builder.AddField(param.Name, CommandUtilities.FriendlyPrimitiveTypeNames.GetValueOrDefault(param.Type, param.Type.Name))
            Next
            If cmd.Aliases.Count > 1 Then builder.WithFooter($"Alt: {String.Join(",", cmd.Aliases.Skip(1).Select(Function(cmdAlias) cmdAlias))}")
            pages.Add(builder.Build)
        Next

        Dim pageProvider = New DefaultPageProvider(pages)
        Dim commandMenu = New PagedMenu(Context.User.Id, pageProvider)

        Await Context.Channel.StartMenuAsync(commandMenu, TimeSpan.FromMinutes(3))
    End Function
End Class
