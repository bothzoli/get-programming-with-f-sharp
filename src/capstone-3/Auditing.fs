module Capstone3.Auditing

open Capstone3.Operations
open Capstone3.Domain
open Capstone3.FileRepository
open Capstone3.Domain.Transaction

let printTransaction _ accountId transaction =
    printfn
        "Account %O: Operation %s %M (Success: %b)"
        accountId
        transaction.Operation
        transaction.Amount
        transaction.Accepted

let composedLogger =
    let loggers = [ writeTransaction; printTransaction ]

    fun accountId owner transaction ->
        loggers
        |> List.iter (fun logger -> logger accountId owner transaction)
