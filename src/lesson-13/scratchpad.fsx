type Customer = { Age: int }

let where filter customers =
    seq {
        for customer in customers do
            if filter customer then yield customer
    }

let customers =
    [ { Age = 21 }
      { Age = 35 }
      { Age = 36 } ]

let isOver age customer = customer.Age > age

let isOver35 = isOver 35

customers |> where (isOver 33)
customers |> where isOver35

customers
|> where (fun customer -> customer.Age > 35)

let getGreeting customer =
    if customer.Age < 18 then "Hello Child"
    elif customer.Age < 65 then "Hello Adult"
    else "Hello Elderly"

let printCustomerAge writer customer = writer (getGreeting customer)

printCustomerAge System.Console.WriteLine customers.[0]

let fileWriter content =
    System.IO.File.WriteAllText("test.txt", content)

printCustomerAge fileWriter customers.[0]

printfn "%s" (System.IO.File.ReadAllText("test.txt"))
