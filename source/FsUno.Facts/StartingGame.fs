module GameStart
    open Xunit
    open FsUnit.Xunit
    open System
    open Uno


    [<Fact>]
    let ``Starting a game`` () =
        let events = []
        let newEvents : list<Event> = send

        newEvents |> List.isEmpty |> should be False

//    [<Fact>]
//    let ``Finishing a game`` () = 
//        let events = []
//        events |> ignore
