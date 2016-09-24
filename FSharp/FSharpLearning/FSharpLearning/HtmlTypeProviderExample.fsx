#r "../packages/FSharp.Data.2.2.5/lib/net40/FSharp.Data.dll"
//#load "../packages/FSharp.Charting.0.90.13/FSharp.Charting.fsx"

open FSharp.Data
//open FSharp.Charting

type BoardGameRankingProvider = HtmlProvider<Sample="https://boardgamegeek.com/browse/boardgame?sort=bggrating&sortdir=desc">
let rankings = BoardGameRankingProvider()

let topRatedBoardGamesTable = rankings.Tables.Collectionitems

let topRatedGamesByCohort =
    topRatedBoardGamesTable.Rows
        |> Array.map (fun row -> row.Title, row.``Avg Rating``)
        |> Array.map(fun (title, rating) -> (title, rating, int(rating)))
        |> Array.groupBy(fun (_, _, cohort) -> cohort)
        |> Array.map(fun (cohort, games) -> (cohort, games |> Seq.length))


//Chart.Bar(
//    data=topRatedGamesByCohort, 
//    Labels= (topRatedGamesByCohort |> Seq.map(fun (cohort, nbgames) -> sprintf "%d - %d" cohort nbgames)))