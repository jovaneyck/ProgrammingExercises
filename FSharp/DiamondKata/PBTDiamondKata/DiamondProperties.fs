module DiamondProperties

open System
open FsCheck
open FsCheck.Xunit

type UppercaseLetters =
    static member Chars() =
        Arb.Default.Char()
        |> Arb.filter (fun c -> 'A' <= c && c <= 'Z')

type DiamondPropertyAttribute() =
    inherit PropertyAttribute(
        Arbitrary = [| typeof<UppercaseLetters> |])

[<DiamondProperty(Verbose=true)>]
let ``Diamond is non-empty`` (letter : char) =
    let actual = Diamond.make letter
    not (String.IsNullOrWhiteSpace actual)

let split (s : String) = 
    s.Split([| Environment.NewLine |], StringSplitOptions.None)
let trim (s : String) = s.Trim()

[<DiamondProperty>]
let ``First row contains 'A'`` (letter : char) =
    let actual = Diamond.make letter
    let rows = split actual
    rows
    |> Seq.head
    //|> Seq.exists ((=) 'A')
    |> trim
        = "A"

let leadingSpaces (s : String) =
    let indexOfFirstNonSpace = s.IndexOfAny([| 'A' .. 'Z' |])
    s.Substring(0, indexOfFirstNonSpace)

let trailingSpaces (s : String) =
    let indexOfLastNonSpace = s.LastIndexOfAny([| 'A' .. 'Z' |])
    s.Substring(indexOfLastNonSpace + 1)

[<DiamondProperty>]
let ``All rows have a vertically symmetric contour`` (letter : char) =
    let actual = Diamond.make letter
    let rows = actual |> split
    rows
    |> Seq.forall (fun r -> 
        (leadingSpaces r) = (trailingSpaces r))