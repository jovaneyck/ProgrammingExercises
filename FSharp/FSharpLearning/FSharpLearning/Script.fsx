let substitute = function
    | 'A' -> 'N'
    | 'a' -> 'n'
    | 'B' -> 'O'
    | 'b' -> 'o'
    | 'C' -> 'P'
    | 'c' -> 'p'
    | 'D' -> 'Q'
    | 'd' -> 'q'
    | 'E' -> 'R'
    | 'e' -> 'r'
    | 'F' -> 'S'
    | 'f' -> 's'
    | 'G' -> 'T'
    | 'g' -> 't'
    | 'H' -> 'U'
    | 'h' -> 'u'
    | 'I' -> 'V'
    | 'i' -> 'v'
    | 'J' -> 'W'
    | 'j' -> 'w'
    | 'K' -> 'X'
    | 'k' -> 'x'
    | 'L' -> 'Y'
    | 'l' -> 'y'
    | 'M' -> 'Z'
    | 'm' -> 'z'
    | 'N' -> 'A'
    | 'n' -> 'a'
    | 'O' -> 'B'
    | 'o' -> 'b'
    | 'P' -> 'C'
    | 'p' -> 'c'
    | 'Q' -> 'D'
    | 'q' -> 'd'
    | 'R' -> 'E'
    | 'r' -> 'e'
    | 'S' -> 'F'
    | 's' -> 'f'
    | 'T' -> 'G'
    | 't' -> 'g'
    | 'U' -> 'H'
    | 'u' -> 'h'
    | 'V' -> 'I'
    | 'v' -> 'i'
    | 'W' -> 'J'
    | 'w' -> 'j'
    | 'X' -> 'K'
    | 'x' -> 'k'
    | 'Y' -> 'L'
    | 'y' -> 'l'
    | 'Z' -> 'M'
    | 'z' -> 'm'
    | c -> c

let decrypt (secret : string) = 
    let characters = secret.ToCharArray()
    characters
    |> Seq.map substitute
    |> Seq.map (fun c -> c.ToString())
    |> Seq.reduce (+)

decrypt @"ahyivremriraivws ivwsgjrrmrfartramriramrira. TRRA FZF!" 
|> printfn "%A"