module Capstone3.Operations

open System

open Capstone3.Domain

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
    let audit =
        audit account.AccountId account.Owner.Name

    let updatedAccount = operation amount account

    let accountIsUnchanged = (updatedAccount = account)

    audit
        { Timestamp = DateTime.UtcNow
          Operation = operationName
          Amount = amount
          Accepted = not accountIsUnchanged }

    updatedAccount
