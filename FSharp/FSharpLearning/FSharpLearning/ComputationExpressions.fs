module ComputationExpressions

open Xunit
open Swensen.Unquote

let fizzbuzzify n =
        match (n % 3, n % 5) with
        | (0,0) -> "FizzBuzz"
        | (0,_) -> "Fizz"
        | (_,0) -> "Buzz"
        | _     -> sprintf "%d" n

type FizzBuzzSequenceBuilder() =
    member x.Yield(v) = fizzbuzzify v
    member x.Delay(f) = f()
    member x.Combine(l,r) = Seq.append (Seq.singleton l) (Seq.singleton r)
    member x.Combine(l,r) = Seq.append (Seq.singleton l) r
    member x.For(g,f) = Seq.map f g

let fizzbuzz = FizzBuzzSequenceBuilder()

[<Fact>]
let canSolveFizzBuzzUsingAComputationExpression() =
    let expected = ["1"; "2"; "Fizz"; "4"; "Buzz"]
    let actual = 
        fizzbuzz { 
            yield 1
            yield 2
            for i in 3..5 do yield i 
        } |> List.ofSeq
    test <@ actual = expected @>

//String building example
type StringFragment = //monadic type
    | Empty
    | Fragment of string
    | Concatenation of StringFragment * StringFragment

    override x.ToString() =
        let rec flatten fragment (builder : System.Text.StringBuilder) =
            match fragment with
            | Empty -> builder
            | Fragment(s) -> builder.Append(s)
            | Concatenation(l,r) -> builder |> flatten l |> flatten r
        (new System.Text.StringBuilder() |> flatten x).ToString()

type StringFragmentBuilder() =  //builder
    member x.Zero() = Empty
    member x.Yield(v) = Fragment(v)
    member x.YieldFrom(v) = v
    member x.Combine(l, r) = Concatenation(l, r)
    member x.Delay(f) = f()
    member x.For(s, f) =
        //MapReduce much?
        Seq.map f s
        |> Seq.reduce (fun l r -> x.Combine(l, r))

let buildstring = StringFragmentBuilder()

[<Fact>]
let canBuildStrings() =
    let expected = "I can build strings me"
    let actual = 
        buildstring {
            yield "I " //Yield
            yield! //YieldFrom 
                buildstring {
                    yield "can "
                    yield "build "
                }
            for word in ["strings "; "me"] do yield word //For
        }

    test <@ actual.ToString() = expected @>

//https://fsharpforfunandprofit.com/posts/computation-expressions-intro/