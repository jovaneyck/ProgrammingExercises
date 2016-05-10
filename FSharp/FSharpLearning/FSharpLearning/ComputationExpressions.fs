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
type LoggingBuilder() =
    member this.Bind(x, f) = 
        printfn "%A" x
        f x

    member this.Return(x) = 
        x

[<Fact>]
let canAutomagicallyLogBetweenStatementsUsingAComputationExpression() =
    let buffer = System.Text.StringBuilder()
    let logged = LoggingBuilder()

    let sum = 
        logged {
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
    //"Wrapper type" (Monadic type): 'T option

    member this.Bind(x, f) =  //Or simply <Option.bind f x>
        match x with
        | None -> None
        | Some a -> f a

    //Wrap result in wrapper type
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

[<Fact>]
let canUseInfixBindOperatorInsteadOfComputationExpressions() =
    let toInt (text : string) =
        match System.Int32.TryParse(text) with
        | (true,parsedNumber) -> Some parsedNumber
        | _ -> None

    //adding strings with maybe computation expression:
    let stringAdd x y z =
        maybe {
            ////computation expressions "Wrap" and "Unwrap" behind the scenes:
            let! a (*: int!*) = toInt x (*: int option!*) 
            let! b = toInt y
            let! c = toInt z
            return a+b+c 
            (*a+b+c: int but return type of stringAdd = int Option!*)
        }

    test <@ stringAdd "12" "3" "2" = Some 17 @>
    test <@ stringAdd "12" "xyz" "2" = None @>

    //Or with an infix bind operator:
    let strAdd str i = toInt str |> Option.map ((+) i)
    let (>>=) m f = Option.bind f m

    let goodSum = toInt "1" >>=  strAdd "2" >>= strAdd "3"
    test <@ goodSum = Some 6 @>
    let badSum = toInt "1" >>=  strAdd "xyz" >>= strAdd "3"
    test <@ badSum = None @>

//Bind signature: M<'T> * ('T -> M<'U>) -> M<'U>
//Types T and U are generic and don't have to be the same (as customer ID and order ID in example below):

//Different types:
type CustomerId = 
    | CustomerId of string
type OrderId = 
    |OrderId of int

//wrapper type
type DbResult<'t> =
    | Success of 't
    | Error of string

//Some query stubs
let getCustomer name : DbResult<CustomerId> =
    match name with
    | "Alice" -> Success (CustomerId "Alice ID")
    | unknown -> Error ("Did not find customer "+unknown)

let getLastOrderId customerId : DbResult<OrderId> =
    match customerId with
    | CustomerId "Alice ID" -> Success (OrderId 1)
    | CustomerId unknown -> Error ("No orders for customer "+unknown)

type DbResultBuilder() =
    member this.Bind(m, f) = 
        match m with
        | Error e -> Error e
        | Success a -> f a
    member this.Return(x) = 
        Success x

let dbresult = new DbResultBuilder()

[<Fact>]
let wrappedTypeDoesNotHaveToBeTheSameInEachStep()=
    let fetchLastOrderIdForCustomer name = //string->DbResult<OrderId>
        dbresult {
            let! (customerId : CustomerId) = getCustomer name //string->DbResult<CustomerId>
            let! (orderId : OrderId) = getLastOrderId customerId //CustomerId->DbResult<OrderId>
            return orderId //OrderId
        }

    test <@ fetchLastOrderIdForCustomer "Alice" = Success (OrderId 1) @>
    test <@ fetchLastOrderIdForCustomer "Bob" = Error "Did not find customer Bob" @>