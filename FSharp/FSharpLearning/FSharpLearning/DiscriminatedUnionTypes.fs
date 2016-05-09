module DiscriminatedUnionTypes

open Xunit
open Swensen.Unquote

type Tree =
    | InnerNode of int * Tree * Tree
    | Leaf of int

let rec NbNodes t =
    match t with
    | Leaf _ -> 1

[<Fact>]
let canPatternMatchOnUnionTypes() =
    let tree = 
        InnerNode(3, 
            InnerNode(2, 
                Leaf 1, 
                Leaf 1), 
            Leaf 2)

    let tuple = (1 ,2 ,3)
    let (x,_,z) = tuple

    test <@ 5 = NbNodes tree @>