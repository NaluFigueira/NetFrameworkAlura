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

        [Fact(DisplayName = "Should generate vehicle ticket on entrance")]
        [Trait("Yard", "Features")]
        public void GenerateTicketOnVehicleEntranceTest()
        {
            //Arrange

            //Act
            yard.RegisterVehicleEntrance(vehicle);

            //Assert
            Assert.NotNull(vehicle.TicketId);
        }

        [Fact(DisplayName = "Should display header on vehicle ticket")]
        [Trait("Yard", "Features")]
        public void GenerateTicketWithHeaderTest()
        {
            //Arrange

            //Act
            yard.RegisterVehicleEntrance(vehicle);

            //Assert
            Assert.Contains("### Parking lot ticket ###\n", vehicle.Ticket);
        }

        [Fact(DisplayName = "Should display identfier on vehicle ticket")]
        [Trait("Yard", "Features")]
        public void GenerateTicketWithIdentifierTest()
        {
            //Arrange

            //Act
            yard.RegisterVehicleEntrance(vehicle);

            //Assert
            Assert.Contains(">>> Identifier: ", vehicle.Ticket);
        }

        [Fact(DisplayName = "Should display date on vehicle ticket")]
        [Trait("Yard", "Features")]
        public void GenerateTicketWithDateTest()
        {
            //Arrange

            //Act
            yard.RegisterVehicleEntrance(vehicle);

            //Assert
            Assert.Contains($">>> Entrance date/time: {DateTime.Now}\n", vehicle.Ticket);
        }

        [Fact(DisplayName = "Should display license plate on vehicle ticket")]
        [Trait("Yard", "Features")]
        public void GenerateTicketWithLicensePlateTest()
        {
            //Arrange
            vehicle.LicensePlate = "xxx-9999";

            //Act
            yard.RegisterVehicleEntrance(vehicle);

            //Assert
            Assert.Contains($">>> Vehicle license plate: {vehicle.LicensePlate}", vehicle.Ticket);
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
            yard.RegisterVehicleExit(vehicle.TicketId);

            //Act
            double billing = yard.TotalBilled();

            //Assert
            Assert.Equal(2, billing);
        }

        [Fact(DisplayName = "Should find in parking lot vehicle by ticket id")]
        [Trait("Yard", "Features")]
        public void FindVehicleByTicketIdTest()
        {
            //Arrange
            yard.RegisterVehicleEntrance(vehicle);

            //Act
            var foundVehicle = yard.FindVehicleByTicketId(vehicle.TicketId);

            //Assert
            Assert.Equal(vehicle.TicketId, foundVehicle.TicketId);
        }

        [Fact(DisplayName = "Should update vehicle color from black to green")]
        [Trait("Yard", "Features")]
        public void UpdateVehicleColorTest()
        {
            //Arrange
            vehicle.Color = "black";

            var updatedVehicle = new Vehicle();
            updatedVehicle.Color = "green";

            yard.RegisterVehicleEntrance(vehicle);

            //Act
            var result = yard.UpdateVehicleData(vehicle.TicketId, updatedVehicle);

            //Assert
            Assert.Equal(result.Color, updatedVehicle.Color);
        }


    }
}
