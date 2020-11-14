namespace Model

type Car =
    { Wheels: int
      Brand: string
      Dimensions: float * float }

[<CLIMutable>]
type CliMutableCar =
    { Wheels: int
      Brand: string }

type Vehicle =
    | Motorcar of Car
    | Motorbike of Name: string * EngineSize: float

module Functions =
    let CreateCar wheels brand x y =
        { Wheels = wheels
          Brand = brand
          Dimensions = x,y}

    let CreateFourWheeledCar = CreateCar 4

    let Partial brand x y = CreateCar 4 brand x y
