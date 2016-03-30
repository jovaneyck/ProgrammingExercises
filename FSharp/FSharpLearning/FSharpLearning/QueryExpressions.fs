module QueryExpressions

open Swensen.Unquote
open Xunit
open System.Linq

[<Fact>]
let canUseStandardLinqQueries() =
    let filtered = 
        [1..100]
            .Where(fun el -> el % 2 = 0)
            .Skip(1)
            .Take(2)
            .Select(fun el -> el + 1)
            .ToList()
        |> List.ofSeq

    test <@ [5;7] = filtered @>

[<Fact>]
let canUseQueryExpressionsToGiveThingsAMoreFunctionalFeel() =
    let filtered = 
        query {
            for i in [1..100] do
            where (i % 2 = 0)
            skip 1
            take 2
            select (i + 1)
        } |> List.ofSeq

    test <@ [5;7] = filtered @>