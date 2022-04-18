using System;
using ParkingLot.Main.Models;
using Xunit;

namespace ParkingLot.Tests
{
    public class YardTests
    {
        [Theory]
        [InlineData("Ana Figueira", "abc-1234", "black", "Impala")]
        [InlineData("Cassio Maia", "def-5678", "silver", "Civic")]
        [InlineData("Breno da Cruz", "ghi-9101", "blue", "Jaguar")]
        public void MultipleVehiclesBillingTest(string owner,
                                                string licensePlate,
                                                string color,
                                                string model)
        {
            //Arrange
            var yard = new Yard();
            var vehicle = new Vehicle();
            vehicle.Owner = owner;
            vehicle.LicensePlate = licensePlate;
            vehicle.Color = color;
            vehicle.Model = model;

            yard.RegisterVehicleEntrance(vehicle);
            yard.RegisterVehicleExit(vehicle.LicensePlate);

            //Act
            double billing = yard.TotalBilled();

            //Assert
            Assert.Equal(2, billing);
        }
    }
}
