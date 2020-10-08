open System

open Capstone3.Operations
open Capstone3.Domain
open Capstone3.Auditing
open Capstone3.FileRepository

let isValidCommand char = char = 'd' || char = 'w' || char = 'x'

let isStopCommand char = char = 'x'

let getAmountConsole char =
    Console.Write "\nEnter amount: "
    let amount = Console.ReadLine() |> Decimal.Parse
    (char, amount)

let consoleCommands = seq {
    while true do
        Console.Write "(d)eposit, (w)ithdraw or (e)xit: "
        yield Console.ReadKey().KeyChar
    }

let transactionToOperation transaction =
    (transaction.Operation, transaction.Amount)

[<EntryPoint>]
let main _ =
    let name =
        Console.Write "Please enter your name: "
        Console.ReadLine()

    let withdrawWithAudit = auditAs 'w' composedLogger withdraw
    let depositWithAudit = auditAs 'd' composedLogger deposit

    let processCommand account (command, amount) =
        if command = 'd' then depositWithAudit amount account
        elif command = 'w' then withdrawWithAudit amount account
        else account

    let openingAccount = findTransactionsOnDisk name |> Seq.map transactionToOperation |> Seq.fold processCommand { Owner = { Name = name }; Balance = 0M; AccountId = Guid.Empty }

    let closingAccount =
        consoleCommands
        |> Seq.filter isValidCommand
        |> Seq.takeWhile (not << isStopCommand)
        |> Seq.map getAmountConsole
        |> Seq.fold processCommand openingAccount

    Console.Clear()
    printfn "Closing Balance:\r\n %A" closingAccount
    Console.ReadKey() |> ignore

    0
