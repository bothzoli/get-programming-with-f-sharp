type FootballResult =
    { HomeTeam: string
      AwayTeam: string
      HomeGoals: int
      AwayGoals: int }

let create (ht, hg) (at, ag) =
    { HomeTeam = ht
      AwayTeam = at
      HomeGoals = hg
      AwayGoals = ag }

let results =
    [ create ("Messiville", 1) ("Ronaldo City", 2)
      create ("Messiville", 1) ("Bale Town", 3)
      create ("Bale Town", 3) ("Ronaldo City", 1)
      create ("Bale Town", 2) ("Messiville", 1)
      create ("Ronaldo City", 4) ("Messiville", 2)
      create ("Ronaldo City", 1) ("Bale Town", 2) ]

let isAwayWin result = result.AwayGoals > result.HomeGoals

results
|> List.filter isAwayWin
|> List.groupBy (fun result -> result.AwayTeam)
|> List.map (fun group -> (fst group, (snd group).Length))
|> List.sortByDescending snd
|> List.head
|> fst

results
|> List.filter isAwayWin
|> List.countBy (fun result -> result.AwayTeam)
|> List.sortByDescending (fun (_, awayWins) -> awayWins)
|> List.head
|> fst

let numbersArray = [| 1; 2; 3; 4; 6 |]
numbersArray.[0..2]
numbersArray.[4] <- 5
numbersArray.[4]

let numbers = [ 1 .. 6 ]
let head :: tail = numbers
let moreNumbers = 0 :: numbers
let evenMoreNumbers = 0 :: numbers @ [ 7 .. 10 ]
