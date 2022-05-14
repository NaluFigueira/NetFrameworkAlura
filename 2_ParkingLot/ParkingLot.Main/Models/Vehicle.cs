using System;
namespace ParkingLot.Main.Models
{
    public class Vehicle
    {
        private string _licensePlate;
        private string _owner;
        private VehicleType _type;

        public string LicensePlate
        {
            get
            {
                return _licensePlate;
            }
            set
            {
                if (value.Length != 8)
                {
                    throw new FormatException(" License plate should have 8 digits");
                }

                for (int i = 0; i < 3; i++)
                {
                    if (char.IsDigit(value[i]))
                    {
                        throw new FormatException("The 3 first digits should be letters!");
                    }
                }

                if (value[3] != '-')
                {
                    throw new FormatException("The 4th digit should be a hyphen");
                }

                for (int i = 4; i < 8; i++)
                {
                    if (!char.IsDigit(value[i]))
                    {
                        throw new FormatException("Between 5th and 8th digits should be a number!");
                    }
                }
                _licensePlate = value;

            }
        }

        public string Color { get; set; }
        public double Width { get; set; }
        public double Speed { get; set; }
        public string Model { get; set; }
        public string Owner
        {
            get; set;
        }
        public DateTime EntranceTime { get; set; }
        public DateTime ExitTime { get; set; }
        public VehicleType Type
        {
            get { return _type; }
            set
            {
                if (value == null)
                {
                    _type = VehicleType.Car;
                }
                else { _type = value; }
            }
        }

        public void Accelerate(int timeInSeconds)
        {
            this.Speed += (timeInSeconds * 10);
        }

        public void Break(int timeInSeconds)
        {
            this.Speed -= (timeInSeconds * 15);
        }

        public Vehicle()
        {

        }

        public Vehicle(string owner)
        {
            Owner = owner;
        }

        public void UpdateData(Vehicle updatedVehicle)
        {
            this.Color = updatedVehicle.Color;
            this.Model = updatedVehicle.Model;
            this.Owner = updatedVehicle.Owner;
            this.Speed = updatedVehicle.Speed;
            this.Width = updatedVehicle.Width;
            this.Type = updatedVehicle.Type;
        }

        public override string ToString()
        {
            return $"Vehicle's record:\n " +
                   $"Type: {this.Type.ToString()}\n" +
                   $"Owner: {this.Owner}\n" +
                   $"Model: {this.Model}\n" +
                   $"Color: {this.Color}\n" +
                   $"License Plate: {this.LicensePlate}\n";
        }

    }
}
