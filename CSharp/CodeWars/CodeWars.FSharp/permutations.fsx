let input = [1;2;3;4;5]

let permute input =
    let length = input |> List.length
    [0..length]
    |> List.map(fun n ->
            input
            |> List.permute(fun index -> (index + n) % length))

permute input;;