//[tutorial](http://www.quanttec.com/fparsec/tutorial.html)

//The ORDER of imports is important in .fsx scripts or you will get weird "The type referenced through 'FParsec.CharStream`1' is defined in an assembly that is not referenced. You must add a reference to assembly 'FParsecCS'." errors
#r @"..\..\packages\FParsec.1.0.3\lib\net40-client\FParsecCS.dll"
#r @"..\..\packages\FParsec.1.0.3\lib\net40-client\FParsec.dll"
#r @"..\..\packages\Unquote.3.1.1\lib\net45\Unquote.dll"

open FParsec
open Swensen.Unquote

let testParse (parser : Parser<'a, unit>) (input : string) : 'a =
    match run parser input with //"run" a parser
    | ParserResult.Success (result,_,_) -> result
    | ParserResult.Failure _ -> failwithf "Did not expect to parse fail"

let expectParseFail parser input messagePredicate =
    match run parser input with 
    | ParserResult.Failure (msg,_,_) -> messagePredicate msg 
    | _ -> false

type direction = North | East | South | West

let tt () = 
    //Parsing floats
    test <@ testParse pfloat "33.2" = 33.2 @>
    test <@ expectParseFail pfloat "TYPO33.2" (fun msg -> msg.Contains("Expecting: floating-point number")) @>

    //Parsing specific strings
    test <@testParse (pstring "hello world") "hello world" = "hello world" @>
    test <@expectParseFail (pstring "hello world") "hUllo world" (fun msg -> msg.Contains("Expecting: 'hello world'")) @>

    //Combining parsers >>. and .>> (. is the side that "returns its value", other end just nibbles the matching input
    let floatBetweenBrackets = pstring "[" >>. pfloat .>> pstring "]"
    test <@ testParse floatBetweenBrackets "[33.4]" = 33.4 @>

    //Abstracting parsers: how about making a "something between brackets" parser?
    let pBetweenBrackets parser = pstring "[" >>. parser .>> pstring "]"
    let intBetweenBrackets = pint32 |> pBetweenBrackets
    let builtInBetween = between (pstring "[") (pstring "]") pint32
    test <@ testParse intBetweenBrackets "[2]" = 2 @>
    test <@ testParse builtInBetween "[212]" = 212 @>

    //Parsing lists:
    //*: many
    test <@ testParse (many builtInBetween) "[1][2][3]" = [1;2;3] @>
    test <@ testParse (many builtInBetween) "" = [] @>
    test <@ expectParseFail (many builtInBetween) "[1][NOPE][3]" (fun msg -> msg.Contains("Expecting: integer number")) @>
    //+ (at least one): many1
    test <@ testParse (many1 builtInBetween) "[1]" = [1] @>
    test <@ expectParseFail (many1 builtInBetween) "" (fun msg -> msg.Contains("Expecting: '['")) @>

    //Skipping irrelevant tokens: skipMany/skipMany1
    test <@ testParse ((skipMany <| pchar 'a') >>. pint32) "aaaaa1337" = 1337 @>

    //x-separated lists
    test <@ testParse (sepBy pint32 (pchar ',')) "1,2,3,4" = [1;2;3;4] @>
    test <@ testParse (sepBy pint32 (pchar ',')) "" = [] @>

    //Combined parsers give helpful error messages
    let pNumberList = pstring "[" >>. sepBy pint32 (pstring ",") .>> pstring "]"
    test <@ expectParseFail pNumberList "[12" (fun msg -> msg.Contains("Expecting: ',' or ']'")) @>
    //Custom parser error messages. <?>
    test <@ expectParseFail (many1 builtInBetween) "" (fun msg -> msg.Contains("Expecting: '['")) @>
    test <@ expectParseFail (many1 (builtInBetween <?> "a number between brackets")) "" (fun msg -> msg.Contains("Expecting: a number between brackets")) @>

    //Ignoring trailing whitespace
    test <@ expectParseFail pNumberList "[1, 2]" (fun msg -> msg.Contains("Expecting: integer number")) @>
    let pNumberListSpaces = (pstring "[" .>> spaces) >>. sepBy (pint32 .>> spaces) (pstring "," .>> spaces) .>> (pstring "]" .>> spaces)
    test <@ testParse pNumberListSpaces "[ 1 , 2 ] " = [1;2] @>

    //Making sure you parse the entire string: eof
    test <@ testParse (pint32) "32a" = 32 @> //that's not what we want!
    test <@ expectParseFail (pint32 .>> eof) "32a" (fun msg -> msg.Contains("Expecting: end of input")) @>

    //Parsers return matched contents
    test <@ testParse (many <| pstring "a") "aaaa" = ["a"; "a"; "a"; "a"] @>
    //skipX parsers perform faster if you don't care about the results
    test <@ testParse (many <| skipString "a") "aaaa" = [();();();()] @>
    test <@ testParse (skipString "a" >>. pint32) "a33" = 33 @>

    //String parsing
    //CaseInsensitive
    test <@ testParse (skipStringCI "<INTEGER>" >>. pint32) "<inteGer>32" = 32 @>

    //Sequential parsers
    //Say you need the consumed result of multiple parsers in a pipeline, enter tuple/pipe!
    //tuple2: Parser<'a,'u> -> Parser<'b, 'u> -> Parser<'a*'b, 'u>
    test <@ testParse (tuple2 pint32 (pstring "*" >>. pint32)) "3*2" = (3,2) @>
    //Shorthand: (.>>.)
    test <@ testParse (pint32 .>>. (pstring "*" >>. pint32)) "3*2" = (3,2) @>
    //pipe2: Parser<'a,'u> -> Parser<'b, 'u> -> ('a->'b->'c) -> Parser<'c, 'u>
    let product = pipe2 pint32 (pstring "*" >>. pint32) (*)
    test <@ testParse product "3*2" = 6 @>

    //Parsing to custom types: xReturn, >>% and bind |>>
    let pdir = stringReturn "N" North
    test <@ testParse pdir "N" = North @>
    test <@ testParse (pint32 >>% "a number") "33" = "a number" @>
    test <@ testParse (pint32 |>> (fun i -> sprintf "the number %d" i)) "33" = "the number 33" @>

    //Alternatives: <|>
    let pbool = stringReturn "true" true <|> stringReturn "false" false

    test <@ testParse pbool "true" = true @>
    test <@ testParse pbool "false" = false @>
    test <@ expectParseFail pbool "carrot" (fun msg -> msg.Contains("Expecting: 'false' or 'true'")) @>
    
tt ()