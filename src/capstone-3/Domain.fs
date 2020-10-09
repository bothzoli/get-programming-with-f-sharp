namespace Capstone3.Domain

open System

type Customer = { Name: string }

type Account =
    { AccountId: Guid
      Owner: Customer
      Balance: decimal }

type Transaction =
    { Timestamp: DateTime
      Operation: string
      Amount: decimal
      Accepted: bool }

module Transaction =
    let serialized transaction =
        sprintf "%O***%s***%M***%b" transaction.Timestamp transaction.Operation transaction.Amount transaction.Accepted

    let deserialized (transaction: string) =
        let trx =
            transaction.Split("***", StringSplitOptions.None)

        { Timestamp = DateTime.Parse trx.[0]
          Operation = trx.[1]
          Amount = Decimal.Parse trx.[2]
          Accepted = Boolean.Parse trx.[3] }
