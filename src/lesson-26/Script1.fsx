#load @".paket\load\netstandard2.0\Humanizer.Core.fsx"
#load @".paket\load\netstandard2.0\Newtonsoft.Json.fsx"
#load "Library.fs"

open Humanizer

"ScriptsAreAGreatWayToExplorePackages".Humanize()

"ScriptsAreAGreatWayToExplorePackages".Humanize(LetterCasing.AllCaps)
"ScriptsAreAGreatWayToExplorePackages".Humanize(LetterCasing.LowerCase)
"ScriptsAreAGreatWayToExplorePackages".Humanize(LetterCasing.Sentence)
"ScriptsAreAGreatWayToExplorePackages".Humanize(LetterCasing.Title)

open NugetFSharp.Library

getPerson ()
