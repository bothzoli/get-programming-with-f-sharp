namespace NugetFSharp

open Newtonsoft.Json

module Library =
    type Person = { Name: string; Age: int }

    let getPerson () =
        let text = """{ "Name": "Sam", "Age": 18 }"""

        let person =
            JsonConvert.DeserializeObject<Person>(text)

        printfn "Name is %s with age %d." person.Name person.Age
        person
