#load "Domain.fs"
#load "Operations.fs"

open Capstone4.Operations
open Capstone4.Domain
open System

type CreditAccount = CreditAccount of Account

type RatedAccount =
    | InCredit of CreditAccount
    | Overdrawn of Account

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

let myAccount =
    { Balance = 0M
      AccountId = Guid.Empty
      Owner = { Name = "Albert Einstein " } }
    |> classifyBankAccount

myAccount
|> deposit 100M
|> withdrawSafe 200M
|> withdrawSafe 200M
|> deposit 150M
|> withdrawSafe 200M
|> withdrawSafe 200M
|> deposit 150M
|> withdrawSafe 200M
|> withdrawSafe 200M
