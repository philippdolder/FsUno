module Uno

    open System

    type Color = 
        | Red
        | Blue
        | Green
        | Yellow

    type Card = 
        | Number of value : int * color : Color

    type Command =
        | StartGame of StartGame
        | PlayCard of GameId : int * card : Card

    and StartGame = {
        GameId : int }

    type Event = 
        | GameStarted of GameStarted
        | CardPlayed of GameId : int * card : Card

    and GameStarted = {
        GameId : int }

    type State = {
        GameId : int
        IsStarted : bool
        CurrentPlayer : int } // 1 to 4

        with static member empty = { GameId = 0; IsStarted = false; CurrentPlayer = 1 }
     
    let exec (item:State) = function
        | StartGame game -> [GameStarted { GameId = game.GameId }]
        | PlayCard(id, card) -> 
            match item.IsStarted with
            | true -> [CardPlayed(id, card)]
            | _ -> raise (InvalidOperationException("Cannot play card in a game that is not started"))

    let apply (item:State) = function
        | GameStarted event -> { item with State.GameId = event.GameId; State.IsStarted = true; }
        | CardPlayed(id, card) -> item

    let load events : State =
        events |> List.fold apply State.empty
