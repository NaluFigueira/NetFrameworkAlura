using System;
using ParkingLot.Main.Models;
using Xunit;

namespace ParkingLot.Tests
{ 
    public class YardTests
    {
        private Yard yard;
        private Vehicle vehicle;

        public YardTests()
        {
            yard = new Yard();
            vehicle = new Vehicle();
        }


        [Theory(DisplayName = "Parking lot billing should have correct values")]
        [Trait("Yard", "Features")]
        [InlineData("Ana Figueira", "abc-1234", "black", "Impala")]
        [InlineData("Cassio Maia", "def-5678", "silver", "Civic")]
        [InlineData("Breno da Cruz", "ghi-9101", "blue", "Jaguar")]
        public void MultipleVehiclesBillingTest(string owner,
                                                string licensePlate,
                                                string color,
                                                string model)
        {
            //Arrange
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

        [Fact(DisplayName = "Should find in parking lot vehicle with license plate xxx-9999")]
        [Trait("Yard", "Features")]
        public void FindVehicleByPlateTest()
        {
            //Arrange
            vehicle.LicensePlate = "xxx-9999";

            yard.RegisterVehicleEntrance(vehicle);

            //Act
            var foundVehicle = yard.FindVehicleByPlate(vehicle.LicensePlate);

            //Assert
            Assert.Equal(vehicle.LicensePlate, foundVehicle.LicensePlate);
        }

        [Fact(DisplayName = "Should update vehicle xxx-9999 color from black to green")]
        [Trait("Yard", "Features")]
        public void UpdateVehicleColorTest()
        {
            //Arrange
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
