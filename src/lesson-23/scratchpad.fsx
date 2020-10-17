type Customer =
    { CustomerId: string
      Email: string
      Telephone: string
      Address: string }

let createCustomer customerId email telephone address =
    { CustomerId = telephone
      Email = customerId
      Telephone = address
      Address = email }

let customer =
    createCustomer "C-123" "nicki@myemail.com" "029-293-23" "1 The Street"


type Address = Address of string
let myAddress = Address "1 The Street"
let isTheSame = (myAddress = Address "1 The Street")

type CustomerId = CustomerId of string
type Email = Email of string
type Telephone = Telephone of string

type RealCustomer =
    { CustomerId: CustomerId
      Email: Email
      Telephone: Telephone
      Address: Address }

let createRealCustomer customerId email telephone address =
    { CustomerId = CustomerId customerId
      Email = Email email
      Telephone = Telephone telephone
      Address = Address address }

let realCustomer =
    createRealCustomer "C-123" "nicki@myemail.com" "029-293-23" "1 The Street"

type ContactDetails =
    | Address of string
    | Email of string
    | Telephone of string

type AwesomeCustomer =
    { CustomerId: CustomerId
      PrimaryContactDetails: ContactDetails
      SecondaryContactDetails: ContactDetails option }

let createAwesomeCustomer customerId primaryContactDetails secondaryContactDetails =
    { CustomerId = customerId
      PrimaryContactDetails = primaryContactDetails
      SecondaryContactDetails = secondaryContactDetails }

let awesomeCustomer =
    createAwesomeCustomer (CustomerId "C-123") (Address "1 The Street") None

let secondAwesomeCustomer =
    createAwesomeCustomer (CustomerId "C-123") (Address "1 The Street") (Some(Email "awesome@customer.org"))

let thirdAwesomeCustomer =
    createAwesomeCustomer (CustomerId "C-123") (Email "awesome@customer.org") None


type GenuineCustomer = GenuineCustomer of AwesomeCustomer

let validateCustomer customer =
    match customer.PrimaryContactDetails with
    | Email email when email.EndsWith "awesome.org" -> Some(GenuineCustomer customer)
    | Address _
    | Telephone _ -> Some(GenuineCustomer customer)
    | Email _ -> None


validateCustomer awesomeCustomer
validateCustomer secondAwesomeCustomer
validateCustomer thirdAwesomeCustomer

let sendWelcomeEmail (GenuineCustomer customer) = sprintf "Hello %A" customer.CustomerId

validateCustomer awesomeCustomer
|> Option.map sendWelcomeEmail

let insertContact customer =
    match customer.PrimaryContactDetails with
    | Email email when email = "awesome@customer.org" -> Ok "Inserted"
    | _ -> Error "Failed"

insertContact awesomeCustomer
insertContact thirdAwesomeCustomer
