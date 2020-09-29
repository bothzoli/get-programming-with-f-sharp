open System

open Domain
open Operations
open Auditing

let getOperation() =
    Console.Write("Enter operation (d/w/x): ")
    Console.ReadLine()

let getAmount() =
    Console.Write("Enter amount: ")
    Decimal.Parse(Console.ReadLine())

[<EntryPoint>]
let main argv =
    Console.Write "Hello from your F# Bank!\n"

    let mutable account =
        Console.Write "Please enter your name: "
        let name = Console.ReadLine()

        Console.Write "Please enter opening balance: "
        let balance = Console.ReadLine() |> Decimal.Parse

        { AccountId = Guid.NewGuid()
          Balance = balance
          Owner = { Name = name } }

    let consoleDeposit = deposit |> accountOperation fileAudit
    let consoleWithdraw = withdraw |> accountOperation fileAudit

    while true do
        try
            let operation = getOperation()
            if operation = "x" then Environment.Exit 0
            let amount = getAmount()

            account <-
                if operation = "d" then account |> consoleDeposit amount
                elif operation = "w" then account |> consoleWithdraw amount
                else account
        with ex -> printfn "ERROR: %s" ex.Message
    0
