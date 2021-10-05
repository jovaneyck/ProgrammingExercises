#r "nuget: Unquote"
open Swensen.Unquote
//Array2D is really interesting when you need to do algebra on matrices (submatrices, vectors, rotating, flipping) because of the advanced slicing support


//Constructing an array2D
let matrix = array2D [[1;2];[3;4]]
let three_by_three = array2D [[1;2;3];[4;5;6];[7;8;9]]
let zeroes : int[,] = Array2D.zeroCreate 2 2
zeroes =! array2D [[0;0];[0;0]]

let ones = Array2D.create 2 2 1
ones =! array2D [[1;1];[1;1]]

//Take care, array2D supports destructive updates!
let updated = Array2D.copy zeroes
updated.[1,1] <- 1337
updated =! array2D [[0;0];[0;1337]]

//Accessing an element
matrix.[1,1] =! 4
//Accessing an entire row vector
matrix.[0,*] =! [|1;2|]
//Accessing an entire column vector
matrix.[*,1] =! [|2;4|]
//Accessing a submatrix
matrix.[0..0,0..0] =! array2D [[1]]

//Accessing a slice from beginning to a specified index
matrix.[0,..0] =! [|1|] 
//Accessing a slice from a specified index to the end
matrix.[0,1..] =! [|2|] 

//Time for some commonly used array2D operations!

///Splits an Array2D into multiple Array2Ds by the given chunk size
let chunkBy (size : int) (m : 'a[,]) : 'a[,][,] = 
    let chunks = (m |> Array2D.length1) / size - 1
    let indices = [for i in 0..chunks -> i * size]
    [ for i in indices do 
        [for j in indices -> 
            m.[i..i+(size-1),j..j+(size-1)]]]
    |> array2D

///Recombines an Array2D of Array2D's into a single Array2D, effectively being the inverse operation of a chunk
let join (m : 'a[,][,]) : 'a[,] = 
    let outerDim = Array2D.length1 m
    let innerDim = Array2D.length1 m.[0,0]
    [for outer in 0..outerDim - 1 do
        for inner in 0..innerDim - 1 ->
            m.[outer,*] |> Seq.collect (fun m -> m.[inner,*])]
    |> array2D

///Rotates an Array2D by 90 degrees ccw
let rotate (m : 'a[,]) = 
    let lastColumn = (m |> Array2D.length1) - 1
    [for c in [lastColumn .. -1 .. 0] -> m.[*,c]]
    |> array2D

///Flips an Array2D on x-axis
let flip (m : 'a[,]) = [for r in [(Array2D.length1 m - 1) .. (-1) .. 0] -> m.[r,*]] |> array2D

///Returns all possible orientations (flips, rotations and flips of rotations) of an Array2D
let orientations (m : 'a[,]) : 'a[,] list = 
    let r1 = rotate m
    let r2 = rotate r1
    let r3 = rotate r2
    [m;r1;r2;r3; flip m; flip r1; flip r2; flip r3]

test <@ chunkBy 2 (array2D [[1;2;3;4]
                            [5;6;7;8]
                            [9;10;11;12]
                            [13;14;15;16]]) = 
    array2D [
                [array2D [[1;2];[5;6]];array2D [[3;4];[7;8]]]
                [array2D [[9;10];[13;14]];array2D [[11;12];[15;16]]]
            ] @>

test <@ join (array2D [
                                [array2D [[1;2];[5;6]];array2D [[3;4];[7;8]]]
                                [array2D [[9;10];[13;14]];array2D [[11;12];[15;16]]]
                            ]) =
                    array2D     [[1;2;3;4]
                                 [5;6;7;8]
                                 [9;10;11;12]
                                 [13;14;15;16]] @>
three_by_three |> chunkBy 1 |> join =! three_by_three

test <@ rotate (array2D [[1;2;3]
                         [4;5;6]
                         [7;8;9]]) =
                (array2D [[3;6;9]
                          [2;5;8]
                          [1;4;7]]) @>
matrix |> rotate |> rotate |> rotate |> rotate =! matrix
array2D [[1;2;3];[4;5;6];[7;8;9]] |> flip =! array2D [[7;8;9];[4;5;6];[1;2;3]]
array2D [[1;2];[3;4]] |> orientations |> Seq.length =! 8 
test <@ array2D [[1;2];[3;4]] |> orientations |> Set.ofSeq = 
            Set.ofSeq [
                array2D [[1;2];[3;4]]
                array2D [[2;4];[1;3]]
                array2D [[4;3];[2;1]]
                array2D [[3;1];[4;2]]
                array2D [[3;4];[1;2]]
                array2D [[2;1];[4;3]]
                array2D [[1;3];[2;4]]
                array2D [[4;2];[3;1]] 
            ] @>