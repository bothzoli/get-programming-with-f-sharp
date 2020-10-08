#load "Domain.fs"
#load "Operations.fs"
#load "FileRepository.fs"

open System
open System.IO

open Capstone3.Operations
open Capstone3.Domain
open Capstone3.FileRepository

let openingAccount =
    { Owner = { Name = "Isaac" }
      Balance = 0M
      AccountId = Guid.Empty }

let isValidCommand char = char = 'd' || char = 'w' || char = 'x'

isValidCommand 'd'
isValidCommand 'w'
isValidCommand 'x'
isValidCommand 'f'
isValidCommand 'g'


let isStopCommand char = char = 'x'

isStopCommand 'x'
isStopCommand 'd'


let getAmount char =
    if char = 'd' then char, 50M
    elif char = 'w' then char, 25M
    else 'x', 0M

getAmount 'd'
getAmount 'w'
getAmount 'x'
getAmount 'q'

let getAmountConsole char =
    Console.Write "Enter amount: "
    let amount = Console.ReadLine() |> Decimal.Parse
    (char, amount)

getAmountConsole 'd'

let processCommand account (command, amount) =
    if command = 'd' then deposit amount account
    elif command = 'w' then withdraw amount account
    else account

processCommand openingAccount ('w', 100M)


let commands = [ 'd'; 'w'; 'z'; 'f'; 'd'; 'x'; 'w' ]

commands
|> Seq.filter isValidCommand
|> Seq.takeWhile (not << isStopCommand)
|> Seq.map getAmount
|> Seq.fold processCommand openingAccount
