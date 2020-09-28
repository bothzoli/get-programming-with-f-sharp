open Domain
open Operations
open Calculator

[<EntryPoint>]
let main argv =
    let joe = {
        FirstName = "Joe"
        LastName = "Blogs"
        Age = 21
    }

    if joe |> isOlderThan 18 then printfn "%s is an adult" joe.FirstName
    else printfn "%s is a child" joe.FirstName

    printfn "%d + %d = %d" 1 2 (add 1 2)
    printfn "%d * %d = %d" 1 2 (Extra.multiply 1 2)
    0 // return an integer exit code
