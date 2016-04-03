let toInt (text : string) =
    match System.Int32.TryParse(text) with
    | (true,parsedNumber) -> Some parsedNumber
    | _ -> None

type MaybeBuilder() =
    member this.Bind(m, f) = Option.bind f m
    member this.Return(v) = Some v
    
let maybe = MaybeBuilder()

//with maybe computation expression:
let stringAdd x y z =
    maybe {
        let! a (*int!*) = toInt x (*int option!*)
        let! b = toInt y
        let! c = toInt z
        return a+b+c 
        (*a+b+c: int but return type of stringAdd = int Option!*)
    }

let good = stringAdd "12" "3" "2"
let bad = stringAdd "12" "xyz" "2"

//Or with an infix bind operator:
let strAdd str i = toInt str |> Option.map ((+) i)
let (>>=) m f = Option.bind f m

let good2 = toInt "1" >>=  strAdd "2" >>= strAdd "3"
let bad2 = toInt "1" >>=  strAdd "xyz" >>= strAdd "3"