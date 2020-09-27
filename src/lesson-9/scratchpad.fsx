let parseName (fullName:string) =
    let splitName = fullName.Split ' '
    splitName.[0], splitName.[1]

parseName "Isaac Newton"
let fName, sName = parseName "Albert Einstein"


let parseGame (person:string) =
    let parsedPerson = person.Split ' '
    parsedPerson.[0], parsedPerson.[1], int parsedPerson.[2]

let playername, game, score = parseGame "Mary Asteroids 2500"


let nameAndAge = ("Joe", "Bloggs"), 28
let name, age = nameAndAge
let (forename, surname), theAge = nameAndAge


let (fstName, sndName), _ = nameAndAge
fst nameAndAge
snd nameAndAge


let addNumbers arguments =
    let a, b = arguments
    a + b


let addNumbersGen arguments =
    let a, b, c, _ = arguments
    a + b
