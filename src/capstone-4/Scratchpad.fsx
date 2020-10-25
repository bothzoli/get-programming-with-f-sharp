#load "Domain.fs"
#load "Operations.fs"

open Capstone4.Operations
open Capstone4.Domain
open System

let tryParseCommand cmd =
    match cmd with
    | 'd' -> Some Deposit
    | 'w' -> Some Withdraw
    | 'x' -> Some Exit
    | _ -> None
