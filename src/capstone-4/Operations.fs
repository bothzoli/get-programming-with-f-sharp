module Capstone4.Operations

open System
open Capstone4.Domain

let classifyBankAccount account =
    if account.Balance >= 0M then InCredit(CreditAccount account) else Overdrawn account

let withdraw amount (CreditAccount account) =
    { account with
          Balance = account.Balance - amount }
    |> classifyBankAccount

let deposit amount account =
    let account =
        match account with
        | InCredit (CreditAccount account) -> account
        | Overdrawn account -> account

    { account with
          Balance = account.Balance + amount }
    |> classifyBankAccount

let withdrawSafe amount account =
    match account with
    | InCredit account -> withdraw amount account
    | Overdrawn account ->
        printfn "Your account is overdrawn - withdrawal rejected!"
        Overdrawn account

let auditAs operationName audit operation amount account =
    let updatedAccount = operation amount account

    let transaction =
        { Operation = operationName
          Amount = amount
          Timestamp = DateTime.UtcNow }

    let account =
        match account with
        | InCredit (CreditAccount account) -> account
        | Overdrawn account -> account

    audit account.AccountId account.Owner.Name transaction
    updatedAccount

let loadAccount (owner, accountId, transactions) =
    let openingAccount =
        { AccountId = accountId
          Balance = 0M
          Owner = { Name = owner } }

    transactions
    |> Seq.sortBy (fun txn -> txn.Timestamp)
    |> Seq.fold (fun account txn ->
        match txn.Operation with
        | "withdraw" -> account |> withdrawSafe txn.Amount
        | "deposit" -> account |> deposit txn.Amount
        | _ -> account) (openingAccount |> classifyBankAccount)
