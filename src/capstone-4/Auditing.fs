module Capstone4.Auditing

open Capstone4.Domain

let printTransaction _ accountId transaction =
    printfn "Account %O: %s of %M" accountId transaction.Operation transaction.Amount

let composedLogger =
    let loggers =
        [ FileRepository.writeTransaction
          printTransaction ]

    fun accountId owner transaction ->
        loggers
        |> List.iter (fun logger -> logger accountId owner transaction)
