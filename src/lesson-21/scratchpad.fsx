type SimpleDisk = { SizeGb: int }

type Computer =
    { Manufacturer: string
      SimpleDisks: SimpleDisk list }

let myPc =
    { Manufacturer = "Computers Inc."
      SimpleDisks =
          [ { SizeGb = 100 }
            { SizeGb = 250 }
            { SizeGb = 500 } ] }

type Disk =
    | HardDrive of RPM: int * Platters: int
    | SSD
    | MMC of Pins: int

let myHardDisk = HardDrive(250, 5)
let mySSD = SSD

let seek disk =
    match disk with
    | HardDrive (rpm, platters) -> sprintf "Seeking HardDrive (%d, %d)" rpm platters
    | SSD -> sprintf "Seeking SSD"
    | MMC pins -> sprintf "Seeking MMC with pins %d" pins

seek myHardDisk
seek mySSD


let describe disk =
    match disk with
    | SSD -> sprintf "I'm a newfangled SSD."
    | MMC 1 -> sprintf "I have only 1 pin."
    | MMC pins when pins < 5 -> sprintf "I'm an MMC with few pins."
    | MMC pins -> sprintf "I'm an MMC with %d pins." pins
    | HardDrive (5400, _) -> sprintf "I'm a slow hard disk."
    | HardDrive (_, 7) -> sprintf "I have 7 spindles!"
    | HardDrive _ -> sprintf "I'm a hard disk."

describe SSD
describe (MMC(1))
describe (MMC(4))
describe (MMC(7))
describe (HardDrive(5400, 7))
describe (HardDrive(7200, 7))
describe (HardDrive(7200, 5))


type MMCDisk =
    | RsMmc
    | MmcPlus
    | SecureMmc

type OtherDisk = MMC of MMCDisk * numberOfPins: int

let seekMMC disk =
    match disk with
    | MMC (MmcPlus, 3) -> "Seeking quitely but slowly"
    | MMC (SecureMmc, 6) -> "Seeking quietly with 6 pins"
    | _ -> "Seeking nothing special"

type DiskInfo =
    { Manufacturer: string
      SizeGb: int
      DiskData: Disk }

type NewComputer =
    { Manufacturer: string
      Disks: DiskInfo list }

let myNewPc =
    { Manufacturer = "Computers Inc."
      Disks =
          [ { Manufacturer = "HardDisks Inc."
              SizeGb = 100
              DiskData = HardDrive(5400, 7) }
            { Manufacturer = "SuperDisks Corp."
              SizeGb = 250
              DiskData = SSD } ] }

printfn "%A" myNewPc

let (|Even|Odd|) input = if input % 2 = 0 then Even else Odd

let testNumber input =
    match input with
    | Even -> sprintf "%d is even" input
    | Odd -> sprintf "%d is odd" input

testNumber 2
testNumber 3

type Printer =
    | Inkjet = 0
    | Laserjet = 1
    | DotMatrix = 2

let testPrinter printer =
    match printer with
    | Printer.Inkjet -> sprintf "It's an inkjet"
    | Printer.Laserjet -> sprintf "It's a laserjet"
    | Printer.DotMatrix -> sprintf "It's a dotmatrix"
    | _ -> sprintf "It's a printer"

testPrinter Printer.Inkjet


open System

type Result =
    | Success
    | Error of errorMessage: string

type Rule = string -> Result

let check3words (text: string) = (text.Split ' ').Length = 3

let checkMinLength (text: string) = text.Length >= 10

let checkMaxLength (text: string) = text.Length <= 30

let checkAllCaps (text: string) =
    text
    |> Seq.filter Char.IsLetter
    |> Seq.forall Char.IsUpper

let checkRule (predicate, errorMessage) (input: string) =
    if predicate input then Success else Error errorMessage

let rules: Rule list =
    [ checkRule (check3words, "Must be three words")
      checkRule (checkMinLength, "Min length is 10 characters")
      checkRule (checkMaxLength, "Max length is 30 characters")
      checkRule (checkAllCaps, "All letters must be all caps") ]

let buildValidator (rules: Rule list) =
    rules
    |> List.reduce (fun firstRule secondRule word ->
        let firstResult =
            match firstRule word with
            | Error errorMessage -> Error errorMessage
            | Success -> Success

        match firstResult with
        | Error errorMessage -> Error errorMessage
        | Success -> secondRule word)

let myValidator rules word =
    rules
    |> List.fold (fun result rule ->
        match result with
        | Success -> rule word
        | Error errorMessage -> Error errorMessage) Success

let validate = buildValidator rules
let myValidate = myValidator rules

myValidate "HELLO FROM"
myValidate "HELLOHELLOHELLOHELLOHELLO FROM F#"
myValidate "Hello From F#"
myValidate "HELLO FROM F#"
