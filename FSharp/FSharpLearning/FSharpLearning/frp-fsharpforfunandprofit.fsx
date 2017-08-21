open System
//https://fsharpforfunandprofit.com/posts/concurrency-reactive/
(*
    Create two timers, called '3' and '5'. The '3' timer ticks every 300ms and the '5' timer ticks 
    every 500ms. 

    Handle the events as follows:
    a) for all events, print the id of the time and the time
    b) when a tick is simultaneous with a previous tick, print 'FizzBuzz'
    otherwise:
    c) when the '3' timer ticks on its own, print 'Fizz'
    d) when the '5' timer ticks on its own, print 'Buzz'
*)

let createTimerAndObservable timerInterval totalRunningTime =
    let timer = new System.Timers.Timer(float timerInterval)
    timer.AutoReset <- true
    let observable = timer.Elapsed  

    let task = 
        async {
            timer.Start()
            do! Async.Sleep totalRunningTime
            timer.Stop()
        }

    (task,observable)

type FizzBuzzEvent = {label:int; time: DateTime}

let areSimultaneous (earlier, later) =
    let {time = earlierTime} = earlier
    let {time = laterTime} = later
    laterTime.Subtract(earlierTime).Milliseconds < 50

// create the event streams and raw observables
let runningTime = 10000
let timer3, timerEventStream3 = createTimerAndObservable 300 runningTime
let timer5, timerEventStream5 = createTimerAndObservable 500 runningTime

let eventStream3  = 
    timerEventStream3  
    |> Observable.map (fun _ -> {label=3; time=DateTime.Now})
let eventStream5  = 
    timerEventStream5  
    |> Observable.map (fun _ -> {label=5; time=DateTime.Now})

// combine the two streams
let combinedStream = 
   Observable.merge eventStream3 eventStream5
 
// make pairs of events
let pairwiseStream = 
   combinedStream |> Observable.pairwise
 
// split the stream based on whether the pairs are simultaneous
let simultaneousStream, nonSimultaneousStream = 
   pairwiseStream |> Observable.partition areSimultaneous

// split the non-simultaneous stream based on the id
let fizzStream, buzzStream  =
    nonSimultaneousStream  
    // convert pair of events to the first event
    |> Observable.map (fun (ev1,_) -> ev1)
    // split on whether the event id is three
    |> Observable.partition (fun {label=id} -> id=3)

//print events from the combinedStream
combinedStream 
|> Observable.subscribe (fun {label=id;time=t} -> printf "[%i] %i.%03i " id t.Second t.Millisecond)
|> ignore
 
//print events from the simultaneous stream
simultaneousStream 
|> Observable.subscribe (fun _ -> printfn "FizzBuzz")
|> ignore


fizzStream 
|> Observable.subscribe (fun _ -> printfn "Fizz")
|> ignore

buzzStream 
|> Observable.subscribe (fun _ -> printfn "Buzz")
|> ignore

let fizzCount =
    fizzStream 
    |> Observable.scan (fun total _ -> total + 1) 0

// run the two timers at the same time
[timer3;timer5]
|> Async.Parallel
|> Async.RunSynchronously
|> ignore