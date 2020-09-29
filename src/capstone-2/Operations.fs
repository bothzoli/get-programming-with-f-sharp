module Operations

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

let accountOperation audit operation amount account =
    audit account (sprintf "%O - Current balance: %M" DateTime.Now account.Balance)

    let updatedAccount = operation amount account
    if account.Balance > updatedAccount.Balance then
        audit updatedAccount (sprintf "%O - Withdraw %M, new balance: %M" DateTime.Now amount updatedAccount.Balance)
    elif account.Balance < updatedAccount.Balance then
        audit updatedAccount (sprintf "%O - Deposit %M, new balance: %M" DateTime.Now amount updatedAccount.Balance)
    else
        audit updatedAccount (sprintf "%O - Operation rejected" DateTime.Now)
    updatedAccount
