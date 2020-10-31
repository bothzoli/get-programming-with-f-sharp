#r @"..\CSharpProject\bin\Debug\netstandard2.0\CSharpProject.dll"

open CSharpProject

let tony = Person "Tony"
tony.PrintName()

let shorthand =
    [ "Tony"
      "Fred"
      "Samantha"
      "Brad"
      "Sophie" ]
    |> List.map (Person >> (fun p -> p.PrintName()))

open System.Collections.Generic

type PersonComparer() =
    interface IComparer<Person> with
        member __.Compare(x, y) = x.Name.CompareTo(y.Name)

// Upcast from PErsonComparer to IComparer<Person> using the :> operator
// Because F# implements interfaces explicitly
let pComparer = PersonComparer() :> IComparer<Person>

pComparer.Compare(tony, Person "Tony")
pComparer.Compare(tony, Person "Toby")

let objectExpressionComparer =
    { new IComparer<Person> with
        member __.Compare(x, y) = x.Name.CompareTo(y.Name) }

objectExpressionComparer.Compare(tony, Person "Tony")
objectExpressionComparer.Compare(tony, Person "Toby")

open System

let blank: string = null
let name = "Vera"
let number = Nullable 10
let blankAsOption = blank |> Option.ofObj
let nameAsOption = name |> Option.ofObj
let numberAsOption = number |> Option.ofNullable
let unsafeBlank = Some blank |> Option.toObj
let unsafeName = Some name |> Option.toObj
let unsafeNumber = Some 10 |> Option.toNullable
