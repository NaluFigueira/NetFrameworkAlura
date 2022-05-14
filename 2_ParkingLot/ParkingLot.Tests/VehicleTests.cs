using System;
using ParkingLot.Main.Models;
using Xunit;

namespace ParkingLot.Tests
{
    public class VehicleTests
    {
        [Fact(DisplayName = "Accelerate vehicle")]
        [Trait("Feature", "Accelerate")]
        public void AccelerateVehicleTest()
        {
            //Arrange
            Vehicle vehicle = new Vehicle();

            //Act
            vehicle.Accelerate(10);

            //Assert
            Assert.Equal(100, vehicle.Speed);
        }

        [Fact(DisplayName = "Break vehicle")]
        [Trait("Feature", "Break")]
        public void BreakVihicleTest()
        {
            //Arrange
            Vehicle vehicle = new Vehicle();

            //Act
            vehicle.Break(10);

            //Assert
            Assert.Equal(-150, vehicle.Speed);
        }

        [Fact(DisplayName = "Vehicle type should be car by default")]
        public void TypeVihicleTest()
        {
            //Arrange
            Vehicle vehicle = new Vehicle();

            //Act

            //Assert
            Assert.Equal(VehicleType.Car, vehicle.Type);
        }

        [Fact(Skip = "Method not yet implemented")]
        public void VehicleOwnerNameTest()
        {

        }

        [Theory]
        [InlineData("xxx-9999", "black", "Ana Figueira", "Civic", VehicleType.Car)]
        [InlineData("xxx-9999", "red", "Breno Maia", "Repsol Honda", VehicleType.Motorcycle)]
        public void GetVehicleRecordTest(string licensePlate,
                                         string color,
                                         string owner,
                                         string model,
                                         VehicleType type)
        {
            //Arrange
            var vehicle = new Vehicle();
            vehicle.LicensePlate = licensePlate;
            vehicle.Color = color;
            vehicle.Model = model;
            vehicle.Type = type;
            vehicle.Owner = owner;
            var expectedRecord = $"Vehicle's record:\n " +
                                 $"Type: {type.ToString()}\n" +
                                 $"Owner: {owner}\n" +
                                 $"Model: {model}\n" +
                                 $"Color: {color}\n" +
                                 $"License Plate: {licensePlate}\n";

            //Act
            var vehicleRecord = vehicle.ToString();

            //Assert
            Assert.Contains(expectedRecord, vehicleRecord);

        }
    }
}
