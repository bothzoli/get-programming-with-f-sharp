#load @".paket\load\netstandard2.1\main.group.fsx"

open FSharp.Data
open XPlot.GoogleCharts

type StarWarsApi = JsonProvider<"https://swapi.dev/api/people/">

let people = StarWarsApi.GetSample()

people.Results.[0].Name
people.Results.[0].Homeworld

type Films = HtmlProvider<"https://en.wikipedia.org/wiki/Robert_De_Niro_filmography">

let deNiro = Films.GetSample()

deNiro.Tables.Films.Rows
|> Array.countBy (fun row -> row.Year |> string)
|> Chart.SteppedArea
|> Chart.Show

type NuGetSample = HtmlProvider<"https://raw.githubusercontent.com/isaacabraham/get-programming-fsharp/master/data/sample-package.html">

let nugetPackages = NuGetSample.GetSample()

nugetPackages.Tables.``Version History``

let nunit = NuGetSample.Load "https://www.nuget.org/packages/nunit"
let entityframework = NuGetSample.Load "https://www.nuget.org/packages/entityframework"
let newtonsoft = NuGetSample.Load "https://www.nuget.org/packages/newtonsoft.json"

[nunit; entityframework; newtonsoft]
|> Seq.collect (fun package -> package.Tables.``Version History``.Rows)
|> Seq.sortByDescending (fun package -> package.Downloads)
|> Seq.take 10
|> Seq.map (fun package -> package.Version, package.Downloads)
|> Chart.Column
|> Chart.Show

type DreamTheaterSongs = HtmlProvider<"https://en.wikipedia.org/wiki/List_of_songs_recorded_by_Dream_Theater">
let songs = DreamTheaterSongs.GetSample()

songs.Tables.List.Rows
|> Array.countBy (fun song -> song.Year)
|> Array.map (fun (year, songs) -> year |> string, songs)
|> Array.sortBy fst
|> Chart.Line
|> Chart.Show
