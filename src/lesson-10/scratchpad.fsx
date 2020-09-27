type Address =
    { Street: string
      Town: string
      City: string }


type Customer =
    { Forename: string
      Surname: string
      Age: int
      Address: Address
      EmailAddress: string }

let customer =
    { Forename = "Joe"
      Surname = "Bloggs"
      Age = 30
      Address =
          { Street = "The Street"
            Town = "The Town"
            City = "The City" }
      EmailAddress = "joe@bloggs.com" }


let onelineAddress = { Street = "New Street"; Town = "New Town"; City = "New City" }

let multilineAddress =
    { Street = "New Street"
      Town = "New Town"
      City = "New City" }

onelineAddress = multilineAddress
onelineAddress.Equals multilineAddress
System.Object.ReferenceEquals(onelineAddress, multilineAddress)

let newAddress =
    { Address.Street = "Explicit Street"
      Town = "T"
      City = "C" }

let printAddress address =
    printfn "You live in %s, %s, %s" address.Street address.Town address.City

printAddress multilineAddress

let updatedMultilineAddress =
    { multilineAddress with
          Town = "Updated Town"
          City = "Updated City" }


let ageCustomer customer =
    let randomAge = System.Random().Next(18, 45)
    printfn "Current age is %d" customer.Age
    printfn "New age will be %d" randomAge
    { customer with Age = randomAge }

ageCustomer customer


// Generate record stubs
let newCustomer =
    { Forename = "New Customer"
      Surname = "Not Implemented"
      Age = 12
      Address =
          { Town = "New Town"
            Street = "Not Implemented"
            City = "Not Implemented" }
      EmailAddress = "Not Implemented" }

let myHome =
    { Street = "The Street"
      Town = "The Town"
      City = "The City" }

printAddress myHome

let printer address =
    printAddress address
    // shadowing
    let address = { address with City = "The Other City" }
    printAddress address
    // shadowing
    let address = { address with City = "The Third City" }
    printAddress address

printer myHome
