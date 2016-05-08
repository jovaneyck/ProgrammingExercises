module DiamondProperties

open System
open FsCheck.Xunit

[<Property>]
let ``Diamond is non-empty`` (letter : char) =
    let actual = Diamond.make letter
    not (String.IsNullOrWhiteSpace actual)