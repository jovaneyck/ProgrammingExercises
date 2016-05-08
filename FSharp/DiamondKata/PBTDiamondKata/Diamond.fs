module Diamond

open System

let make letter = 
    let makeLine width letter =
        let padding = String(' ', width - 1)
        match letter with
        | 'A' ->
            sprintf "%s%c%s" padding letter padding
        | other ->
            let left = 
                sprintf "%c%s" letter padding
                |> Seq.toList
            left
            @ (left |> List.rev |> List.tail)
            |> List.map string
            |> List.reduce (sprintf "%s%s")

    let letters = ['A'..letter]
    
    letters
    @ (letters |> List.rev |> List.tail )
    |> Seq.map (makeLine letters.Length)
    |> Seq.reduce (fun f s -> 
        sprintf "%s%s%s" f Environment.NewLine s) 