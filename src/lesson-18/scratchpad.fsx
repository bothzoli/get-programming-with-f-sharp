let sum inputs =
    Seq.fold (fun state input -> state + input) 0 inputs

sum [ 1; 2; 3; 4; 5 ]

let sumLog inputs =
    Seq.fold (fun state input ->
        let newState = state + input
        printfn "Current state is %d, input is %d, new state value is %d" state input newState
        newState) 0 inputs

sumLog [ 1 .. 5 ]


let length inputs =
    Seq.fold (fun state _ -> state + 1) 0 inputs

length [ 1 .. 12 ]


let max inputs =
    Seq.fold (fun state input -> if input > state then input else state) 0 inputs

max [ 1 .. 22 ]

let inputs = [ 1 .. 7 ]

(0, inputs)
||> Seq.fold (fun state input -> state + input)

open System.IO

let lines fileName =
    seq {
        use sr = new StreamReader(File.OpenRead fileName)
        while (not sr.EndOfStream) do
            yield sr.ReadLine()
    }

(0, (lines "src\\lesson-18\\scratchpad.fsx"))
||> Seq.fold (fun total line -> total + line.Length)


open System

type Rule = string -> bool * string

let rules: Rule list =
    [ fun text -> (text.Split ' ').Length = 3, "Must be three words"
      fun text -> text.Length >= 10, "Min length is 10 characters"
      fun text -> text.Length <= 30, "Max length is 30 characters"
      fun text ->
          text
          |> Seq.filter Char.IsLetter
          |> Seq.forall Char.IsUpper,
          "All letters must be caps" ]

let buildValidator (rules: Rule list) =
    rules
    |> List.reduce (fun firstRule secondRule word ->
        let passed, error = firstRule word
        if passed then
            let passed, error = secondRule word
            if passed then true, "" else false, error
        else
            false, error)

let validate = buildValidator rules

validate "HELLO FROM F#"

let myValidator rules word =
    rules
    |> List.fold (fun (passed, error) rule ->
        if passed then
            let passed, error = rule word
            if passed then true, "" else false, error
        else
            false, error) (true, "")

myValidator rules "HELLO FROM F#"

[ 1 .. 10 ] |> List.reduce ((+))

let add1 x = x + 1
let add3 x = x + 3
let times2 x = x * 2

let ops = [ add1; add3; times2; add1 ]

let combi l =
    l |> List.reduce (fun fn1 fn2 i -> fn1 i |> fn2)

combi ops 1
combi ops 2
combi ops 5
