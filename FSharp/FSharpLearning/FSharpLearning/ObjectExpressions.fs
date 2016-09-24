module ObjectExpressions

open Xunit
open Swensen.Unquote

type Order = {Id : int}
type Customer = { Name: string; NumberOfProductsOrdered: int; Orders : Order list }

[<Fact>]
let ``tHIS IS AN ENCHGL``() =
    let john = {Name="John"; NumberOfProductsOrdered=0; Orders = [] }
    let johnAfterOrderingSomething = { john with NumberOfProductsOrdered = 1; Orders= [ {Id = 123} ] }
    let johnAfterARename = {johnAfterOrderingSomething with Name = "Johnny"}
    test <@ johnAfterARename = {Name="Johnny"; NumberOfProductsOrdered=1; Orders = [ {Id = 123} ] } @>

    //Worried about performance hit when "copying" values? Don't. Code above compiles to:
    (*
        ObjectExpressions.Customer func1 = new ObjectExpressions.Customer("John", 0, []);
        ObjectExpressions.Customer func2 = new ObjectExpressions.Customer(func1.Name, 1, [new ObjectExpressions.Order(123)]);
        ObjectExpressions.Customer func3 = new ObjectExpressions.Customer("Johnny", customer.NumberOfProductsOrdered, customer.Orders);
    *)

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