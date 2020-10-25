namespace Capstone4.Domain

open System

type Customer = { Name: string }

type Account =
    { AccountId: Guid
      Owner: Customer
      Balance: decimal }

type Command =
    | Deposit
    | Withdraw
    | Exit

type Transaction =
    { Timestamp: DateTime
      Operation: string
      Amount: decimal
      Accepted: bool }

module Transactions =
    let serialize transaction =
        sprintf "%O***%s***%M***%b" transaction.Timestamp transaction.Operation transaction.Amount transaction.Accepted

    let deserialize (fileContents: string) =
        let parts =
            fileContents.Split([| "***" |], StringSplitOptions.None)

        { Timestamp = DateTime.Parse parts.[0]
          Operation = parts.[1]
          Amount = Decimal.Parse parts.[2]
          Accepted = Boolean.Parse parts.[3] }
