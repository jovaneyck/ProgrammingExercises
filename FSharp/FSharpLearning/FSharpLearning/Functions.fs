module Functions

open Swensen.Unquote
open Xunit

[<Fact>]
let canDefineAndCallFunctions() =
    let add a b = a + b
    test <@ 8 = add 3 5@>

[<Fact>]
let canPartiallyApplyFunctions() =
    let add a b = a + b
    let addTwo = add 2

    test <@ 7 = addTwo 5 @>

[<Fact>]
let canAliasFunctions() =
    let add = (+)
    test <@ 7 = add 2 5 @>

    let addTwo = (+) 2
    test <@ 4 = addTwo 2 @>