module AsyncAndParallel

open Xunit
open Swensen.Unquote
open System.Threading.Tasks
open System.Text

//TPL = _potential_ parallellism (tasks instead of threads!)
[<Fact>]
let canUseTPLDataParallellism() =
    let buffer = new StringBuilder()
    let result = Parallel.ForEach([1..10000], fun (i : int) -> buffer.Append(i) |> ignore)
    test <@ buffer.ToString().Contains("1337") @>
