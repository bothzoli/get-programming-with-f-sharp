module Capstone4.Program

open System
open Capstone4.Domain
open Capstone4.Operations

let withdrawWithAudit =
    auditAs "withdraw" Auditing.composedLogger withdrawSafe

let depositWithAudit =
    auditAs "deposit" Auditing.composedLogger deposit

let tryLoadAccountFromDisk =
    FileRepository.tryFindTransactionsOnDisk
    >> Option.map loadAccount

[<AutoOpen>]
module CommandParsing =
    let tryParseCommand cmd =
        match cmd with
        | 'd' -> Some(AccountCommand Deposit)
        | 'w' -> Some(AccountCommand Withdraw)
        | 'x' -> Some Exit
        | _ -> None

    let tryGetBankOperation cmd =
        match cmd with
        | AccountCommand op -> Some op
        | Exit -> None

[<AutoOpen>]
module UserInput =
    let commands =
        seq {
            while true do
                Console.Write "(d)eposit, (w)ithdraw or e(x)it: "
                yield Console.ReadKey().KeyChar
                Console.WriteLine()
        }

    let tryGetAmount command =
        Console.WriteLine()
        Console.Write "Enter Amount: "
        let amount = Console.ReadLine() |> Decimal.TryParse
        match amount with
        | true, amount -> Some(command, amount)
        | false, _ -> None

[<EntryPoint>]
let main _ =
    let openingAccount =
        Console.Write "Please enter your name: "
        let owner = Console.ReadLine()
        match owner |> tryLoadAccountFromDisk with
        | Some account -> account
        | None ->
            { AccountId = Guid.NewGuid()
              Balance = 0M
              Owner = { Name = owner } }
            |> classifyBankAccount

    match openingAccount with
    | InCredit (CreditAccount account) -> printfn "Current balance is £%M" account.Balance
    | Overdrawn account -> printfn "Current balance is £%M" account.Balance

    let processCommand account (command, amount) =
        printfn ""

        let account =
            match command with
            | Deposit -> account |> depositWithAudit amount
            | Withdraw -> account |> withdrawWithAudit amount

        let balance =
            match account with
            | InCredit (CreditAccount account) -> account.Balance
            | Overdrawn (account) -> account.Balance

        printfn "Current balance is £%M" balance
        account

    let closingAccount =
        commands
        |> Seq.choose tryParseCommand
        |> Seq.takeWhile ((<>) Exit)
        |> Seq.choose tryGetBankOperation
        |> Seq.choose tryGetAmount
        |> Seq.fold processCommand openingAccount

    printfn ""
    printfn "Closing Balance:\r\n %A" closingAccount
    Console.ReadKey() |> ignore

    0
