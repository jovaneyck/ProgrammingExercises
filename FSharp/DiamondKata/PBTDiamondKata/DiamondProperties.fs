module DiamondProperties

open System
open FsCheck
open FsCheck.Xunit

type UppercaseLetters =
    static member Char() =
        Arb.Default.Char()
        |> Arb.filter (fun c -> 'A' <= c && c <= 'Z')

[<Property(Arbitrary = [| typeof<UppercaseLetters> |])>]
let ``Diamond is non-empty`` (letter : char) =
    let actual = Diamond.make letter
    not (String.IsNullOrWhiteSpace actual)