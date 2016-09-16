module Main

open System

let printDiamond l =
    printfn "%s" (Diamond.make l)

[<EntryPoint>]
let main (args : string array) =
    try
        if args = [||] then
            printfn "No input provided. Going all out!"
            printDiamond 'Z'
    
        match args.[0] |> System.Char.TryParse with
        | (false, _) -> failwith "Please provide an input letter"
        | (true, letter) -> printDiamond letter

        0
    with
        e -> 
            printfn "ERROR: %s" e.Message
            1