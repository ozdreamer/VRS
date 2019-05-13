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

            var user1 = new UserCredential { UserName = "email01@test.com", Password = "pass1", Active = true };
            user1 = databaseService.CreateUserCredential(user1);
            var user2 = new UserCredential { UserName = "email02@test.com", Password = "pass2", Active = true };
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

            var user = new UserCredential { UserName = "email01@test.com", Password = "pass1", Active = true };
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

            var destination1 = new Destination { City = "Test Location 1", State = "Test Location 1", PostCode = 1000, Active = true };
            destination1 = databaseService.CreateDestination(destination1);
            var destination2 = new Destination { City = "Test Location 2", State = "Test Location 2", PostCode = 6000, Active = true };
            destination2 = databaseService.CreateDestination(destination2);

            var newDestination1 = databaseService.GetDestination(destination1.Id);
            Assert.AreEqual(newDestination1.State, destination1.State);

            var newDestination2 = databaseService.GetDestination(destination2.Id);
            Assert.AreEqual(newDestination2.City, destination2.City);
            Assert.AreEqual(newDestination2.PostCode, destination2.PostCode);

            newDestination1.PostCode = 6001;
            newDestination1.City = "Shalbagan";
            newDestination1 = databaseService.UpdateDestination(newDestination1);

            var updatedDestination1 = databaseService.GetDestination(newDestination1.Id);
            Assert.AreEqual(updatedDestination1.PostCode, newDestination1.PostCode);
            Assert.AreEqual(updatedDestination1.City, newDestination1.City);

            databaseService.DeleteDestination(destination1.Id);
            databaseService.DeleteDestination(destination2.Id);
            Assert.AreEqual(databaseService.GetDestination(destination1.Id), null);
            Assert.AreEqual(databaseService.GetDestination(newDestination1.Id), null);
            Assert.AreEqual(databaseService.GetDestination(updatedDestination1.Id), null);
            Assert.AreEqual(databaseService.GetDestination(destination2.Id), null);
        }

        /// <summary>
        /// Tests the booking offices.
        /// </summary>
        [TestMethod]
        public void TestBookingOffices()
        {
            var databaseService = this.container.Resolve<IDatabaseService>();

            var destination = new Destination { City = "Test Location 1", State = "Test Location 1", PostCode = 1000, Active = true };
            destination = databaseService.CreateDestination(destination);

            var bookingOffice1 = new BookingOffice { AddressLine1 = "100 Magbazar Road", Area = "Magbazar", DestinationId = destination.Id, Email = "test.magbazar@email.com", PrimaryContact = "01711665223", Active = true };
            bookingOffice1 = databaseService.CreateBookingOffice(bookingOffice1);
            var bookingOffice2 = new BookingOffice { AddressLine1 = "5 Mirpur Road", Area = "Mirpur 1", DestinationId = destination.Id, Email = "test.mirpur1@email.com", PrimaryContact = "01711335223", Active = true };
            bookingOffice2 = databaseService.CreateBookingOffice(bookingOffice2);

            var newBookingOffice1 = databaseService.GetBookingOffice(bookingOffice1.Id);
            Assert.IsTrue(newBookingOffice1 != null);
            Assert.AreEqual(newBookingOffice1.AddressLine1, bookingOffice1.AddressLine1);
            Assert.AreEqual(newBookingOffice1.Area, bookingOffice1.Area);
            Assert.AreEqual(newBookingOffice1.SecondaryContact, null);

            newBookingOffice1.Email = "test2.magbazar@email.com";
            newBookingOffice1.PrimaryContact = "0178822334";
            newBookingOffice1.SecondaryContact = "0165515521";
            databaseService.UpdateBookingOffice(newBookingOffice1);

            var updatedBookingOffice1 = databaseService.GetBookingOffice(newBookingOffice1.Id);
            Assert.IsTrue(updatedBookingOffice1 != null);
            Assert.AreEqual(updatedBookingOffice1.Email, newBookingOffice1.Email);
            Assert.AreEqual(updatedBookingOffice1.PrimaryContact, newBookingOffice1.PrimaryContact);
            Assert.AreEqual(updatedBookingOffice1.SecondaryContact, newBookingOffice1.SecondaryContact);

            databaseService.DeleteBookingOffice(bookingOffice1.Id);
            databaseService.DeleteBookingOffice(bookingOffice2.Id);
            Assert.AreEqual(databaseService.GetBookingOffice(bookingOffice1.Id), null);
            Assert.AreEqual(databaseService.GetBookingOffice(newBookingOffice1.Id), null);
            Assert.AreEqual(databaseService.GetBookingOffice(updatedBookingOffice1.Id), null);
            Assert.AreEqual(databaseService.GetBookingOffice(bookingOffice2.Id), null);

            databaseService.DeleteDestination(destination.Id);
        }

        /// <summary>
        /// Tests the routes.
        /// </summary>
        [TestMethod]
        public void TestRouteSchedules()
        {
            var databaseService = this.container.Resolve<IDatabaseService>();

            var fleetOperator = new Operator { Name = "Test Operator", AddressLine1 = "58 Portree Cres", AddressCity = "Heathwood", AddressPostCode = "4110", AddressState = "QLD", AddressCountry = "Australia", PrimaryContact = "0411342791", PrimaryEmail = "operator1@email.com", Active = true };
            fleetOperator = databaseService.CreateOperator(fleetOperator);

            var destination1 = new Destination { City = "Test Location 1", State = "Test Location 1", PostCode = 1000, Active = true };
            destination1 = databaseService.CreateDestination(destination1);
            var destination2 = new Destination { City = "Test Location 2", State = "Test Location 2", PostCode = 6000, Active = true };
            destination2 = databaseService.CreateDestination(destination2);

            var route = new Route { DepartureId = destination1.Id, ArrivalId = destination2.Id, Active = true };
            route = databaseService.CreateRoute(route);

            var routeSchedule = new RouteSchedule { OperatorId = fleetOperator.Id, RouteId = route.Id, Day = DayOfWeek.Monday, Time = new TimeSpan(9, 0, 0), Active = true };
            routeSchedule = databaseService.CreateRouteSchedule(routeSchedule);

            routeSchedule = databaseService.GetRouteSchedule(routeSchedule.Id);
            Assert.AreEqual(routeSchedule.Route.Departure.City, "Test Location 1");
            Assert.AreEqual(routeSchedule.Route.Arrival.City, "Test Location 2");
            Assert.AreEqual(routeSchedule.Time, new TimeSpan(9, 0, 0));

            routeSchedule.Day = DayOfWeek.Friday;
            routeSchedule.Time = new TimeSpan(11, 0, 0);
            routeSchedule = databaseService.UpdateRouteSchedule(routeSchedule);

            routeSchedule = databaseService.GetRouteSchedule(routeSchedule.Id);
            Assert.AreEqual(routeSchedule.Day, DayOfWeek.Friday);
            Assert.AreEqual(routeSchedule.Time, new TimeSpan(11, 0, 0));

            databaseService.DeleteRouteSchedule(routeSchedule.Id);
            Assert.AreEqual(databaseService.GetRouteSchedule(routeSchedule.Id), null);

            databaseService.DeleteRoute(route.Id);
            databaseService.DeleteDestination(destination1.Id);
            databaseService.DeleteDestination(destination2.Id);
            databaseService.DeleteOperator(fleetOperator.Id);
        }

        /// <summary>
        /// Tests the operators.
        /// </summary>
        [TestMethod]
        public void TestOperators()
        {
            var databaseService = this.container.Resolve<IDatabaseService>();

            var fleetOperator = new Operator { Name = "Greyhound", AddressLine1 = "58 Portree Cres", AddressCity = "Heathwood", AddressPostCode = "4110", AddressState = "QLD", AddressCountry = "Australia", PrimaryContact = "0411342791", PrimaryEmail = "operator1@email.com", Active = true };
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
        /// Tests the seat layouts.
        /// </summary>
        [TestMethod]
        public void TestSeatLayouts()
        {
            var databaseService = this.container.Resolve<IDatabaseService>();

            var layout1 = new SeatLayout { Rows = 10, Columns = 4, Layout = "A1:B1;A2:B2;A3,B3:C3,D3", Active = true };
            layout1 = databaseService.CreateSeatLayout(layout1);
            var layout2 = new SeatLayout { Rows = 8, Columns = 4, Layout = "A1,B1:C1,C2;", Active = true };
            layout2 = databaseService.CreateSeatLayout(layout2);

            var newLayout1 = databaseService.GetSeatLayout(layout1.Id);
            Assert.IsTrue(newLayout1 != null);
            Assert.AreEqual(newLayout1.Rows, layout1.Rows);
            Assert.AreEqual(newLayout1.Columns, layout1.Columns);

            newLayout1.Layout = "A1,B1:C1,D1;A2,B2:C2,D2;";
            newLayout1.Active = false;
            newLayout1 = databaseService.UpdateSeatLayout(newLayout1);

            var updatedLayout1 = databaseService.GetSeatLayout(newLayout1.Id);
            Assert.IsTrue(updatedLayout1 != null);
            Assert.AreEqual(updatedLayout1.Layout, newLayout1.Layout);
            Assert.AreEqual(updatedLayout1.Active, newLayout1.Active);
            Assert.AreEqual(updatedLayout1.Rows, newLayout1.Rows);

            databaseService.DeleteSeatLayout(layout1.Id);
            databaseService.DeleteSeatLayout(layout2.Id);
            Assert.AreEqual(databaseService.GetSeatLayout(layout1.Id), null);
            Assert.AreEqual(databaseService.GetSeatLayout(newLayout1.Id), null);
            Assert.AreEqual(databaseService.GetSeatLayout(updatedLayout1.Id), null);
            Assert.AreEqual(databaseService.GetSeatLayout(layout2.Id), null);
        }

        /// <summary>
        /// Tests the vehicles.
        /// </summary>
        [TestMethod]
        public void TestVehicles()
        {
            var databaseService = this.container.Resolve<IDatabaseService>();

            var layout = new SeatLayout { Rows = 10, Columns = 4, Layout = "A1:B1;A2:B2;A3,B3:C3,D3", Active = true };
            layout = databaseService.CreateSeatLayout(layout);

            var vehicle1 = new Vehicle { SeatLayoutId = layout.Id, VehicleType = "Premium Volvo", Manufacturer = "Volvo", DriveType = "Automatic", Model = "V2110", RegistrationNumber = "BA 2233", RegistrationState = "Dhaka Metro", RegistrationExpiry = new DateTime(2021, 9, 12), VIN = "V5263YY83784", Year = 2017, BaseStation = "Dhaka", TotalSeats = 24, Active = true };
            vehicle1 = databaseService.CreateVehicle(vehicle1);
            var vehicle2 = new Vehicle { SeatLayoutId = layout.Id, VehicleType = "Non A/C", Manufacturer = "Tata", DriveType = "Manual", Model = "TT334", RegistrationNumber = "BA 1211", RegistrationState = "Dhaka Metro", RegistrationExpiry = new DateTime(2022, 8, 12), VIN = "V5263YY86254", Year = 2017, BaseStation = "Dhaka", TotalSeats = 36, Active = true };
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

            databaseService.DeleteSeatLayout(layout.Id);
        }

        /// <summary>
        /// Tests the vehicle schedules.
        /// </summary>
        [TestMethod]
        public void TestVehicleSchedules()
        {
            var databaseService = this.container.Resolve<IDatabaseService>();

            var fleetOperator = new Operator { Name = "Greyhound", AddressLine1 = "58 Portree Cres", AddressCity = "Heathwood", AddressPostCode = "4110", AddressState = "QLD", AddressCountry = "Australia", PrimaryContact = "0411342791", PrimaryEmail = "operator1@email.com", Active = true };
            fleetOperator = databaseService.CreateOperator(fleetOperator);

            var destination1 = new Destination { City = "Test Location 1", State = "Test Location 1", PostCode = 1000, Active = true };
            destination1 = databaseService.CreateDestination(destination1);
            var destination2 = new Destination { City = "Test Location 2", State = "Test Location 2", PostCode = 6000, Active = true };
            destination2 = databaseService.CreateDestination(destination2);

            var route = new Route { DepartureId = destination1.Id, ArrivalId = destination2.Id, Active = true };
            route = databaseService.CreateRoute(route);

            var routeSchedule = new RouteSchedule { OperatorId = fleetOperator.Id, RouteId = route.Id, Day = DayOfWeek.Monday, Time = new TimeSpan(9, 0, 0), Active = true };
            routeSchedule = databaseService.CreateRouteSchedule(routeSchedule);

            var layout = new SeatLayout { Rows = 10, Columns = 4, Layout = "A1:B1;A2:B2;A3,B3:C3,D3", Active = true };
            layout = databaseService.CreateSeatLayout(layout);

            var vehicle = new Vehicle { SeatLayoutId = layout.Id, VehicleType = "Premium Volvo", Manufacturer = "Volvo", DriveType = "Automatic", Model = "V2110", RegistrationNumber = "BA 2233", RegistrationState = "Dhaka Metro", RegistrationExpiry = new DateTime(2021, 9, 12), VIN = "V5263YY83784", Year = 2017, BaseStation = "Dhaka", TotalSeats = 24, Active = true };
            vehicle = databaseService.CreateVehicle(vehicle);

            var vehicleSchedule = new VehicleSchedule { OperatorId = fleetOperator.Id, RouteScheduleId = routeSchedule.Id, VehicleId = vehicle.Id, Date = DateTime.Today, Active = true };
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
            databaseService.DeleteSeatLayout(layout.Id);
            databaseService.DeleteRouteSchedule(routeSchedule.Id);
            databaseService.DeleteRoute(route.Id);
            databaseService.DeleteDestination(destination1.Id);
            databaseService.DeleteDestination(destination2.Id);
            databaseService.DeleteOperator(fleetOperator.Id);
        }
    }
}