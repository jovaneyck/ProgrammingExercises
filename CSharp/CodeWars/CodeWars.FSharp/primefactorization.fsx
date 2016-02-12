(*
Sieve of Eratosthenes:
To find all the prime numbers less than or equal to a given integer n by Eratosthenes' method:

Create a list of consecutive integers from 2 through n: (2, 3, 4, ..., n).
Initially, let p equal 2, the smallest prime number.
Enumerate the multiples of p by counting to n from 2p in increments of p, and mark them in the list (these will be 2p, 3p, 4p, ... ; the p itself should not be marked).
Find the first number greater than p in the list that is not marked. If there was no such number, stop. Otherwise, let p now equal this new number (which is the next prime), and repeat from step 3.
*)

let filterMultiples n candidates = 
    candidates |> List.filter (fun el -> el % n <> 0)

let rec findPrimesRec acc candidates =
    match candidates with
    | [] -> acc
    | nextPrime :: t ->
        findPrimesRec (nextPrime :: acc) (candidates |> filterMultiples nextPrime)

let findPrimesSmallerOrEqual n = 
    let possiblePrimes = [2..n]
    let smallestPrime = 2
    
    findPrimesRec [smallestPrime] (possiblePrimes |> filterMultiples smallestPrime)

