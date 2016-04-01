//Let's create a calculator that works asynchronously. Using the actor model. Because we can!

type CalculationCommand<'T> =
    | Result of AsyncReplyChannel<'T>
    | Add of 'T
    | Subtract of 'T
    | Clear

type CalculatorAgent = MailboxProcessor<CalculationCommand<float>>

let calculator = 
    CalculatorAgent.Start(fun inbox ->
        let rec processCommand total =
            async {
                let! command = inbox.Receive()
                let newTotal =
                    match command with
                    | Add(value) -> total + value
                    | Subtract(value) -> total - value
                    | Clear -> 0.0
                    | Result(replyChannel) -> 
                        replyChannel.Reply(total)
                        total
                return! processCommand(newTotal)
            }
        processCommand 0.0)

let printResult() =
    calculator.PostAndReply(fun replyChannel -> Result(replyChannel)) |> printfn "Result: %A" //Awaits reply!

printResult()
[ 
    Add 2.0
    Add 3.0
    Subtract 4.0 
]
|> List.iter calculator.Post
printResult()
calculator.Post(Clear)
printResult()