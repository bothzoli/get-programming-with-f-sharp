let mutable name = "isaac"
name <- "kate"


let petrol = 100.0

let drive(distance, petrol) =
    if distance > 50 then petrol / 2.0
    elif distance > 25 then petrol - 10.0
    elif distance > 0 then petrol - 1.0
    elif distance = 0 then petrol
    else petrol

drive(40, petrol)
drive(80, petrol)
drive(5, petrol)
drive(0, petrol)
drive(-2, petrol)
