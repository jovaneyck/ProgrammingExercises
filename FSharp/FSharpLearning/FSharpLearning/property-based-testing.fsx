﻿#r @"..\packages\FsCheck.2.10.4\lib\net452\FsCheck.dll"
open FsCheck

//Simple property
let reversingAReversedList l = List.rev (List.rev l) = l
Check.Quick reversingAReversedList

//Failing property: example of standard shrinking
let reversingAList l = (List.rev l) = l
Check.Quick reversingAList

//Let's play with generators
//Constant values
let one = Gen.constant 1
let size = -1
let nbSamples = 10
let sample = Gen.sample size nbSamples
one |> sample

//Picking a number from within a range
Gen.choose (1,10) |> sample

//Picking from a set of elements
Gen.elements [1;2;3;5;7] |> sample

//Shuffling a sequence
Gen.shuffle [1..5] |> Gen.sample -1 3

//Generating n-tuples
Gen.three (Gen.choose (1,5)) |> Gen.sample -1 2

//Picking from a set of generators
Gen.oneof [ gen{return 1} (*computation expressions supported*); Gen.constant 2 ] |> sample

//Generating more complex types
type Tree = | Branch of Tree * Tree | Leaf of int 
let aTree = Arb.generate<Tree>
aTree |> Gen.sample size nbSamples //Interesting: all leaves when using Arb.generate

let aTreeIsAlwaysALeaf t =
    match t with
    | Leaf _ -> true
    | _ -> false
Check.Quick aTreeIsAlwaysALeaf //As expected: branches get generated by FsCheck

//Transforming generators
Gen.elements ['a'..'z'] |> Gen.map int |> sample

//Can make other generators more interesting/build upon
let aSmallNumber = Gen.choose (1, 10)
Gen.listOf aSmallNumber |> Gen.sample 5 10

//Can filter out unwanted values from a generator
Gen.elements [1..3] 
|> Gen.suchThat (fun x -> x <> 2)
|> sample

//Arbitraries bundle generators and shrinkers for a type
type MyGenerators =
  static member Tree() =
      {new Arbitrary<Tree>() with
          override x.Generator = Gen.constant (Leaf 1337)
          override x.Shrinker t = Seq.empty }
Arb.register<MyGenerators>()
Arb.generate<Tree> |> Gen.sample 3 5
