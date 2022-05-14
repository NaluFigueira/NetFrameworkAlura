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

        [Fact]
        public void FindVehicleByPlateTest()
        {
            //Arrange
            var yard = new Yard();
            var vehicle = new Vehicle();
            vehicle.LicensePlate = "xxx-9999";

            yard.RegisterVehicleEntrance(vehicle);

            //Act
            var foundVehicle = yard.FindVehicleByPlate(vehicle.LicensePlate);

            //Assert
            Assert.Equal(vehicle.LicensePlate, foundVehicle.LicensePlate);
        }

        [Fact]
        public void UpdateVehicleDataTest()
        {
            //Arrange
            var yard = new Yard();
            var vehicle = new Vehicle();
            vehicle.LicensePlate = "xxx-9999";
            vehicle.Color = "black";

            var updatedVehicle = new Vehicle();
            updatedVehicle.LicensePlate = "xxx-9999";
            updatedVehicle.Color = "green";

            yard.RegisterVehicleEntrance(vehicle);

            //Act
            var result = yard.UpdateVehicleData(vehicle.LicensePlate, updatedVehicle);

            //Assert
            Assert.Equal(result.Color, updatedVehicle.Color);
        }
    }
}
