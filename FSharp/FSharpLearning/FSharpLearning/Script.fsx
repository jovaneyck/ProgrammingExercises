type Customer = {Name : string}

let print (c) =
    let { Name = n } = c
    printfn "%s" n

print { Name = "Marty McFly"}

print null
print ()

