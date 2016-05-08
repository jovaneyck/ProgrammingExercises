module Diamond

open System

let make letter = 
    let letters = ['A'..letter]
    
    letters
    @ (letters |> List.rev |> List.tail )
    |> Seq.map (sprintf "  %c  ")
    |> Seq.reduce (fun f s -> 
        sprintf "%s%s%s" f Environment.NewLine s) 