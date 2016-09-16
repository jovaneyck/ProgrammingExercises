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

[<DiamondProperty>]
let ``A side of the diamond has the correct letters in the correct order`` (letter : char) =
    let diamond = Diamond.make letter
    let expected = ['A'..letter]
    let rows = diamond |> split
    let lettersOnTopLeftSide =
        rows
        |> Seq.take expected.Length
        |> Seq.map trim
        |> Seq.map Seq.head
        |> Seq.toList
    expected = lettersOnTopLeftSide

[<DiamondProperty>]
let ``Diamond is symmetric around the horizontal axis`` (letter : char) =
    let diamond = Diamond.make letter
    let rows = split diamond
    let topRows = 
        rows 
        |> Seq.takeWhile (fun r -> not (r.Contains(string letter)))
        |> Seq.toList
    let bottomRows =
        rows
        |> Seq.skipWhile (fun r -> not (r.Contains(string letter)))
        |> Seq.skip 1
        |> Seq.toList
        |> List.rev
    topRows = bottomRows

[<DiamondProperty>]
let ``Diamond should be as high as it is wide`` (letter : char) =
    let diamond = Diamond.make letter
    let rows = split diamond
    let height = rows.Length
    rows 
    |> Seq.forall(fun r -> 
            let width = r.Length
            width = height)

[<DiamondProperty>]
let ``All inner rows have two identical letters``(letter : char) =
    let diamond = Diamond.make letter
    let hasTwoIdenticalLetters x =
        let hasTwoLetters = x |> Seq.length = 2
        let areIdentical = x |> Seq.distinct |> Seq.length = 1
        hasTwoLetters && areIdentical
    let rows = split diamond
    rows
    |> Seq.filter (fun r -> not (r.Contains("A")))
    |> Seq.map (fun s -> s.Replace(" ", ""))
    |> Seq.forall hasTwoIdenticalLetters

[<DiamondProperty>]
let ``Lower left outer padding is a isoscele right triangle`` (letter : char) =
    let diamond = Diamond.make letter
    let rows = split diamond
    let lowerLeftOuterPadding =
        rows
        |> Seq.skipWhile (fun r -> not (r.Contains(string letter)))
        |> Seq.map leadingSpaces
        |> Seq.toList
    let spaceCounts =
        lowerLeftOuterPadding
        |> List.map (fun r -> r.Length)

    let expected = 
        Seq.initInfinite id 
        |> Seq.take spaceCounts.Length
        |> Seq.toList
    expected = spaceCounts