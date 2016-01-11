module RecursiveTypes

open Xunit
open Swensen.Unquote

type A = {B : B Option; AText : string}
and B = {A : A Option; BText : string}

[<Fact>]
let canDefineMutuallyRecursiveTypesUsingTheAndKeyword() =
    //A references B, which in turn references A using the "and" keyword in the type declaration.
    test <@ "BText here!" = {B = Some {BText = "BText here!"; A = None}; AText = "AText"}.B.Value.BText @>

type Centimeters = {CValue : float}
type Meters = {MValue : float}

type Centimeters with
    static member From {MValue = v} = {CValue = v * 100.0 }
type Meters with
    static member From {CValue = v} = {MValue = v / 100.0}

[<Fact>]
let canDefineMutuallyRecursiveTypesUsingStaticTypeExtensions() =
    //if it's a method that would introduce a circular dependency, you can use (static) type extensions
    test <@ Centimeters.From {MValue = 2.0} = {CValue = 200.0} @>