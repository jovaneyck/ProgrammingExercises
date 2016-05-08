module CsvTypeProvider

open Swensen.Unquote
open Xunit
open FSharp.Data

//Great for rapid data exploration!

type MyDemoSpreadsheetProvider = CsvProvider<Sample="_demo_spreadsheet_.csv",Separators=";">

[<Fact>]
let canReadCsvWithHelpFromTheCompiler() =
    use csv = new MyDemoSpreadsheetProvider()
    let lastRow = 
        query {
            for row in csv.Rows do
            select (row.Name, row.Age) //column headers/types checked by compiler. Wowzerz!
            last
        }
    test <@ ("Bob",37) = lastRow@>