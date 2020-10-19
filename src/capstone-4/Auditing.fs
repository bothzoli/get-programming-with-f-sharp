module Capstone4.Auditing

open Capstone4.Domain

let printTransaction _ accountId transaction =
    printfn
        "Account %O: %s of %M (approved: %b)"
        accountId
        transaction.Operation
        transaction.Amount
        transaction.Accepted

let composedLogger =
    let loggers =
        [ FileRepository.writeTransaction
          printTransaction ]

    fun accountId owner transaction ->
        loggers
        |> List.iter (fun logger -> logger accountId owner transaction)
