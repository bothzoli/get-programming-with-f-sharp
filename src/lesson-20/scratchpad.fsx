open System
open System.IO

for number in 1 .. 10 do
    printfn "Hello %d" number

for number in 10 .. -1 .. 1 do
    printfn "Hello %d" number

let customerIds = [ 12 .. 17 ]

for customerId in customerIds do
    printfn "Customer ID: %d" customerId

for even in 2 .. 2 .. 20 do
    printfn "Even numbers: %d" even

let reader =
    new StreamReader(File.OpenRead "test.txt")

while (not reader.EndOfStream) do
    printfn "%s" (reader.ReadLine())


let arrayOfChars =
    [| for c in 'a' .. 'z' -> Char.ToUpper c |]

let listOfSquares = [ for i in 1 .. 10 -> i * i * i ]

let seqOfStrings =
    seq { for i in 2 .. 4 .. 20 -> sprintf "Number %d" i }

seqOfStrings |> Seq.map (printfn "%s")


let limit customer =
    match customer with
    | "medium", 1 -> 500
    | "good", 0
    | "good", 1 -> 750
    | "good", 2 -> 1000
    | "good", _ -> 2000
    | _ -> 250

limit ("medium", 0)
limit ("good", 0)
limit ("good", 1)
limit ("good", 2)
limit ("good", 4)


let getCreditLimit customer =
    match customer with
    | "medium", 1 -> 500
    | "good", years when years < 2 -> 750
    | _ -> 250

let getCreditLimitMod customer =
    match customer with
    | "medium", 1 -> 500
    | "good", years ->
        match years with
        | 0
        | 1 -> 750
        | 2 -> 1000
        | _ -> 2000
    | _ -> 250


type Customer = { Name: string; Balance: decimal }

let handleCustomers customers =
    match customers with
    | [] -> failwith "No customers supplied!"
    | [ customer ] -> printfn "Single customer, name is %s" customer.Name
    | [ { Name = "Joe"; Balance = balance }; _ ] -> printfn "Two customers, first is Joe and has %M" balance
    | [ first; second ] -> printfn "Two customers, balance %M" (first.Balance + second.Balance)
    | customers -> printfn "%d customers supplied" customers.Length

handleCustomers []
handleCustomers [ { Name = "Joe"; Balance = 100M } ]

handleCustomers [ { Name = "Joe"; Balance = 100M }
                  { Name = "John"; Balance = 300M } ]

let randomList = [ 1; 3; 51; 32; 4; 89; 9; 49; 52; 78 ]

let checkList list =
    match list with
    | [] -> sprintf "Empty list"
    | head :: _ when head = 1 -> sprintf "Starts with one"
    | head :: _ when head = 3 -> sprintf "Starts with three"
    | other -> sprintf "Has length of %d" other.Length

checkList randomList.Tail
