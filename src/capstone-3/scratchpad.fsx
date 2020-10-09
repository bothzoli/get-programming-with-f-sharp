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
