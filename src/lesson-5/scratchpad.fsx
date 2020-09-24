let add (a, b) =
    a + b

add("zxcv", "zxcv")

// let getLength name = sprintf "Name is %d letters." name.Length
let getLength (name:string) = sprintf "Name is %d letters." name.Length
let foo(name) = "Hello! " + getLength(name)
foo "asdf"

open System.Collections.Generic
let numbers = List<_>()
numbers.Add(10)
numbers.Add(20)

let otherNumbers = List()
otherNumbers.Add(10)
otherNumbers.Add(20)

let createList(first, second) =
    let output = List()
    output.Add(first)
    output.Add(second)
    output

createList("asdf", "qwer")
createList(1234, 4321)


let sayHello(someValue) =
    let innerFunction(number) =
        if number > 10 then "Isaac"
        elif number > 20 then "Fred"
        else "Sara"

    let resultOfInner =
        if someValue < 10.0 then innerFunction(5)
        else innerFunction(15)

    "Hello " + resultOfInner

let result = sayHello(10.5)


let createDictionary(firstKey, firstValue) =
    let output = Dictionary();
    output.Add(firstKey, firstValue)
    output

createDictionary(1234, 123)
createDictionary("asdf", 123)
createDictionary("asdf", "asdf")
