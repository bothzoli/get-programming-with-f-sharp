#load @".paket\load\netcoreapp3.1\main.group.fsx"

open FSharp.Data
open XPlot.GoogleCharts

type Football = CsvProvider< @"C:\Users\wd973464\_misc\_src\get-programming-with-f-sharp\src\lesson-30\data\FootballResults.csv">

let data = Football.GetSample().Rows |> Seq.toArray


data
|> Seq.filter(fun row -> row.``Full Time Home Goals`` > row.``Full Time Away Goals``)
|> Seq.countBy(fun row -> row.``Home Team``)
|> Seq.sortByDescending snd
|> Seq.take 10
|> Chart.Column
|> Chart.Show