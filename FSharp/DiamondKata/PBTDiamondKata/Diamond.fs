module Diamond

open System

let make letter = 
    let makeLine width (letter, letterIndex) =
        let leftPadding = String(' ', width - 1 - letterIndex)
        let innerPadding = String(' ', width - 1 - leftPadding.Length)
        match letter with
        | 'A' ->
            sprintf "%s%c%s" leftPadding letter leftPadding
        | other ->
            let left = 
                sprintf "%s%c%s" leftPadding letter innerPadding
                |> Seq.toList
            left
            @ (left |> List.rev |> List.tail)
            |> List.map string
            |> List.reduce (sprintf "%s%s")

    let lettersWithTheirIndex = 
        ['A'..letter]
        |> List.mapi(fun i l -> l,i)

    lettersWithTheirIndex
    @ (lettersWithTheirIndex |> List.rev |> List.tail )
    |> Seq.map (makeLine lettersWithTheirIndex.Length)
    |> Seq.reduce (fun f s -> 
        sprintf "%s%s%s" f Environment.NewLine s) 