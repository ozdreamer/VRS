using System;
using System.Linq;
using Autofac;
using Autofac.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OZD.VRS.DataInterface.Models.Admin;
using OZD.VRS.DataInterface.Models.User;
using OZD.VRS.Repository;
using OZD.VRS.Service;
using OZD.VRS.Service.Interfaces;

namespace OZD.VRS.Test
{
    [TestClass]
    public class DatabaseServiceTest
    {
        /// <summary>
        /// The container.
        /// </summary>
        private IContainer container;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            var builder = new ContainerBuilder();
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .AddEntityFrameworkProxies()
                .BuildServiceProvider();

            var optionBuilder = new DbContextOptionsBuilder<VehicleContext>()
                .UseSqlServer($"Server=.;Database=VRS;Trusted_Connection=True;MultipleActiveResultSets=true")
                .UseInternalServiceProvider(serviceProvider)
                .UseLazyLoadingProxies();

            builder.RegisterInstance(new VehicleContext(optionBuilder.Options));
            builder.RegisterType<EntityFrameworkDatabaseService>().As<IDatabaseService>();
            
            this.container = builder.Build(ContainerBuildOptions.IgnoreStartableComponents);
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            this.container.Dispose();
        }

        /// <summary>
        /// Tests the user credentials.
        /// </summary>
        [TestMethod]
        public void TestUserCredentials()
        {
            var databaseService = this.container.Resolve<IDatabaseService>();

            var user1 = new UserCredential { UserName = "email01@test.com", Password = "pass1" };
            user1 = databaseService.CreateUserCredential(user1);
            var user2 = new UserCredential { UserName = "email02@test.com", Password = "pass2" };
            user2 = databaseService.CreateUserCredential(user2);

            var newUser1 = databaseService.GetUserCredential(user1.UserName);
            Assert.AreEqual(newUser1.Password, user1.Password);

            var newUser2 = databaseService.GetUserCredential(user2.UserName);
            Assert.AreEqual(newUser2.Password, user2.Password);

            newUser2.Password = "pass3";
            newUser2 = databaseService.UpdateUserCredential(newUser2);

            var updatedUser2 = databaseService.GetUserCredential(newUser2.UserName);
            Assert.AreEqual(updatedUser2.Password, newUser2.Password);

            databaseService.DeleteUserCredential(user1.UserName);
            databaseService.DeleteUserCredential(user2.UserName);

            Assert.AreEqual(databaseService.GetUserCredential(user1.UserName), null);
            Assert.AreEqual(databaseService.GetUserCredential(user2.UserName), null);
            Assert.AreEqual(databaseService.GetUserCredential(newUser2.UserName), null);
            Assert.AreEqual(databaseService.GetUserCredential(updatedUser2.UserName), null);
        }

        /// <summary>
        /// Tests the user details.
        /// </summary>
        [TestMethod]
        public void TestUserDetails()
        {
            var databaseService = this.container.Resolve<IDatabaseService>();

            var user = new UserCredential { UserName = "email01@test.com", Password = "pass1" };
            databaseService.CreateUserCredential(user);
            var userDetail = new UserDetail { FirstName = "Test", LastName = "Name", DateOfBirth = new DateTime(1980, 11, 9), AddressLine1 = "58 Portree Cres", AddressCity = "Heathwood", AddressPostCode = "4110", AddressState = "QLD", AddressCountry = "Australia", PrimaryContact = "0411342791", UseAddressAsPostal = true };
            databaseService.CreateUserDetail("email01@test.com", userDetail);

            userDetail = databaseService.GetUserDetail("email01@test.com");
            Assert.AreEqual(userDetail.DateOfBirth, new DateTime(1980, 11, 9));
            Assert.AreEqual(userDetail.AddressCity, "Heathwood");
            Assert.AreEqual($"{userDetail.FirstName} {userDetail.LastName}", "Test Name");
            Assert.IsTrue(userDetail.UseAddressAsPostal);

            userDetail.DateOfBirth = userDetail.DateOfBirth.AddMonths(1);
            userDetail.DateOfBirth = userDetail.DateOfBirth.AddDays(5);
            userDetail.LastName = "Modified";
            databaseService.UpdateUserDetail(userDetail);
            Assert.AreEqual(userDetail.DateOfBirth, new DateTime(1980, 12, 14));
            Assert.AreEqual($"{userDetail.FirstName} {userDetail.LastName}", "Test Modified");

            databaseService.DeleteUserCredential("email01@test.com");
            Assert.AreEqual(databaseService.GetUserDetail("email01@test.com"), null);
            Assert.AreEqual(databaseService.GetUserCredential("email01@test.com"), null);
        }

