module PatternMatching

open Xunit
open Swensen.Unquote

[<Fact>]
let canDecomposeStuffByAMatchEpression() =
    let point = (30, 40)
    let x,y = point //deconstruct all the things by a match epression in a let-binding or wherever!
    test <@ x = 30 @>
    test <@ y = 40 @>
    
    let projectToX : int * int -> int * int = 
        (fun (_,y) -> (0,y))     //also possible in argument signature
    test <@ projectToX point = (0,40) @>

[<Fact>]
let basicPatternMatchExample() =
    let isOnAxis p =
        match p with
        | (0,0) -> "origin"
        | (0,_) -> "x-axis"
        | (_,0) -> "y-axis"
        | p -> "not on axis"

    test <@ isOnAxis (0,3) = "x-axis" @>
    test <@ isOnAxis (0,0) = "origin" @>
    test <@ isOnAxis (1,2) = "not on axis" @>

type Animal =
        | Dog of string
        | Cat of int

[<Fact>]
let compilerIssuesAWarningWhenMatchIsNotExhaustive() =
    let isDog a = 
        match a with 
            //compiler warning: incomplete pattern match on this expression. 
            //For example, the value Cat(_) may indicate a case not covered by the pattern(s).
        | Dog _ -> true
    
    test <@ Dog "Nestor" |> isDog @>
    Assert.Throws<MatchFailureException>(fun () -> //runtime horror :(
        Cat 4 |> isDog |> ignore)

[<Fact>]
let canMatchOnConstantVariableOrWildcard() =
    let isOnAxis p =
        match p with
        | (0,0) -> true //constants
        | (x, 0) -> true //variable and constant
        | (0, y) -> true //variable and constant
        | (_,_) -> false //wildcards
    ()

[<Fact>]
let canMatchOnUnionCases() =

    let nameOrNumberOfLegs a =
        match a with
        | Cat nb -> nb.ToString()
        | Dog name -> name

    test <@ nameOrNumberOfLegs (Dog "Max") = "Max" @>
    test <@ nameOrNumberOfLegs (Cat 4) = "4" @>

[<Fact>]
let canDeconstructCollections() =
    let rec length (l : int list) =
        match l with
        | [] -> 0
        | h :: t -> 1 + (length t)

    test <@ [1;2;3;4] |> length = 4 @>

[<Fact>]
let canBothPatternMatchAndNameTheEntireMatchedExpressionUsingTheAsKeyword() =
    let onAxis =
        function
        | ((0,y) : int * int) as p -> sprintf "%A is on the x-axis" p
        | _ -> "don't care now"

    test <@ onAxis (0,3) = "(0, 3) is on the x-axis" @>

[<Fact>]
let canAddExtraMatchConstraintsUsingTheWhenKeyword() =
    let onAxis p =
        match p with
        | (x,y) when x = 0 || y = 0 -> true
        | _ -> false

    test <@ onAxis (1,0) @>
    test <@ not (onAxis (1,2)) @>

[<Fact>]
let canDefineAnActivePatternForCustomPatternMatching() =
    //fun fact, the | here are called "banana clips"
    let (|Fizz|Buzz|FizzBuzz|Number|) n =
        match (n % 3, n % 5) with
        | (0, 0) -> FizzBuzz 
        | (0, _) -> Fizz
        | (_, 0) -> Buzz
        | _ -> Number n

    let fizzBuzzify =
        function
        | Fizz -> "Fizz"
        | Buzz -> "Buzz"
        | FizzBuzz -> "FizzBuzz"
        | Number n -> n.ToString()

    test <@ fizzBuzzify 1 = "1" @> 
    test <@ fizzBuzzify 15 = "FizzBuzz" @> 

[<Fact>]
let canDefineActivePatternsIncrementallyUsingPartialActivePatterns() =
    //partial active patterns have a wildcard in the banana clip and return an option
    let (|Fizz|_|) n = if n % 3 = 0 then Some Fizz else None
    let (|Buzz|_|) n = if n % 5 = 0 then Some Buzz else None
    
    let fizzBuzzify n =
        match n with
        | Fizz & Buzz -> "FizzBuzz"
        | Fizz -> "Fizz"
        | Buzz -> "Buzz"
        | n -> n.ToString()

    test <@ fizzBuzzify 1 = "1" @>
    test <@ fizzBuzzify 3 = "Fizz" @>
    test <@ fizzBuzzify 15 = "FizzBuzz" @>

[<Fact>]
let canParameterizeActivePatterns() =
    //using some partial application magic
    let (|DivisibleBy|_|) d n =
        if n % d = 0 then Some DivisibleBy else None

    let fizzBuzzify =
        function
        | DivisibleBy 3 & DivisibleBy 5 -> "FizzBuzz"
        | DivisibleBy 3 -> "Fizz"
        | DivisibleBy 5 -> "Buzz"
        | n -> n.ToString()
            
    test <@ fizzBuzzify 1 = "1" @>
    test <@ fizzBuzzify 3 = "Fizz" @>
    test <@ fizzBuzzify 15 = "FizzBuzz" @>