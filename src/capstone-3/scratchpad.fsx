#load "Domain.fs"
#load "Operations.fs"
#load "FileRepository.fs"

open System
open System.IO

open Capstone3.Operations
open Capstone3.Domain
open Capstone3.Domain.Transaction
open Capstone3.FileRepository

let openingAccount =
    { Owner = { Name = "Isaac" }
      Balance = 0M
      AccountId = Guid.Empty }

let isValidCommand command =
    [ 'd'; 'w'; 'x' ] |> Seq.contains command

isValidCommand 'd'
isValidCommand 'w'
isValidCommand 'x'
isValidCommand 'f'
isValidCommand 'g'


let isStopCommand command = command = 'x'

isStopCommand 'x'
isStopCommand 'd'


let getAmount command =
    if command = 'd' then command, 50M
    elif command = 'w' then command, 25M
    else 'x', 0M

getAmount 'd'
getAmount 'w'
getAmount 'x'
getAmount 'q'

let getAmountConsole command =
    Console.Write "Enter amount: "
    let amount = Console.ReadLine() |> Decimal.Parse
    (command, amount)

getAmountConsole 'd'

let processCommand account (command, amount) =
    if command = 'd' then deposit amount account
    elif command = 'w' then withdraw amount account
    else account

processCommand openingAccount ('d', 80M)


let commands = [ 'd'; 'w'; 'z'; 'f'; 'd'; 'x'; 'w' ]

commands
|> Seq.filter isValidCommand
|> Seq.takeWhile (not << isStopCommand)
|> Seq.map getAmountConsole
|> Seq.fold processCommand openingAccount

let consoleCommands =
    seq {
        while true do
            Console.Write "(d)eposit, (w)ithdraw or e(x)it: "
            yield Console.ReadKey().KeyChar
    }

consoleCommands
|> Seq.filter isValidCommand
|> Seq.takeWhile (not << isStopCommand)
|> Seq.map getAmountConsole
|> Seq.fold processCommand openingAccount

let deposit100 =
    { Timestamp = DateTime.Now.AddDays -2.0
      Operation = "deposit"
      Amount = 100M
      Accepted = true }

let withdraw70 =
    { Timestamp = DateTime.Now.AddDays -1.0
      Operation = "withdraw"
      Amount = 70M
      Accepted = true }

let processTransaction account transaction =
    if transaction.Operation = "deposit" then deposit transaction.Amount account
    elif transaction.Operation = "withdraw" then withdraw transaction.Amount account
    else account

processTransaction openingAccount deposit100

processTransaction openingAccount withdraw70
// let getAccountId owner =
//     let accountFolder = findAccountFolder owner
//     if accountFolder = ""
//     then Guid.NewGuid()
//     else accountFolder.Split('_').[1] |> Guid.Parse
// getAccountId "Sam"
// let getAccountHistory path =
//     if Directory.Exists path then (Directory.GetFiles path) |> Array.toSeq else Seq.empty
// let getTransactions owner =
//     buildPath (owner, getAccountId owner)
//     |> getAccountHistory
//     |> Seq.map (File.ReadAllText >> deserialized)
//     |> Seq.filter (fun trx -> trx.Accepted)
// getTransactions "Sam"
// let loadAccount name =
//     let openingAccount =
//         { Owner = { Name = name }
//           Balance = 0M
//           AccountId = getAccountId name }
//     getTransactions name
//     |> Seq.fold processTransaction openingAccount
// loadAccount "Sam"
