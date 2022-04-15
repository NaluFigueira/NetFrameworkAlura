using System;
using ParkingLot.Main.Models;
using Xunit;

namespace ParkingLot.Tests
{
    public class VehicleTests
    {
        [Fact]
        public void AccelerateVehicleTest()
        {
            //Arrange
            Vehicle vehicle = new Vehicle();

            //Act
            vehicle.Accelerate(10);

            //Assert
            Assert.Equal(100, vehicle.Speed);
        }

        [Fact]
        public void BreakVihicleTest()
        {
            //Arrange
            Vehicle vehicle = new Vehicle();

            //Act
            vehicle.Break(10);

            //Assert
            Assert.Equal(-150, vehicle.Speed);
        }

        [Fact]
        public void TypeVihicleTest()
        {
            //Arrange
            Vehicle vehicle = new Vehicle();

            //Act

            //Assert
            Assert.Equal(VehicleType.Car, vehicle.Type);
        }
    }
}
