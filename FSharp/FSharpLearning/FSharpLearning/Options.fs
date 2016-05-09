module Options

let customers = 
    [(1, "John"); (2, "Alice")]
    |> Map.ofList

type Option<'a> =
    | Some of 'a
    | Nothing

let getCustomer id =
    if not (customers |> Map.containsKey id) then
        Nothing
    else
        let c = customers |> Map.find id
        Some c

let print c =
    match c with
    | Some name -> printfn "%s" name
    | Nothing -> printfn "No customer found"