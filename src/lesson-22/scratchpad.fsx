let aNumber = 10.0
let maybeANumber = Some 10.0

let calculatePremium score =
    match score with
    | Some s -> sprintf "Score %f!" s
    | None -> sprintf "No score!"

calculatePremium maybeANumber
calculatePremium None

maybeANumber |> Option.map (fun x -> x + 1.0)
None |> Option.map (fun x -> x + 1)

let safeDiv a b: Option<float> = if b = 0.0 then None else Some(a / b)

let score s = if s = 0.0 then None else Some s

safeDiv 4.0 3.0
safeDiv 4.0 0.0

// Functor
(4.0, 2.0) ||> safeDiv |> Option.map score
// Monad
(4.0, 2.0) ||> safeDiv |> Option.bind score

(4.0, 2.0)
||> safeDiv
|> Option.filter (fun x -> x = 2.0)

(4.0, 0.0)
||> safeDiv
|> Option.filter (fun x -> x = 2.0)

(4.0, 2.0) ||> safeDiv |> Option.count

(4.0, 2.0)
||> safeDiv
|> Option.exists (fun x -> x = 2.0)


let tryLoadCustomer customerId =
    match customerId with
    | x when x >= 2 && x <= 7 -> Some(sprintf "Customer %d" x)
    | _ -> None

[ 0 .. 10 ] |> List.choose tryLoadCustomer

let anEmptyList: int list = []
List.tryHead anEmptyList


open System.IO

let tryGetFileInfo path =
    if not (File.Exists path) then None else Some(FileInfo path)

tryGetFileInfo "README.md"
tryGetFileInfo "asdf.md"
