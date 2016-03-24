module Exampletests

open Xunit
open Swensen.Unquote
open FsCheck
open FsCheck.Xunit

[<Fact>]
let sanityCheck() = 
    test <@ 1  + 1 = 2 @>

[<Property>]
let ``reverse of reverse of a list is the list itself`` (xs : int list) = 
   xs = (xs |> List.rev |> List.rev)