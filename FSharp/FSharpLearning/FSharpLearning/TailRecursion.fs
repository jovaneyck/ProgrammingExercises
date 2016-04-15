module TailRecursion

open Xunit
open Swensen.Unquote

let rec ones length =
    if length = 0 then []
    else 1 :: ones (length - 1)

let onesTail length =
    let rec onesTR acc length =
        if length = 0 then acc
        else onesTR (1 :: acc) (length - 1)
    onesTR [] length

[<Fact>]
let tailRecursiveFunctionsDoNotCauseAStackOverflow() =
    let length = 1000000

    //let o = ones length //will stackoverflow
    let o = onesTail length
    test <@ o |> List.length = length @>