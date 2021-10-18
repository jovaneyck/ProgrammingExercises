#r "nuget: Unquote"
open Swensen.Unquote

let xs = [1..10]

//Subdividing a list
//By predicate
xs |> List.partition ((<) 5) =! ([6..10], [1..5])

//By sublist size
xs |> List.chunkBySize 4 =! [[1..4];[5..8];[9;10]]

//By sublist count
xs |> List.splitInto 3 =! [[1..4];[5..7];[8..10]]

//At an index
xs |> List.splitAt 5 =! ([1..5],[6..10])
