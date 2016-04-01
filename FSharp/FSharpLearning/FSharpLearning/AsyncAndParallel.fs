module AsyncAndParallel

open Xunit
open Swensen.Unquote
open System.Threading.Tasks
open System.Text

//TPL = _potential_ parallellism (tasks instead of threads but will singlethread if no need)
[<Fact>]
let canUseTPLDataParallellism() =
    //basic scenario, advanced TPL stuff is certainly possible
    let buffer = new StringBuilder()
    let result = Parallel.ForEach([1..10000], fun (i : int) -> buffer.Append(i) |> ignore)
    test <@ buffer.ToString().Contains("1337") @>

[<Fact>]
let canUseTPLTaskParallellism() =
    //basic scenario, advanced TPL stuff is certainly possible
    let buffer = new StringBuilder()
    let appendOperations = 
        [1..10000]
        |> List.map(fun el -> (fun () -> buffer.Append(el.ToString()) |> ignore))
        |> List.map(fun operation -> new System.Action(operation))
        |> Array.ofList

    Parallel.Invoke(appendOperations)

    test <@ buffer.ToString().Contains("1337") @>

//.Net Tasks also work like a charm:
[<Fact>]
let canUseTasks() =
    let buffer = new StringBuilder()

    Task.Factory
        .StartNew(fun () -> 1337) //run a first task
        .ContinueWith(            //on completion of first task, continue with another task
            fun (previous : Task<int>) -> 
                sprintf "continued after %d" previous.Result |> buffer.Append)
        .Wait() //aaand wait for complete result

    test <@ buffer.ToString() = "continued after 1337" @>