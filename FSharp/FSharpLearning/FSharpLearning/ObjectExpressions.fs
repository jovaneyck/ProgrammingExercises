module ObjectExpressions

open Xunit
open Swensen.Unquote

type Customer = { Name: string; NumberOfProductsOrdered: int }

[<Fact>]
let canCopyAValueAndModifySomePropertiesUsingTheWithKeyword() =
    let john = {Name="John"; NumberOfProductsOrdered=0}

    let afterOrdering = {john with NumberOfProductsOrdered = 2}

    test <@ afterOrdering = {Name="John"; NumberOfProductsOrdered=2} @>

type IPrintable =
    abstract member print : unit -> string

[<Fact>]
let canEasilyImplementInterfacesWithAnAnonymousTypeUsingTheWithKeyword() =
    let printer = {
        new IPrintable 
        with
            member this.print() = "Printed"
        }

    test <@ printer.print() = "Printed" @>