#r "../packages/Unquote.3.1.1/lib/net45/Unquote.dll"
open Swensen.Unquote

let add a b = a + b

test <@ 5 = add 3 1 @>

test <@ ([3; 2; 1; 0] |> List.map ((+) 1)) = [1 + 3..1 + 0] @>