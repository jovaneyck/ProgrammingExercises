module Diamond

open System

let make letter = 
    let makeLine width letter =
        let padding = String(' ', width - 1)
        sprintf "%s%c%s" padding letter padding

    let letters = ['A'..letter]
    
    letters
    @ (letters |> List.rev |> List.tail )
    |> Seq.map (makeLine letters.Length)
    |> Seq.reduce (fun f s -> 
        sprintf "%s%s%s" f Environment.NewLine s) 