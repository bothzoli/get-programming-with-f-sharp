#load @".paket\load\netstandard2.1\main.group.fsx"

open System
open FSharp.Data

type PackageVersion =
    | CurrentVersion
    | PreRelease
    | OlderVersion

type VersionDetails = {
    Version: Version
    Downloads: decimal
    PackageVersion: PackageVersion
    LastUpdated: DateTime
}

type NuGetPackage = {
    PackageName: string
    Versions: VersionDetails list
}


type Package = HtmlProvider< @"C:\Users\wd973464\_misc\_src\get-programming-with-f-sharp\src\lesson-33\sample-package.html">

let parseVersion (versionString : string) =
    match versionString.Split '-' |> Array.toList with
    | [ version ] -> Version.Parse version, OlderVersion
    | version :: _ -> Version.Parse version, PreRelease
    | _ -> failwith "Invalid version format"

let getPackages =
    sprintf "https://www.nuget.org/packages/%s"
    >> Package.Load
    >> fun packages -> packages.Tables.``Version History``.Rows
    >> Array.mapi (fun i ver ->
        let (parsedVersion, versionQualifier) = parseVersion ver.Version
        {
            Version = parsedVersion
            Downloads = ver.Downloads
            PackageVersion = if i = 0 then CurrentVersion else versionQualifier
            LastUpdated = ver.``Last updated``
        })

let getDownloadsForPackage =
    getPackages
    >> Seq.sumBy (fun p -> p.Downloads)

let getDetailsForVersion (versionText : string) =
    getPackages
    >> Seq.tryFind (fun p -> p.Version = Version.Parse versionText)

let getDetailsForCurrentVersion =
    getPackages
    >> Seq.tryHead

getDownloadsForPackage "nunit"
getDownloadsForPackage "EntityFramework"
getDownloadsForPackage "Newtonsoft.Json"

getDetailsForVersion "12.0.3" "Newtonsoft.Json"
getDetailsForCurrentVersion "nunit"
getDetailsForCurrentVersion "EntityFramework"
getDetailsForCurrentVersion "Newtonsoft.Json"

getPackages "nunit"
|> Seq.head

type DreamTheaterSongs = HtmlProvider<"https://en.wikipedia.org/wiki/List_of_songs_recorded_by_Dream_Theater">
let songs = DreamTheaterSongs.GetSample()

type Song = {
    Title: string
    Length: string
}

type Album = {
    Name: string
    Year: int
    Songs: Song list
}


songs.Tables.List.Rows
|> List.ofArray
|> List.sortBy (fun s -> s.Album)
|> List.fold (fun albums song ->
    let (currentAlbum, otherAlbums) =
        match albums with
        | [ currentAlbum ] -> currentAlbum, []
        | currentAlbum :: otherAlbums -> currentAlbum, otherAlbums
        | [] -> {
            Name = song.Album
            Year = song.Year
            Songs = List.empty}, []

    if song.Album = currentAlbum.Name
    then
        let updatedCurrentAlbum = { currentAlbum with Songs = { Title = song.Title; Length = song.Length } :: currentAlbum.Songs}
        updatedCurrentAlbum :: otherAlbums
    else
        {
            Name = song.Album
            Year = song.Year
            Songs = [{ Title = song.Title; Length = song.Length }]
        } :: currentAlbum :: otherAlbums ) List.empty<Album>
|> List.sortBy (fun a -> a.Year)