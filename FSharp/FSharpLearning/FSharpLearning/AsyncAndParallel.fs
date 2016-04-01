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
        [1..1000]
        |> List.map(fun el -> (fun () -> buffer.Append(el.ToString()) |> ignore))
        |> List.map(fun operation -> new System.Action(operation))
        |> Array.ofList

    Parallel.Invoke(appendOperations)

    test <@ buffer.ToString().Contains("137") @>

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

//So far for the boring .NET stuff. Let's get FUNCTIONAL!
[<Fact>]
let canUseAsyncComputationExpressions() = 
    let buffer = new StringBuilder()

    //Looks confusing? Check out Computation Expressions!
    async {
        do buffer.Append("1") |> ignore
        do buffer.Append("2") |> ignore
        do! Async.Sleep 10
        do buffer.Append("3") |> ignore
    } |> Async.RunSynchronously

    test <@ buffer.ToString().Contains("3") @>

//Heard of Akka, agent-model for async programming? FSharp has that out of the box!
type Agent<'T> = MailboxProcessor<'T>
type Message = | Message of string

[<Fact>]
let canUseActorModelOutOfTheBox() =
    let buffer = new StringBuilder()
    let agent = 
        Agent<Message>.Start(fun inbox -> 
            let rec loop () =
                async {
                    let! (Message(body)) = inbox.Receive()
                    buffer.Append(body) |> ignore
                    return! loop()
                }
            loop())

    agent.Post(Message "What the hell ")
    agent.Post(Message "World")

    //Wait for the actor to process its inbox entirely
    //NEVER EVER EVER do this in prod code kthx
    while agent.CurrentQueueLength <> 0 do
        System.Threading.Thread.Sleep 1

    test <@ buffer.ToString() = "What the hell World" @>

type ReplyMessage = | ReplyMessage of Message * AsyncReplyChannel<string>

[<Fact>]
let canReceiveRepliesFromActors() = 
    let agent = Agent.Start(fun inbox ->
        let rec loop() =
            async {
                let! (ReplyMessage(Message(message), replyChannel)) = inbox.Receive()
                replyChannel.Reply(sprintf "Echo: %s" message)
                return! loop()
            }
        loop())

    let response = agent.PostAndReply(fun replyChannel -> ReplyMessage(Message("Heya!"), replyChannel))
    test <@ response = "Echo: Heya!" @>