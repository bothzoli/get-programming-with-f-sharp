module Capstone4.Operations

open System
open Capstone4.Domain

let withdraw amount account =
    if amount > account.Balance then
        account
    else
        { account with
              Balance = account.Balance - amount }

let deposit amount account =
    { account with
          Balance = account.Balance + amount }

let auditAs operationName audit operation amount account =
    let updatedAccount = operation amount account

    let transaction =
        { Operation = operationName
          Amount = amount
          Timestamp = DateTime.UtcNow
          Accepted = (updatedAccount <> account) }

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
        | "withdraw" -> account |> withdraw txn.Amount
        | "deposit" -> account |> deposit txn.Amount
        | _ -> account) openingAccount
