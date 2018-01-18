#r "../packages/Unquote.3.1.1/lib/net45/Unquote.dll"
open Swensen.Unquote

//Fsharp.Core.Result<'a,'b> is a sum type of Ok<'a> and Error<'b>
let ok : Result<int,'a> = Ok 3
let error : Result<'a, string> = Error "something went wrong"

//You can destructure/pattern match as usual
let handle result =
    match result with
    | Ok value -> value
    | Error _ -> -1

test <@ handle ok = 3 @>
test <@ handle error = -1 @>

//result module functions

//Result.map
let addOne : int -> int = (+) 1
test <@ ok |> Result.map addOne = Ok 4 @>
test <@ error |> Result.map addOne = error @>

//Result.bind
let divideBy y x : Result<int, string> = 
    if y = 0 
    then Error "divide by zero"
    else Ok (x / y)

test <@ Result.bind (divideBy 3) ok = Ok 1 @>
test <@ Result.bind (divideBy 3) error = error @>
test <@ Result.bind (divideBy 0) ok = Error "divide by zero" @>
test <@ Result.bind (divideBy 0) error = error @>

//Why? Even with "error cases", pipeling becomes straightforward:
test <@ Ok 35 |> Result.bind (divideBy 7) |> Result.bind (divideBy 5) = Ok 1 @>
test <@ Ok 35 |> Result.bind (divideBy 0) |> Result.bind (divideBy 5) = Error "divide by zero" @>

//Or even more straightforward with computation expressions:
// http://blog.ploeh.dk/2016/03/21/composition-with-an-either-computation-expression/
type ResultBuilder () =
    member this.Bind(x, f) = Result.bind f x
    member this.ReturnFrom x = x
let result = ResultBuilder ()

//usage:
test <@ Ok 2 = 
    result {
            let x = 30
            let! y = x |> divideBy 5
            return! y |> divideBy 3
    } @>


test <@ Error "divide by zero" = 
    result {
            let x = 30
            let! y = x |> divideBy 0
            return! y |> divideBy 3
    } @>