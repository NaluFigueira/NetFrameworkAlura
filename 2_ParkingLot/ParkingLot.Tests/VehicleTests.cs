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
    }
}
