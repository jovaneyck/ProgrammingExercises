#r "../packages/FsCheck.2.2.4/lib/net45/FsCheck.dll"

open FsCheck

let revRevIsOriginal xs = 
    xs = (xs |> List.rev |> List.rev)

Check.Quick revRevIsOriginal