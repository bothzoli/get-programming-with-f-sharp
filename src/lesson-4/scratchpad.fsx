open System

let hello = "Hello F#"
let rand = System.Random

let age = 123
let website = System.Uri "http://fsharp.org"

let addPrint (first, second) =
    let add (first, second) = first + second
    printfn "%d + %d = %d" first second (add(first, second))
    Console.WriteLine("Hello from inside")
    add(first, second) * 2

addPrint(1, 2)
addPrint(11, 22)

let estimatedAge yourBirthYear =
    let age =
        let year = DateTime.Now.Year
        year - yourBirthYear
    sprintf "You are about %d years old!" age

estimatedAge 1984

// let former uri =
//     let browser =
//         let fsharpOrg =
//             let webClient = new WebClient()
//             webClient.DownloadString(Uri uri)
//         new WebBrowser(ScriptErrorsSuppressed = true,
//             Dock = DockStyle.Fill,
//             DocumentText = fsharpOrg)
//     let form = new Form(Text = "Hello from F#!")
//     form.Controls.Add browser
//     form.Show()

let generateRandomNumber max =
    let r = System.Random()
    let nextValue = r.Next(1, max)
    nextValue + 10

generateRandomNumber 12