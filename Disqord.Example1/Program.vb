Module Program
    Sub Main()
        If Not IO.File.Exists("token.txt") Then Console.WriteLine("Cannot find token.txt") : Return
        Using bot As New DisqordBot(IO.File.ReadAllText("token.txt"))
            bot.Run(Nothing)
        End Using
    End Sub
End Module