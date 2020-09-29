module Auditing

open Domain

let fileAudit account message =
    System.IO.File.AppendAllText((sprintf "%A.log" account.AccountId), (sprintf "%s\n" message))

let consoleAudit account message =
    System.Console.WriteLine(sprintf "%A: %s" account.AccountId message)
