module Diamond

open System

let make letter = 
    ['A'..letter]
    |> Seq.map (sprintf "  %c  ")
    |> Seq.reduce (fun f s -> 
        sprintf "%s%s%s" f Environment.NewLine s) 