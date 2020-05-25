Imports Disqord.Bot
Imports Qmmands

<Name("Commands")>
Public Class CommandModule
    Inherits DiscordModuleBase(Of DiscordCommandContext)

    Public Property Commands As CommandService

    <Command("ping")>
    <Description("Display the websocket latency")>
    Public Async Function PingAsync() As Task
        Dim watch = Stopwatch.StartNew()
        Dim msg = Await ReplyAsync("Pong")

        Await msg.ModifyAsync(Sub(msgProperty) msgProperty.Content = $"🏓 {watch.ElapsedMilliseconds}ms")
    End Function

    <Command("clean", "clear", "cc")>
    <Description("Delete the command user's recent messages")>
    <Remarks("Defaults to 10 messages. User can specify a maximum of 100")>
    <RequireBotChannelPermissions(Permission.ManageMessages)>
    Public Async Function ClearMessagesAsync(<Range(1, 100, True, True)> Optional amount As Integer = 10) As Task
        Dim minCreatedAt = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromDays(14))
        Dim enumerator = Context.Channel.GetMessagesEnumerable(1000).GetAsyncEnumerator
        Dim userMessages As New List(Of Snowflake)

        While userMessages.Count < amount AndAlso Await enumerator.MoveNextAsync()
            Dim messages = enumerator.Current _
                                     .Take(amount - userMessages.Count) _
                                     .Where(Function(msg) msg.Id.CreatedAt > minCreatedAt AndAlso msg.Author.Id.Equals(Context.User.Id)) _
                                     .Select(Function(msg) msg.Id)
            userMessages.AddRange(messages)
        End While

        If userMessages.Any Then Await DirectCast(Context.Channel, CachedTextChannel).DeleteMessagesAsync(userMessages)
    End Function

    <Command("userinfo", "whois")>
    <Description("Displays basic information about a user")>
    Public Async Function UserInfoAsync(Optional user As CachedMember = Nothing) As Task
        If user Is Nothing Then user = Context.Member

        Dim builder As New LocalEmbedBuilder With {
            .Author = New LocalEmbedAuthorBuilder With {.Name = user.Tag, .IconUrl = user.GetAvatarUrl},
            .Description = user.Mention,
            .ThumbnailUrl = user.GetAvatarUrl,
            .Timestamp = DateTimeOffset.UtcNow,
            .Footer = New LocalEmbedFooterBuilder With {.IconUrl = Context.Bot.CurrentUser.GetAvatarUrl, .Text = user.Id.ToString}
        }

        Dim position = Context.Guild.Members.Values.OrderBy(Function(m) m.JoinedAt).ToList.FindIndex(Function(x) x.Id.Equals(user.Id))
        Dim strPosition = If(position = 0, "Owner", $"#{position}")
        Dim topRole = user.Roles.Values.OrderBy(Function(r) r.Position).Last()

        builder.AddField("Join Position", strPosition, True)
        If Not topRole.Equals(Context.Guild.DefaultRole) Then builder.AddField("Highest Role", topRole.Mention, True)
        builder.AddField("Joined Discord", user.CreatedAt.ToString("f"))
        builder.AddField("Joined Server", user.JoinedAt.ToString("f"))

        Await ReplyAsync(embed:=builder.Build)
    End Function

    <Command("help", "commands", "cmds")>
    <Description("Displays the list of available commands")>
    Public Function HelpAsync() As Task
        Dim builder As New LocalEmbedBuilder With {
            .Title = "Command List",
            .Color = Color.SteelBlue
        }
        For Each cmdModule In Commands.GetAllModules
            Dim cmds As New List(Of String)
            cmds.AddRange(cmdModule.Commands.Select(Function(c) $":white_small_square:{Markdown.Bold(c.Name)} {c.Description}"))
            builder.AddField(cmdModule.Name, $"{String.Join(Environment.NewLine, cmds)}{Environment.NewLine}")
        Next

        builder.WithFooter($"Requested by {Context.User}")
        builder.WithTimestamp(DateTimeOffset.UtcNow)

        Return ReplyAsync(embed:=builder.Build)
    End Function
End Class
