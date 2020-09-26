module Car

let getDistance (destination) =
    if destination = "Gas" then 10
    elif destination = "Home" then 25
    elif destination = "Stadium" then 25
    elif destination = "Office" then 50
    else failwith "Unknown destination!"

let calculateRemainingPetrol(currentPetrol, distance) =
    if currentPetrol >= distance then currentPetrol - distance
    else failwith "Not enough petrol"

let driveTo (petrol, destination) =
    let remainingPetrol = calculateRemainingPetrol(petrol, getDistance(destination))
    if destination = "Gas" then remainingPetrol + 50
    else remainingPetrol
