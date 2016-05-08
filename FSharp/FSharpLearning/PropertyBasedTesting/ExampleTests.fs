module Exampletests

open Xunit
open Swensen.Unquote
open FsCheck
open FsCheck.Xunit

[<Property>]
let ``reverse of reverse of a list is the list itself`` (xs : int list) (* generator supplies random input *)= 
   xs = (xs |> List.rev |> List.rev) 
   //If a failure occurs, tries to shrink to smallest input that falsifies the property
   (*
    FsCheck.Xunit.PropertyFailedException

    Falsifiable, after 3 tests (3 shrinks) (StdGen (455753529,296137357)):
    Original:
    [-1; -3; 2; 0]
    Shrunk:
    [1; 0]
   *)