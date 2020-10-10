module Capstone3.FileRepository

open System.IO
open System

open Capstone3.Domain
open Capstone3.Domain.Transaction
open Capstone3.Operations

let private accountsPath =
    let path = @"accounts"
    Directory.CreateDirectory path |> ignore
    path

let private findAccountFolder owner =
    let folders =
        Directory.EnumerateDirectories(accountsPath, sprintf "%s_*" owner)

    if Seq.isEmpty folders then
        ""
    else
        let folder = Seq.head folders
        DirectoryInfo(folder).Name

let private buildPath (owner, accountId: Guid) =
    sprintf @"%s\%s_%O" accountsPath owner accountId

let private getAccountId owner =
    let accountFolder = findAccountFolder owner

    if accountFolder = ""
    then Guid.NewGuid()
    else accountFolder.Split('_').[1] |> Guid.Parse

let private getAccountHistory path =
    if Directory.Exists path then (Directory.GetFiles path) |> Array.toSeq else Seq.empty

let private getTransactions owner =
    buildPath (owner, getAccountId owner)
    |> getAccountHistory
    |> Seq.map (File.ReadAllText >> deserialized)
    |> Seq.filter (fun trx -> trx.Accepted)
    |> Seq.sortBy (fun trx -> trx.Timestamp)

let writeTransaction accountId owner transaction =
    let path = buildPath (owner, accountId)
    path |> Directory.CreateDirectory |> ignore

    let filePath =
        sprintf "%s/%d.txt" path (DateTime.UtcNow.ToFileTimeUtc())

    File.WriteAllText(filePath, serialized transaction)

let loadAccount name =
    let openingAccount =
        { Owner = { Name = name }
          Balance = 0M
          AccountId = getAccountId name }

    getTransactions name
    |> Seq.fold processTransaction openingAccount
