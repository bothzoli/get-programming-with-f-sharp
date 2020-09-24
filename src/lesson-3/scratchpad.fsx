open System.IO

let text = "Hello F#"
text.Length

let greetPerson name age =
    sprintf "Hello %s, you're %d years old" name age

let createFile filename content =
    File.WriteAllText(filename, content)

let countWords (text:string) =
    let len = text.Split(' ').Length
    let content = sprintf "Text is %s and it is %d words long" text len
    let filename = sprintf "text_%d.txt" len
    createFile filename content
