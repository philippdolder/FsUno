module UnoFacts
    open Uno
    open Xunit
    open System

    [<Fact>]
    let ``StartGame emits GameStarted`` () =
        let id = 5
        
        let result = Uno.exec State.empty (StartGame { GameId = id })

        Assert.Equal(1, result.Length)
        Assert.Contains(GameStarted { GameId = id }, result)

    [<Fact>]
    let ``Game is started after GameStarted`` () =
        let id = 7

        let result = Uno.apply State.empty (GameStarted { GameId = id })

        Assert.True(result.IsStarted)

    [<Fact>]
    let ``It is Player 1's turn after GameStarted`` () =
        let result = Uno.apply State.empty (GameStarted { GameId = 5 })

        Assert.Equal(1, result.CurrentPlayer)

    [<Fact>]
    let ``PlayCard after GameStarted emits CardPlayed`` () =
        let id = 5
        let card = Number(2, Red)

        let startedGame = Uno.apply State.empty (GameStarted { GameId = id })
        let result = Uno.exec startedGame (PlayCard(id, card))

        Assert.Equal(1, result.Length)
        Assert.Contains(CardPlayed(id, card), result)

    [<Fact>]
    let ``PlayCard before GameStarted throws exception`` () =
        let result = fun () -> Uno.exec State.empty (PlayCard(5, Number(3, Blue))) |> ignore

        Assert.Throws<InvalidOperationException>(result)