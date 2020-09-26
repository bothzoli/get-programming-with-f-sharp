let getDistance (destination) =
    if destination = "Gas" then 10
    elif destination = "Home" then 25
    elif destination = "Stadium" then 25
    elif destination = "Office" then 50
    else failwith "Unknown destination!"

getDistance("Gas") = 10
getDistance("Home") = 25
getDistance("Stadium") = 25
getDistance("Office") = 50


let calculateRemainingPetrol(currentPetrol, distance) =
    if currentPetrol >= distance then currentPetrol - distance
    else failwith "Not enough petrol"

calculateRemainingPetrol(10, 10) = 0
calculateRemainingPetrol(20, 10) = 10
calculateRemainingPetrol(20, getDistance("Gas")) = 10


let driveTo (petrol, destination) =
    calculateRemainingPetrol(petrol, getDistance(destination)) +
        if destination = "Gas" then 50 else 0

driveTo(20, "Gas") = 60
driveTo(50, "Home") = 25