        /// <summary>
        /// Tests the route between two destinations.
        /// </summary>
        [TestMethod]
        public void TestDestinations()
        {
            var databaseService = this.container.Resolve<IDatabaseService>();

            var destination1 = new Destination { City = "Test Location 1", State = "Test Location 1", PostCode = 1000 };
            destination1 = databaseService.CreateDestination(destination1);
            var destination2 = new Destination { City = "Test Location 2", State = "Test Location 2", PostCode = 6000 };
            destination2 = databaseService.CreateDestination(destination2);

            destination1 = databaseService.GetDestination(destination1.Id);
            Assert.AreEqual(destination1.State, "Test Location 1");
            destination2 = databaseService.GetDestination(destination2.Id);
            Assert.AreEqual(destination2.City, "Test Location 2");
            Assert.AreEqual(destination2.PostCode, 6000);

            destination2.PostCode = 6001;
            destination2.City = "Shalbagan";
            databaseService.UpdateDestination(destination1);

            destination2 = databaseService.GetDestination(destination2.Id);
            Assert.AreEqual(destination2.PostCode, 6001);
            Assert.AreEqual(destination2.City, "Shalbagan");

            databaseService.DeleteDestination(destination1.Id);
            databaseService.DeleteDestination(destination2.Id);
            Assert.AreEqual(databaseService.GetDestination(destination1.Id), null);
            Assert.AreEqual(databaseService.GetDestination(destination2.Id), null);
        }

        /// <summary>
        /// Tests the routes.
        /// </summary>
        [TestMethod]
        public void TestRouteSchedules()
        {
            var databaseService = this.container.Resolve<IDatabaseService>();

            var fleetOperator = new Operator { Name = "Test Operator", AddressLine1 = "58 Portree Cres", AddressCity = "Heathwood", AddressPostCode = "4110", AddressState = "QLD", AddressCountry = "Australia", PrimaryContact = "0411342791", PrimaryEmail = "operator1@email.com" };
            fleetOperator = databaseService.CreateOperator(fleetOperator);
            var destination1 = new Destination { City = "Test Location 1", State = "Test Location 1", PostCode = 1000 };
            destination1 = databaseService.CreateDestination(destination1);
            var destination2 = new Destination { City = "Test Location 2", State = "Test Location 2", PostCode = 6000 };
            destination2 = databaseService.CreateDestination(destination2);

            var routeSchedule = new RouteSchedule { OperatorId = fleetOperator.Id, FromDestinationId = destination1.Id, ToDestinationId = destination2.Id, Day = DayOfWeek.Monday, Time = new TimeSpan(9, 0, 0) };
            routeSchedule = databaseService.CreateRouteSchedule(routeSchedule);

            routeSchedule = databaseService.GetRouteSchedule(routeSchedule.Id);
            Assert.AreEqual(routeSchedule.From?.City, "Test Location 1");
            Assert.AreEqual(routeSchedule.To?.City, "Test Location 2");
            Assert.AreEqual(routeSchedule.Time, new TimeSpan(9, 0, 0));

            routeSchedule.Day = DayOfWeek.Friday;
            routeSchedule.Time = new TimeSpan(11, 0, 0);
            routeSchedule = databaseService.UpdateRouteSchedule(routeSchedule);

            routeSchedule = databaseService.GetRouteSchedule(routeSchedule.Id);
            Assert.AreEqual(routeSchedule.Day, DayOfWeek.Friday);
            Assert.AreEqual(routeSchedule.Time, new TimeSpan(11, 0, 0));

            databaseService.DeleteRouteSchedule(routeSchedule.Id);
            Assert.AreEqual(databaseService.GetRouteSchedule(routeSchedule.Id), null);
        }

