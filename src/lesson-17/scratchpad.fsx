open System.Collections.Generic

// let inventory = Dictionary<string, float>()
// let inventory = Dictionary<_, _>()
let inventory = Dictionary()

inventory.Add("Apples", 0.33)
inventory.Add("Oranges", 0.23)
inventory.Add("Bananas", 0.45)
inventory.Remove("Oranges")
let apples = inventory.["Apples"]
let bananas = inventory.["Bananas"]

let immutableInventory: IDictionary<_, _> =
    [ "Apples", 0.33
      "Oranges", 0.23
      "Bananas", 0.45 ]
    |> dict

immutableInventory.Add("Pineapples", 0.85) // Throws exception
immutableInventory.Remove("Bananas") // Throws exception

[ "Apples", 0.33
  "Oranges", 0.23
  "Bananas", 0.45 ]
|> dict
|> Dictionary

let mapInventory =
    [ "Apples", 0.33
      "Oranges", 0.23
      "Bananas", 0.45 ]
    |> Map.ofList

mapInventory.Remove "Apples" // Returns a new map

mapInventory.["Apples"]
mapInventory.["Pineapples"]

let newMapInventory =
    mapInventory
    |> Map.add "Pineapples" 0.87
    |> Map.remove "Apples"

let cheapFruit, expensiveFruit =
    mapInventory
    |> Map.partition (fun fruit cost -> cost < 0.3)

open System.IO

let allDirectories = Directory.EnumerateDirectories "Z:\\"

allDirectories
|> (Seq.map DirectoryInfo
    >> Seq.map (fun d -> d.Name, d.CreationTimeUtc))
|> Seq.map (fun (name, date) -> (name, (System.DateTime.Now - date).Days))
|> Map.ofSeq

let myBasket =
    [ "Apples"
      "Apples"
      "Apples"
      "Bananas"
      "Pineapples" ]

let fruitsILike = myBasket |> Set.ofList
let yourBasket = [ "Kiwi"; "Bananas"; "Grapes" ]

let fruitsYouLike = yourBasket |> Set.ofList
let allFruits = fruitsILike + fruitsYouLike

let fruitsJustForMe = allFruits - fruitsYouLike

let fruitsWeCanShare =
    fruitsILike |> Set.intersect fruitsYouLike

let doILikeAllYourFruits =
    fruitsILike |> Set.isSubset fruitsYouLike
