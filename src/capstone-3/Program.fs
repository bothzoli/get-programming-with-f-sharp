module Capstone3.Program

open System

open Capstone3.Domain
open Capstone3.Operations

let isValidCommand command =
    [ 'd'; 'w'; 'x' ] |> Seq.contains command

let isStopCommand command = command = 'x'

let getAmountConsole command =
    Console.Write "\nEnter amount: "
    let amount = Console.ReadLine() |> Decimal.Parse
    (command, amount)

let withdrawWithAudit =
    auditAs "withdraw" Auditing.composedLogger withdraw

let depositWithAudit =
    auditAs "deposit" Auditing.composedLogger deposit

let processCommand account (command, amount) =
    if command = 'd' then depositWithAudit amount account
    elif command = 'w' then withdrawWithAudit amount account
    else account

let consoleCommands =
    seq {
        while true do
            Console.Write "(d)eposit, (w)ithdraw or e(x)it: "
            yield Console.ReadKey().KeyChar
    }


[<EntryPoint>]
let main _ =
    let name =
        Console.Write "Please enter your name: "
        Console.ReadLine()

    let openingAccount =
        { Owner = { Name = name }
          Balance = 0M
          AccountId = Guid() }

    let closingAccount =
        consoleCommands
        |> Seq.filter isValidCommand
        |> Seq.takeWhile (not << isStopCommand)
        |> Seq.map getAmountConsole
        |> Seq.fold processCommand openingAccount

    Console.Clear()
    printfn "Closing Balance for %s (%A):\r\n %M" closingAccount.Owner.Name closingAccount.AccountId
        closingAccount.Balance
    Console.ReadKey() |> ignore

    0