        /// <summary>
        /// Tests the operators.
        /// </summary>
        [TestMethod]
        public void TestOperators()
        {
            var databaseService = this.container.Resolve<IDatabaseService>();

            var fleetOperator = new Operator { Name = "Greyhound", AddressLine1 = "58 Portree Cres", AddressCity = "Heathwood", AddressPostCode = "4110", AddressState = "QLD", AddressCountry = "Australia", PrimaryContact = "0411342791", PrimaryEmail = "operator1@email.com" };
            fleetOperator = databaseService.CreateOperator(fleetOperator);

            var newFleetOperator = databaseService.GetOperator(fleetOperator.Id);
            Assert.IsTrue(newFleetOperator != null);
            Assert.AreEqual(newFleetOperator.Name, fleetOperator.Name);
            Assert.AreEqual(newFleetOperator.AddressPostCode, fleetOperator.AddressPostCode);
            Assert.AreEqual(newFleetOperator.PrimaryEmail, fleetOperator.PrimaryEmail);

            newFleetOperator.AddressCity = "Forest Lake";
            newFleetOperator.PrimaryContact = "0433567223";
            newFleetOperator = databaseService.UpdateOperator(newFleetOperator);

            var updatedFleetOperator = databaseService.GetOperator(newFleetOperator.Id);
            Assert.AreEqual(updatedFleetOperator.AddressCity, newFleetOperator.AddressCity);
            Assert.AreEqual(updatedFleetOperator.PrimaryContact, newFleetOperator.PrimaryContact);
            Assert.AreEqual(updatedFleetOperator.AddressLine1, newFleetOperator.AddressLine1);

            databaseService.DeleteOperator(fleetOperator.Id);
            Assert.AreEqual(databaseService.GetOperator(fleetOperator.Id), null);
            Assert.AreEqual(databaseService.GetOperator(newFleetOperator.Id), null);
            Assert.AreEqual(databaseService.GetOperator(updatedFleetOperator.Id), null);
        }

        /// <summary>
        /// Tests the vehicles.
        /// </summary>
        [TestMethod]
        public void TestVehicles()
        {
            var databaseService = this.container.Resolve<IDatabaseService>();

            var vehicle1 = new Vehicle { VehicleType = "Premium Volvo", Manufacturer = "Volvo", DriveType = "Automatic", Model = "V2110", RegistrationNumber = "BA 2233", RegistrationState = "Dhaka Metro", RegistrationExpiry = new DateTime(2021, 9, 12), VIN = "V5263YY83784", Year = 2017, BaseStation = "Dhaka", TotalSeats = 24 };
            vehicle1 = databaseService.CreateVehicle(vehicle1);
            var vehicle2 = new Vehicle { VehicleType = "Non A/C", Manufacturer = "Tata", DriveType = "Manual", Model = "TT334", RegistrationNumber = "BA 1211", RegistrationState = "Dhaka Metro", RegistrationExpiry = new DateTime(2022, 8, 12), VIN = "V5263YY86254", Year = 2017, BaseStation = "Dhaka", TotalSeats = 36 };
            vehicle2 = databaseService.CreateVehicle(vehicle2);

            var newVehicle1 = databaseService.GetVehicle(vehicle1.Id);
            Assert.IsTrue(newVehicle1 != null);
            Assert.AreEqual(newVehicle1.VehicleType, vehicle1.VehicleType);
            Assert.AreEqual(newVehicle1.DriveType, vehicle1.DriveType);
            Assert.AreEqual(newVehicle1.RegistrationExpiry, vehicle1.RegistrationExpiry);
            Assert.AreEqual(newVehicle1.TotalSeats, vehicle1.TotalSeats);

            var newVehicle2 = databaseService.GetVehicle(vehicle2.Id);
            Assert.IsTrue(newVehicle2 != null);
            Assert.AreEqual(newVehicle2.Manufacturer, vehicle2.Manufacturer);
            Assert.AreEqual(newVehicle2.RegistrationNumber, vehicle2.RegistrationNumber);
            Assert.AreEqual(newVehicle2.VIN, vehicle2.VIN);
            Assert.AreEqual(newVehicle2.Year, vehicle2.Year);

            newVehicle1.Model = "VA 4566";
            newVehicle1.BaseStation = "Rajshahi";
            databaseService.UpdateVehicle(newVehicle1);

            var updatedVehicle1 = databaseService.GetVehicle(newVehicle1.Id);
            Assert.IsTrue(updatedVehicle1 != null);
            Assert.AreEqual(updatedVehicle1.Model, newVehicle1.Model);
            Assert.AreEqual(updatedVehicle1.BaseStation, newVehicle1.BaseStation);

            databaseService.DeleteVehicle(vehicle1.Id);
            databaseService.DeleteVehicle(vehicle2.Id);

            Assert.AreEqual(databaseService.GetVehicle(vehicle1.Id), null);
            Assert.AreEqual(databaseService.GetVehicle(newVehicle1.Id), null);
            Assert.AreEqual(databaseService.GetVehicle(updatedVehicle1.Id), null);
            Assert.AreEqual(databaseService.GetVehicle(vehicle2.Id), null);
        }

