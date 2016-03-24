module Exampletests

open Xunit
open Swensen.Unquote
open FsCheck
open FsCheck.Xunit

[<Fact>]
let sanityCheck() = 
    test <@ 1  + 1 = 2 @>

[<Property>]
let VerifiesAProperty (xs : int list) = 
   xs = (xs |> List.rev |> List.rev)