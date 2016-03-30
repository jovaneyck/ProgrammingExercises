module RegexTypeProvider

open Xunit
open Swensen.Unquote
open FSharp.Text.RegexProvider

type ContactInfoRegex = Regex< @"(?<Name>[A-Za-z]*);(?<TeleponeNumber>\d*);" >
let myMatcher = ContactInfoRegex()

[<Fact>]
let canAccessRegexGroupsThroughTypeCheckPropertiesYay() =
    let aMatch = 
        match myMatcher.Match("Bob;0498736277;") with
        | regex when regex.Success-> 
            Some (regex.Name.Value, regex.TeleponeNumber.Value) //Static typechecking on regex groups!
        | _ -> 
            None
    test <@ Some ("Bob", "0498736277") = aMatch @>