        /// <summary>
        /// Tests the vehicle schedules.
        /// </summary>
        [TestMethod]
        public void TestVehicleSchedules()
        {
            var databaseService = this.container.Resolve<IDatabaseService>();

            var fleetOperator = new Operator { Name = "Greyhound", AddressLine1 = "58 Portree Cres", AddressCity = "Heathwood", AddressPostCode = "4110", AddressState = "QLD", AddressCountry = "Australia", PrimaryContact = "0411342791", PrimaryEmail = "operator1@email.com" };
            fleetOperator = databaseService.CreateOperator(fleetOperator);

            var destination1 = new Destination { City = "Test Location 1", State = "Test Location 1", PostCode = 1000 };
            destination1 = databaseService.CreateDestination(destination1);
            var destination2 = new Destination { City = "Test Location 2", State = "Test Location 2", PostCode = 6000 };
            destination2 = databaseService.CreateDestination(destination2);

            var routeSchedule = new RouteSchedule { OperatorId = fleetOperator.Id, FromDestinationId = destination1.Id, ToDestinationId = destination2.Id, Day = DayOfWeek.Monday, Time = new TimeSpan(9, 0, 0) };
            routeSchedule = databaseService.CreateRouteSchedule(routeSchedule);

            var vehicle = new Vehicle { VehicleType = "Premium Volvo", Manufacturer = "Volvo", DriveType = "Automatic", Model = "V2110", RegistrationNumber = "BA 2233", RegistrationState = "Dhaka Metro", RegistrationExpiry = new DateTime(2021, 9, 12), VIN = "V5263YY83784", Year = 2017, BaseStation = "Dhaka", TotalSeats = 24 };
            vehicle = databaseService.CreateVehicle(vehicle);

            var vehicleSchedule = new VehicleSchedule { OperatorId = fleetOperator.Id, RouteScheduleId = routeSchedule.Id, VehicleId = vehicle.Id, Date = DateTime.Today };
            vehicleSchedule = databaseService.CreateVehicleSchedule(vehicleSchedule);

            var newVehicleSchedule = databaseService.GetVehicleSchedule(vehicleSchedule.Id);
            Assert.IsTrue(newVehicleSchedule != null);
            Assert.AreEqual(newVehicleSchedule.OperatorId, vehicleSchedule.OperatorId);
            Assert.AreEqual(newVehicleSchedule.RouteScheduleId, vehicleSchedule.RouteScheduleId);
            Assert.AreEqual(newVehicleSchedule.Date, vehicleSchedule.Date);
            Assert.AreEqual(newVehicleSchedule.Operator?.Name, vehicleSchedule.Operator?.Name);

            newVehicleSchedule.Date = new DateTime(2019, 07, 19);
            newVehicleSchedule = databaseService.UpdateVehicleSchedule(newVehicleSchedule);

            var updatedVehicleSchedule = databaseService.GetVehicleSchedule(newVehicleSchedule.Id);
            Assert.IsTrue(updatedVehicleSchedule != null);
            Assert.AreEqual(updatedVehicleSchedule.Date, newVehicleSchedule.Date);

            databaseService.DeleteVehicleSchedule(vehicleSchedule.Id);
            Assert.AreEqual(databaseService.GetVehicleSchedule(vehicleSchedule.Id), null);
            Assert.AreEqual(databaseService.GetVehicleSchedule(newVehicleSchedule.Id), null);
            Assert.AreEqual(databaseService.GetVehicleSchedule(updatedVehicleSchedule.Id), null);

            databaseService.DeleteVehicle(vehicle.Id);
            databaseService.DeleteRouteSchedule(routeSchedule.Id);
            databaseService.DeleteDestination(destination1.Id);
            databaseService.DeleteDestination(destination2.Id);
            databaseService.DeleteOperator(fleetOperator.Id);
        }
    }
}