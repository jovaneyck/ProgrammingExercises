//[tutorial: parsing JSON](http://www.quanttec.com/fparsec/tutorial.html#parsing-json)

//The ORDER of imports is important in .fsx scripts or you will get weird "The type referenced through 'FParsec.CharStream`1' is defined in an assembly that is not referenced. You must add a reference to assembly 'FParsecCS'." errors
#r @"..\..\packages\FParsec.1.0.3\lib\net40-client\FParsecCS.dll"
#r @"..\..\packages\FParsec.1.0.3\lib\net40-client\FParsec.dll"
#r @"..\..\packages\Unquote.3.1.1\lib\net45\Unquote.dll"

open FParsec
open Swensen.Unquote

type Json = JString of string
          | JNumber of float
          | JBool   of bool
          | JNull
          | JList   of Json list

//Making a recursive parser: we're defining a Parser "object", not a recursive F# function, notice the lack of "rec" keyword.
let pjson =
    //This forwardedToRef thing gives us a way to forward reference our recursive parser in its own definition.
    let parser, parserRef = createParserForwardedToRef<Json, unit>()
    let pCharacter = ['a'..'z'] @ ['A'..'Z'] |> Seq.map pchar |> choice
    let betweenChars l r p f = between (pchar l) (pchar r) p |>> f
    let pString = betweenChars '"' '"' (many pCharacter) (Seq.map string >> String.concat "" >> JString)
    let pFloat = pfloat |>> JNumber
    let pBool = (pstring "true" >>% true) <|> (pstring "false" >>% false) |>> JBool
    let pNull = pstring "null" >>% JNull
    let pList = betweenChars '[' ']' (sepBy parser (pchar ',')) JList
    
    do parserRef := //Aaaaand initialize the reference with the concrete parser implementation
        pString 
            <|> pFloat
            <|> pBool
            <|> pNull
            <|> pList
    parser .>> eof

let testRun parser input expected =
    test <@ match run parser input with | ParserResult.Success(result,_,_) -> result = expected | _ -> false @>

let tt () = 
    testRun pjson "\"hello\"" (JString "hello")
    testRun pjson "\"world\"" (JString "world")
    testRun pjson "33.2" (JNumber 33.2)
    testRun pjson "true" (JBool true)
    testRun pjson "false" (JBool false)
    testRun pjson "null" (JNull)
    testRun pjson "[1,2,\"three\"]" (JList [JNumber 1.; JNumber 2.; JString "three"])

tt ()