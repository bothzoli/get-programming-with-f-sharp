using System;

using Model;

namespace CSharpApp
{
    public class Program
    {
        public static void Main()
        {
            var car = new Car(4, "Super-Car", Tuple.Create(1.5, 3.5));
            Console.WriteLine(car);
            Console.WriteLine(car.GetType()); // Model.Car
            Console.WriteLine($"Car dimensions are {car.Dimensions.Item1}x{car.Dimensions.Item2}");

            var bike = Vehicle.NewMotorbike("Harley-Davidson", 3.5);
            Console.WriteLine(bike);
            Console.WriteLine(bike.GetType()); // Model.Vehicle+Motorcar
            Console.WriteLine($"Bike is a motorcar = {bike.IsMotorcar}");
            Console.WriteLine($"Bike is a motorbike = {bike.IsMotorbike}");

            var fCar = Functions.CreateCar(4, "FunctionCar", 1.5, 3.5);
            Console.WriteLine(fCar);
            Console.WriteLine(fCar.GetType()); // Model.Car

            var fourWheeledCar = Functions.CreateFourWheeledCar
                .Invoke("4-Wheeled Car")
                .Invoke(1.5)
                .Invoke(3.5);
            Console.WriteLine(fourWheeledCar);
            Console.WriteLine(fourWheeledCar.GetType()); // Model.Car

            var anotherFourWheeledCar = Functions.Partial("Another 4-Wheeled Car", 1.5, 3.5);
            Console.WriteLine(anotherFourWheeledCar);
            Console.WriteLine(anotherFourWheeledCar.GetType()); // Model.Car

            var cliMutableCar = new CliMutableCar();
            Console.WriteLine(cliMutableCar);
            cliMutableCar.Brand = "NewBrand";
            Console.WriteLine(cliMutableCar);
            cliMutableCar.Wheels = 5;
            Console.WriteLine(cliMutableCar);
        }
    }
}
