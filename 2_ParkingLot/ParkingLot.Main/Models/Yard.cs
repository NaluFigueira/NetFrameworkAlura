using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingLot.Main.Models
{
    public class Yard
    {
        public Yard()
        {
            billed = 0;
            vehicles = new List<Vehicle>();
        }

        private List<Vehicle> vehicles;
        private double billed;

        public double Billed { get => billed; set => billed = value; }
        public List<Vehicle> Vehicles { get => vehicles; set => vehicles = value; }

        public double TotalBilled()
        {
            return this.Billed;
        }

        public string ShowBilling()
        {
            string totalBilled = String.Format("Total billed up till now :::::::::::::::::::::::::::: {0:c}", this.TotalBilled());
            return totalBilled;
        }

        public void RegisterVehicleEntrance(Vehicle vehicle)
        {
            vehicle.EntranceTime = DateTime.Now;
            GenerateVehicleTicket(vehicle);
            this.Vehicles.Add(vehicle);
        }

        public string RegisterVehicleExit(String licensePlate)
        {
            Vehicle foundVehicle = null;
            string info = string.Empty;

            foreach (Vehicle v in this.Vehicles)
            {
                if (v.TicketId == licensePlate)
                {
                    v.ExitTime = DateTime.Now;

                    TimeSpan lengthOfStay = v.ExitTime - v.EntranceTime;

                    double price = 0;

                    if (v.Type == VehicleType.Car)
                    {
                        price = Math.Ceiling(lengthOfStay.TotalHours) * 2;

                    }

                    if (v.Type == VehicleType.Motorcycle)
                    {
                        price = Math.Ceiling(lengthOfStay.TotalHours) * 1;
                    }

                    info = string.Format(" Entrance time: {0: HH: mm: ss}\n " +
                                             "Exit time: {1: HH:mm:ss}\n " +
                                             "Stay: {2: HH:mm:ss} \n " +
                                             "Price: {3:c}", v.EntranceTime, v.ExitTime, new DateTime().Add(lengthOfStay), price);
                    foundVehicle = v;
                    this.Billed = this.Billed + price;
                    break;
                }

            }

            if (foundVehicle != null)
            {
                this.Vehicles.Remove(foundVehicle);
            }

            else
            {
                return "We didn't find a vehicle with this license plate";
            }

            return info;
        }

        public Vehicle UpdateVehicleData(string ticketId, Vehicle updatedVehicle)
        {
            var vehicleToUpdate = FindVehicleByTicketId(ticketId);

            vehicleToUpdate.UpdateData(updatedVehicle);

            return vehicleToUpdate;
        }

        public Vehicle FindVehicleByTicketId(string ticketId)
        {
            var foundVehicle = this.vehicles
                .FirstOrDefault((vehicle) => vehicle.TicketId == ticketId);
            return foundVehicle;
        }

        public void GenerateVehicleTicket(Vehicle vehicle)
        {
            vehicle.TicketId = new Guid().ToString();
            vehicle.Ticket = $"### Parking lot ticket ###\n" +
                             $">>> Identifier: {vehicle.TicketId}\n" +
                             $">>> Entrance date/time: {DateTime.Now}\n" +
                             $">>> Vehicle license plate: {vehicle.LicensePlate}";
        }
    }
}
