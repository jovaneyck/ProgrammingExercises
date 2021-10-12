#r "nuget: Unquote"
#r "nuget: Newtonsoft.Json"

open Swensen.Unquote
open Newtonsoft.Json

//Declare using {||} syntax
let anonymousRecord = {| AList = [1..10] |} // : {| AList : int list |}

//accessors
test <@ anonymousRecord.AList = [1..10] @>

//They have structural equality
test <@ {|A = 1|} = {|A = 1|} @>

//Functions can return anonymous records
let functionReturningAnonymousRecord () = // : unit -> {| A : int; B : int |}
    {| A = 1; B = 2 |}

test <@ (functionReturningAnonymousRecord ()).B = 2 @>

//Functions can accept anonymous records
let functionsCanAcceptAnonymousRecors (input : {| A : int |}) =
    input.A

test <@ functionsCanAcceptAnonymousRecors {| A = 2 |} = 2 @>
//test <@ functionsCanAcceptAnonymousRecors {| A = 2; B = 3 |} = 2 @> //This does NOT work as you get a compiler error: "This anonymous record has too many fields".

//Useful scenario: mutually recursive types used to be "anded" together:
type FullName = { FirstName : string; LastName: string }
type ManagerData = { Name : FullName; Reports : Employee list} //Explicit type of the container used below in the discriminated union
and Employee = Manager of ManagerData | Engineer of FullName //<- note the anding here because mutually recursive

let mgr1  : Employee = Manager { Name = { FirstName = "C"; LastName = "EO"}; Reports = []}

//With anonymous records you can skip a step:
type Employee2 = Manager2 of {| Name : FullName; Reports : Employee2 list |} | Engineer of FullName
let mgr2  : Employee2 = Manager2 {| Name = { FirstName = "C"; LastName = "EO"}; Reports = [] |} //this is a leaky abstraction: anonymousness of the type leaks out into its data constructor

//Updating anonymous records: same as ctor: anonymous syntax leaks into updates
let x = {| A = 1; B = 2 |}
//let updated = { x with A = 2} //The record label 'A' is not defined, super helpful error message dear compiler :(
let updated = {| x with A = 3 |}
test <@updated = {| A = 3; B = 2|} @>

//semi-useful scenario: map a regular record type into an anonymous record with an added update
type Record = { A : int; B : int}
let explicitlyTyped = { A = 13; B = 14 }
let anon = {| explicitlyTyped with B = 15 |}

test <@ anon = {| A = 13; B = 15 |} @>

//They can be serialized:
test <@ JsonConvert.SerializeObject(explicitlyTyped) = "{\"A\":13,\"B\":14}" @>
test <@ JsonConvert.SerializeObject(anon) = "{\"A\":13,\"B\":15}" @>
