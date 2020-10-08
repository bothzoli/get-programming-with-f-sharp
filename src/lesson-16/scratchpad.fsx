let numbers = [ 1 .. 3 ]
let letters = [ "a"; "b"; "c" ]

let square n = n * n

numbers |> List.map square

let combiner n c = sprintf "%d %s" n c
List.map2 combiner numbers letters

List.mapi (fun i n -> sprintf "%d %s" i n) letters

let people =
    [ "Isaac", 30
      "John", 25
      "Sarah", 18
      "Faye", 27 ]

// Execute function for all items
people
|> List.map (fun (name, age) -> sprintf "%s is %d years old" name age)

// Execute an `T -> unit function for all items
people
|> List.iter (fun (name, _) -> printfn "%s" name)

// Execute an `T -> `U list function for all items and merge all lists into one `U list
people
|> List.collect (fun (name, age) -> [ name; age.ToString(); "123" ])


type Order = { OrderId: int }
type Customer = { CustomerId: int; Orders: Order list }

let customers =
    [ { CustomerId = 1
        Orders = [ { OrderId = 1 }; { OrderId = 2 } ] }
      { CustomerId = 2
        Orders = [ { OrderId = 3 } ] } ]

customers |> List.collect (fun c -> c.Orders)


open System

// Create pairs
[ 1 .. 4 ] |> List.pairwise

let dates =
    [ DateTime(2010, 5, 1)
      DateTime(2010, 6, 1)
      DateTime(2010, 6, 12)
      DateTime(2010, 7, 3) ]

dates
|> List.pairwise
|> List.map
    (fun (date1, date2) -> date2 - date1
     >> fun date -> date.Days)

// Creates n-tuples
[ 1 .. 9 ] |> List.windowed 3

type Person = { Name: string; Town: string }

let peopleByTown =
    [ { Name = "Isaac"; Town = "London" }
      { Name = "Sara"; Town = "Manchester" }
      { Name = "Tim"; Town = "Liverpool" }
      { Name = "Michelle"
        Town = "Liverpool" }
      { Name = "John"; Town = "London" }
      { Name = "Paul"; Town = "London" } ]

peopleByTown |> List.groupBy (fun p -> p.Town)

peopleByTown
|> List.countBy (fun p -> p.Town)
|> List.sortByDescending snd

peopleByTown
|> List.partition (fun p -> p.Town = "London")

peopleByTown |> List.splitAt 1

peopleByTown |> List.splitInto 5

peopleByTown |> List.chunkBySize 3


let floatNumbers = [ 1.0 .. 10.0 ]
let total = floatNumbers |> List.sum
let average = floatNumbers |> List.average
let max = floatNumbers |> List.max
let min = floatNumbers |> List.min

peopleByTown
|> List.find (fun p -> p.Town = "London")

peopleByTown |> List.head
peopleByTown |> List.item 2
peopleByTown |> List.take 2

peopleByTown
|> List.exists (fun p -> p.Town = "Liverpool")

peopleByTown
|> List.forall (fun p -> p.Town <> "Paris")

peopleByTown
|> List.contains { Name = "Isaac"; Town = "London" }

peopleByTown
|> List.filter (fun p -> p.Town = "Liverpool")

peopleByTown |> List.length

peopleByTown |> List.distinctBy (fun p -> p.Town)

peopleByTown
|> List.map (fun p -> p.Town)
|> List.distinct

peopleByTown |> List.sort
peopleByTown |> List.sortDescending

let ageComparer p1 p2 = snd p1 - snd p2

let nameComparer p1 p2 =
    if fst p1 < fst p2 then -1
    elif fst p1 > fst p2 then 1
    else 0

people |> List.sortWith ageComparer
people |> List.sortWith nameComparer

peopleByTown |> List.sortBy (fun p -> p.Town)

peopleByTown
|> List.sortByDescending (fun p -> p.Town)


let numberOne =
    [ 1 .. 5 ]
    |> List.toArray
    |> Seq.ofArray
    |> Seq.head

open System.IO

Directory.GetFiles("./") |> Array.map FileInfo

type FileInformation =
    { Name: string
      Length: int64
      DirectoryName: string
      Extension: string }

let isDirectory path =
    File.GetAttributes(path).HasFlag(FileAttributes.Directory)

let getFileInfo path =
    let fileInfo = FileInfo(path)
    [ { Name = fileInfo.Name
        Length = fileInfo.Length
        DirectoryName = fileInfo.DirectoryName
        Extension = fileInfo.Extension } ]

getFileInfo ("./README.md")


let rec getFilesInDirectory path =
    Directory.GetFileSystemEntries(path)
    |> Array.toList
    |> List.collect (fun f -> if f |> isDirectory then f |> getFilesInDirectory else f |> getFileInfo)

let sumFileLength fileList =
    fileList |> List.sumBy (fun f -> f.Length)

getFilesInDirectory ("./src")
|> List.groupBy (fun f -> f.DirectoryName)
|> List.map (fun (dirname, files) -> dirname, files.Length, (files |> sumFileLength))
|> List.sortByDescending (fun (_, _, size) -> size)
|> List.map (fun (dirname, count, size) -> sprintf "%s is %d files %d Bytes" dirname count size)
