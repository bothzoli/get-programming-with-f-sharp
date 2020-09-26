open System

let describeAge age =
    let ageDescription =
        if age < 18 then "Child!"
        elif age < 65 then "Adult!"
        else "OAP!"

    let greeting = "Hello"
    Console.WriteLine("{0}! You are a '{1}'.", greeting, ageDescription)

describeAge(16)
describeAge(26)
describeAge(88)

let a = ()
let b = ()
a = b

a = describeAge(1)
b = describeAge(1)


let now = DateTime.UtcNow.TimeOfDay.TotalHours

if now < 12.0 then Console.WriteLine "It's morning"
elif now < 18.0 then Console.WriteLine "It's afternoon"
elif now < 20.0 then ignore(5 + 5)
else ()


let getName =
    let fullName = Console.ReadLine()
    Console.WriteLine(fullName.Split(' ').[0])
