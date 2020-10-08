module Capstone3.Operations

open System
open Capstone3.Domain

/// Withdraws an amount of an account (if there are sufficient funds)
let withdraw amount account =
    if amount > account.Balance then
        account
    else
        { account with
              Balance = account.Balance - amount }

/// Deposits an amount into an account
let deposit amount account =
    { account with
          Balance = account.Balance + amount }

/// Runs some account operation such as withdraw or deposit with auditing.
let auditAs operationName audit operation amount account =
    let audit =
        audit account.AccountId account.Owner.Name

    let transaction =
        { Operation = operationName
          Amount = amount
          TimeStamp = DateTime.UtcNow
          IsSuccessful = false }

    let updatedAccount = operation amount account

    let accountIsUnchanged = (updatedAccount = account)

    if accountIsUnchanged
    then audit transaction
    else audit { transaction with IsSuccessful = true }

    updatedAccount

let serialized transaction =
    sprintf "%O***%A***%M***%b" transaction.TimeStamp transaction.Operation transaction.Amount transaction.IsSuccessful

let loadAccount owner accountId transactions = "a"
