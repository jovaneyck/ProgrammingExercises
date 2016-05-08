module Diamond

open System

let make letter =
    let mirrorAndFuse ls =
        ls @ (ls |> List.rev |> List.tail)  
    let makeLine width (letter, letterIndex) =
        let leftPadding = String(' ', width - 1 - letterIndex)
        let innerPadding = String(' ', width - 1 - leftPadding.Length)
        let left = 
            sprintf "%s%c%s" leftPadding letter innerPadding
            |> Seq.toList

        left
        |> mirrorAndFuse
        |> List.map string
        |> List.reduce (sprintf "%s%s")

    let lettersWithTheirIndex = 
        ['A'..letter]
        |> List.mapi(fun i l -> l,i)

    lettersWithTheirIndex
    |> mirrorAndFuse
    |> Seq.map (makeLine lettersWithTheirIndex.Length)
    |> Seq.reduce (fun f s -> 
        sprintf "%s%s%s" f Environment.NewLine s) 