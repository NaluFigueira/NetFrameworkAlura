using System;
using ParkingLot.Main.Models;
using Xunit;

namespace ParkingLot.Tests
{
    public class VehicleTests
    {
        private Vehicle vehicle;

        public VehicleTests()
        {
            vehicle = new Vehicle();

        }

        [Fact(DisplayName = "Should change vehicle speed to 100 if accelerated by 10")]
        [Trait("Vehicle", "Features")]
        public void AccelerateVehicleTest()
        {
            //Arrange

            //Act
            vehicle.Accelerate(10);

            //Assert
            Assert.Equal(100, vehicle.Speed);
        }

        [Fact(DisplayName = "Should change vehicle speed to -150 if broke by 10")]
        [Trait("Vehicle", "Features")]
        public void BreakVihicleTest()
        {
            //Arrange

            //Act
            vehicle.Break(10);

            //Assert
            Assert.Equal(-150, vehicle.Speed);
        }

        [Fact(DisplayName = "Vehicle type should be car by default")]
        [Trait("Vehicle", "Data")]
        public void DefaultVehicleTypeTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.Equal(VehicleType.Car, vehicle.Type);
        }

        [Theory(DisplayName = "Vehicle record should be displayed correctly")]
        [Trait("Vehicle", "Features")]
        [InlineData("xxx-9999", "black", "Ana Figueira", "Civic", VehicleType.Car)]
        [InlineData("xxx-9999", "red", "Breno Maia", "Repsol Honda", VehicleType.Motorcycle)]
        public void GetVehicleRecordTest(string licensePlate,
                                         string color,
                                         string owner,
                                         string model,
                                         VehicleType type)
        {
            //Arrange
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


        
        [Theory(DisplayName = "Should display correct error message when license plate format is incorrect")]
        [Trait("Vehicle", "Data")]
        [InlineData("xxx-999", "License plate should have 8 digits")]
        [InlineData("1xx-9999", "The 3 first digits should be letters!")]
        [InlineData("xxx.9999", "The 4th digit should be a hyphen")]
        [InlineData("xxx-a999", "Between 5th and 8th digits should be a number!")]
        public void LicensePlateWithNoHiphenTest(string plate, string expectedErrorMessage)
        {
            //Arrange

            //Act
            var error = Assert.Throws<System.FormatException>(
                () => new Vehicle().LicensePlate = plate
            );

            //Assert
            Assert.Equal(error.Message, expectedErrorMessage);
        }
    }
}
