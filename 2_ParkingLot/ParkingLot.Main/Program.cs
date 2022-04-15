using System;
using ParkingLot.Main.Models;

namespace ParkingLot.Main
{
    class MainClass
    {
        static Yard yard = new Yard();

        static void Main(string[] args)
        {
            string option;
            do
            {
                Console.WriteLine(ShowHeader());
                Console.WriteLine(ShowMenu());
                option = ReadMenuOption();
                ProcessMenuOption(option);
                PressKey();
                Console.Clear();
            } while (option != "5");
        }

        static void ShowParkedVehicles()
        {
            Console.Clear();
            Console.WriteLine(" Parked vehicles");

            foreach (Vehicle v in yard.Vehicles)
            {
                Console.WriteLine("License plate :{0}", v.LicensePlate);
                Console.WriteLine("Owner :{0}", v.Owner);
                Console.WriteLine("Entrance time :{0:HH:mm:ss}", v.EntranceTime);
                Console.WriteLine("********************************************");
            }

            if (yard.Vehicles.Count == 0)
            {
                Console.WriteLine("There are no parked vehicles at this time...");
            }

            PressKey();
        }

        static void RegisterVehicleExit()
        {
            Console.Clear();
            Console.WriteLine("Register vehicle exit");
            Console.Write("License plate: ");
            string licensePlate = Console.ReadLine();
            Console.WriteLine(yard.RegisterVehicleExit(licensePlate));
            PressKey();
        }

        static void RegisterVehicleEntrance()
        {
            Console.Clear();
            Console.WriteLine("Register vehicles entrance");
            Console.Write("Vehicle type (1-car; 2-motorcycle) :");
            string type = Console.ReadLine();
            switch (type)
            {
                case "1":
                    RegisterCarEntrance();
                    break;
                case "2":
                    RegisterMotorcycleEntrance();
                    break;
                default:
                    Console.WriteLine("Invalid type");
                    PressKey();
                    break;
            }
        }

        static void RegisterMotorcycleEntrance()
        {
            Console.WriteLine("Motorcycle data");
            Vehicle motorcycle = new Vehicle();
            motorcycle.Type = VehicleType.Motorcycle;

            Console.Write("Type license plate (XXX-9999): ");
            try
            {
                motorcycle.LicensePlate = Console.ReadLine();
            }
            catch (FormatException fe)
            {
                Console.WriteLine("a problem occured: {0}", fe.Message);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return;
            }
            Console.Write("Type motorcycle color: ");
            motorcycle.Color = Console.ReadLine();
            Console.Write("Type owner name: ");
            motorcycle.Owner = Console.ReadLine();
            motorcycle.EntranceTime = DateTime.Now;
            motorcycle.Accelerate(5);
            motorcycle.Break(5);
            yard.RegisterVehicleEntrance(motorcycle);
            Console.WriteLine("Motorcycle successfully registered!");
            Console.ReadKey();
        }

        static void RegisterCarEntrance()
        {
            Console.WriteLine("Car data");
            Vehicle car = new Vehicle();
            car.Type = VehicleType.Car;

            Console.Write("Type license plate (XXX-9999): ");
            try
            {
                car.LicensePlate = Console.ReadLine();
            }
            catch (FormatException fe)
            {
                Console.WriteLine("a problem occured: {0}", fe.Message);
                PressKey();
                return;
            }
            Console.Write("Type car color: ");
            car.Color = Console.ReadLine();
            Console.Write("Type owner name: ");
            car.Owner = Console.ReadLine();
            car.EntranceTime = DateTime.Now;
            car.Accelerate(5);
            car.Break(5);
            yard.RegisterVehicleEntrance(car);
            Console.WriteLine("Car successfully registered!");
        }

        static string ShowHeader()
        {
            return "Parking lot control system";
        }

        static string ReadMenuOption()
        {
            string option;
            Console.Write("Desired option: ");
            option = Console.ReadLine();
            return option;
        }

        static string ShowMenu()
        {
            string menu = "Select an option:\n" +
                            "1 - Register entrance\n" +
                            "2 - Register exit\n" +
                            "3 - Show billed value\n" +
                            "4 - Show parked vehicles\n" +
                            "5 - Exit system \n";
            return menu;
        }

        private static void PressKey()
        {
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        static void ProcessMenuOption(string option)
        {
            switch (option)
            {
                case "1":
                    RegisterVehicleEntrance();
                    break;
                case "2":
                    RegisterVehicleExit();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine(yard.ShowBilling());
                    break;
                case "4":
                    ShowParkedVehicles();
                    break;
                case "5":
                    Console.WriteLine("Thank you for using this system");
                    break;
                default:
                    Console.WriteLine("Invalid menu option!");
                    break;
            }
        }
    }
}
