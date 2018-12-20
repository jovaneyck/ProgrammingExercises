#r @"..\..\packages\FSharpx.Collections.1.17.0\lib\net40\FSharpx.Collections.dll"
#r @"..\..\packages\Unquote.3.1.1\lib\net45\Unquote.dll"
open FSharpx.Collections
open Swensen.Unquote

//DEQue for "Double Ended queue"
let dq = Deque.ofList [1..10]
let toList = Deque.toSeq >> Seq.toList

test <@ Deque.head dq = 1 @> //O(1) amortized
test <@ Deque.tail dq |> toList = [2..10] @> //O(1) amortized
test <@ Deque.last dq = 10 @> //O(1) amortized vs O(N) on List
test <@ Deque.initial dq |> toList = [1..9] @> //O(1) amortized vs O(N) on List
test <@ Deque.cons 0 dq |> toList = [0..10] @> //O(1)
test <@ Deque.conj 11 dq |> toList = [1..11] @> //O(1)
test <@ Deque.rev dq |> toList = [10..(-1)..1] @> //O(1)

test <@ 1 = match dq with
            | Deque.Nil -> -1
            | Deque.Cons (1, tail) -> 1
            | Deque.Conj (init, last) -> 5 @>
