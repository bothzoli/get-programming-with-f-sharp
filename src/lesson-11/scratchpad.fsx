open System

let curriedAdd a b = a + b

let add5 = curriedAdd 5
add5 2


let buildDt year month day = DateTime(year, month, day)
let buildDtThisYear = buildDt DateTime.UtcNow.Year
let buildDtThisMonth = buildDtThisYear DateTime.UtcNow.Month

buildDtThisMonth 12


let writeToFile (date: DateTime) filename text =
    let path =
        sprintf "%s_%s.txt" (date.ToString "yyMMdd") filename

    IO.File.WriteAllText(path, text)

let testToday = writeToFile DateTime.Now "test"
testToday "some content"


IO.Directory.GetCurrentDirectory()
|> IO.Directory.GetCreationTime


let petrol = 100.0

let drive distance petrol =
    if distance >= 50 then petrol / 2.0
    elif distance >= 25 then petrol - 10.0
    elif distance >= 0 then petrol - 1.0
    elif distance = 0 then petrol
    else petrol

drive 50 petrol
|> drive 30
|> drive 30
|> drive 10
|> drive 10
|> drive 10


let checkCurrentDirectoryAge =
    IO.Directory.GetCurrentDirectory
    >> IO.Directory.GetCreationTime

checkCurrentDirectoryAge ()
