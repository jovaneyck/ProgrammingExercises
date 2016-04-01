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

//Loging values
type LoggingBuilder(sb : System.Text.StringBuilder) =
    let log p = sb.AppendLine(sprintf "Log: %A" p) |> ignore

    member this.Bind(x, f) = 
        log x
        f x

    member this.Return(x) = 
        x

[<Fact>]
let canAutomagicallyLogBetweenStatementsUsingAComputationExpression() =
    let buffer = System.Text.StringBuilder()
    let logger = LoggingBuilder(buffer)

    let sum = 
        logger {
            let! x = 1
            let! y = 2
            let! z = x + y
            return z
        }

    test <@ sum = 3 @>
    test <@ "Log: 1\r\nLog: 2\r\nLog: 3\r\n" = buffer.ToString() @>

//Safe division by zero
let safeDivide bottom top = //divisor first to allow easier chaining
    if bottom = 0 then None
    else Some (top / bottom)

type MaybeBuilder() =

    member this.Bind(x, f) = 
        match x with
        | None -> None
        | Some a -> f a

    member this.Return(x) = 
        Some x
   
let maybe = new MaybeBuilder()

[<Fact>]
let canSafelyDivideByZeroUsingTheMaybeMonad() =
    let result =
        maybe {
            let! x = 12 |> safeDivide 3
            let! y = x |> safeDivide 2
            return y
        }
    test <@ result = Some 2 @>

    let divideByZeroSomewhereInBetween =
        maybe {
            let! x = 12 |> safeDivide 3
            let! y = x |> safeDivide 0
            let! z = y |> safeDivide 1
            return z
        }

    test <@ divideByZeroSomewhereInBetween = None @>