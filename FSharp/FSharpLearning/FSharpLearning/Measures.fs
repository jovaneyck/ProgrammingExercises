module Measures

open Swensen.Unquote
open Xunit

[<Measure>] type m
[<Measure>] type h

//Can create derived measure formulas
[<Measure>] type mph = m / h

//Can enforce measures as argument types
let toMilesPerHour (miles : float<m>) (hour : float<h>) : float<mph> = miles / hour

[<Fact>]
let measuresCanResultInCompilerErrorsWhenDevelopersMakeAMistake() =
    let distance = 50.0<m>
    let time = 2.0<h>

    test <@ 25.0<mph> = (toMilesPerHour distance time) @>
    //test <@ 25.0<mph> = (toMilesPerHour time distance) @> //Compiler errors! Type mismatch: expecting a float<m> but given a float<h>!