module Capstone3.FileRepository

open System.IO
open System

open Capstone3.Operations
open Capstone3.Domain

let private accountsPath =
    let path = @"accounts"
    Directory.CreateDirectory path |> ignore
    path

let findAccountFolder owner =
    let folders =
        Directory.EnumerateDirectories(accountsPath, sprintf "%s_*" owner)

    if Seq.isEmpty folders then
        ""
    else
        let folder = Seq.head folders
        DirectoryInfo(folder).Name

let buildPath (owner, accountId: Guid) =
    sprintf @"%s\%s_%O" accountsPath owner accountId

/// Logs to the file system
let writeTransaction accountId owner transaction =
    let path = buildPath (owner, accountId)
    path |> Directory.CreateDirectory |> ignore

    let filePath =
        sprintf "%s/%d.txt" path (DateTime.UtcNow.ToFileTimeUtc())

    File.WriteAllText(filePath, serialized transaction)

let private deserialized (transaction: string) =
    let [| timeStamp; operation; amount; success |] = transaction.Split("***")
    { Operation = Char.Parse(operation.Substring(1, 1))
      Amount = Decimal.Parse(amount)
      TimeStamp = DateTime.Parse(timeStamp)
      IsSuccessful = bool.Parse(success) }

let findTransactionsOnDisk owner =
    let folderName = findAccountFolder owner

    if folderName = "" then
        Seq.empty
    else
        let accountId =
            folderName.Split('_') |> Array.tail |> Array.head

        Directory.EnumerateFiles(buildPath (owner, Guid(accountId)), "*.txt")
        |> Seq.map (FileInfo >> (fun x -> x.FullName))
        |> Seq.sort
        |> Seq.map (File.ReadAllText >> deserialized)
