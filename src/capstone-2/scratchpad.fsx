#load "Domain.fs"

open System

open Domain

let deposit amount account =
    { account with
          Balance = account.Balance + amount }

let withdraw amount account =
    if account.Balance < amount then
        account
    else
        { account with
              Balance = account.Balance - amount }

let MyAccount =
    { AccountId = Guid.NewGuid()
      Balance = 100M
      Owner = { Name = "Albert Einstein" } }

MyAccount
|> withdraw 50M
|> deposit 20M
|> withdraw 100M

let getTimeStamp =
    let date = DateTime.Now.ToShortDateString()
    let time = DateTime.Now.ToLongTimeString()
    sprintf "%s %s" date time

let fileSystemAudit account message =
    System.IO.File.AppendAllText((sprintf "%A.log" account.AccountId), (sprintf "%s - %s\n" getTimeStamp message))

let console account message =
    System.Console.WriteLine(sprintf "%s - %A: %s" getTimeStamp account.AccountId message)

fileSystemAudit MyAccount "Testing file audit"
console MyAccount "Testing console audit"
console MyAccount

let accountOperation audit operation amount account =
    audit account (sprintf "Current balance: %A" account.Balance)

    let newAccount = operation amount account
    if account.Balance > newAccount.Balance then
        let amount = account.Balance - newAccount.Balance
        audit newAccount (sprintf "Withdraw %A, new balance: %A" amount newAccount.Balance)
    elif account.Balance < newAccount.Balance then
        let amount = newAccount.Balance - account.Balance
        audit newAccount (sprintf "Deposit %A, new balance: %A" amount newAccount.Balance)
    else
        audit newAccount "Operation rejected"
    newAccount

let fileOperation =
    accountOperation fileSystemAudit withdraw

let consoleWithDraw = accountOperation console withdraw


MyAccount |> consoleWithDraw 20M
