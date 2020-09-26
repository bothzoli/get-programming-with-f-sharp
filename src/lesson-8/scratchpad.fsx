let petrol = 100

let letsDrive(currentLocation, destination, petrol) =
    let driveTo(destination, petrol) =
        if destination = "Home" && petrol >= 25 then (petrol - 25, "Home")
        elif destination = "Office" &&  petrol >= 50 then (petrol - 50, "Office")
        elif destination = "Stadium" &&  petrol >= 25 then (petrol - 25, "Stadium")
        elif destination = "Gas station" &&  petrol >= 10 then (min (petrol - 10 + 50) 100, "Gas station")
        else petrol, currentLocation
    if currentLocation <> destination then driveTo(destination, petrol)
    else petrol, currentLocation

letsDrive("Home", "Home", petrol)
letsDrive("Home", "Office", petrol)
letsDrive("Home", "Stadium", petrol)
letsDrive("Home", "Gas station", petrol)
