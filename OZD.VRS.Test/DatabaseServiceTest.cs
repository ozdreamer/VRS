using System;
using System.Linq;
using Autofac;
using Autofac.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OZD.VRS.DataInterface.Models;
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
            var user2 = new UserCredential { UserName = "email02@test.com", Password = "pass2" };
            databaseService.CreateUserCredential(user1);
            databaseService.CreateUserCredential(user2);

            user1 = databaseService.GetUserCredential("email01@test.com");
            Assert.AreEqual(user1.Password, "pass1");

            user2 = databaseService.GetUserCredential("email02@test.com");
            Assert.AreEqual(user2.Password, "pass2");

            user2.Password = "pass3";
            databaseService.UpdateUserCredential(user2);

            user2 = databaseService.GetUserCredential("email02@test.com");
            Assert.AreEqual(user2.Password, "pass3");

            databaseService.DeleteUserCredential("email01@test.com");
            databaseService.DeleteUserCredential("email02@test.com");

            Assert.AreEqual(databaseService.GetUserCredential("email01@test.com"), null);
            Assert.AreEqual(databaseService.GetUserCredential("email02@test.com"), null);
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

            var routeSchedule = new RouteSchedule { OperatorId = fleetOperator.Id, FromDestinationId = destination1.Id, ToDestinationId = destination2.Id, Day = "Monday", Time = new TimeSpan(9, 0, 0) };
            routeSchedule = databaseService.CreateRouteSchedule(routeSchedule);

            routeSchedule = databaseService.GetRouteSchedule(routeSchedule.Id);
            Assert.AreEqual(routeSchedule.From?.City, "Test Location 1");
            Assert.AreEqual(routeSchedule.To?.City, "Test Location 2");
            Assert.AreEqual(routeSchedule.Time, new TimeSpan(9, 0, 0));

            routeSchedule.Day = "Friday";
            routeSchedule.Time = new TimeSpan(11, 0, 0);
            routeSchedule = databaseService.UpdateRouteSchedule(routeSchedule);

            routeSchedule = databaseService.GetRouteSchedule(routeSchedule.Id);
            Assert.AreEqual(routeSchedule.Day, "Friday");
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

            fleetOperator = databaseService.GetOperator(fleetOperator.Id);
            Assert.IsTrue(fleetOperator != null);
            Assert.AreEqual(fleetOperator.Name, "Greyhound");
            Assert.AreEqual(fleetOperator.AddressPostCode, "4110");
            Assert.AreEqual(fleetOperator.PrimaryEmail, "operator1@email.com");

            fleetOperator.AddressCity = "Forest Lake";
            fleetOperator.PrimaryContact = "0433567223";
            databaseService.UpdateOperator(fleetOperator);

            fleetOperator = databaseService.GetOperator(fleetOperator.Id);
            Assert.AreEqual(fleetOperator.AddressCity, "Forest Lake");
            Assert.AreEqual(fleetOperator.PrimaryContact, "0433567223");
            Assert.AreEqual(fleetOperator.AddressLine1, "58 Portree Cres");

            databaseService.DeleteOperator(fleetOperator.Id);
            fleetOperator = databaseService.GetOperator(fleetOperator.Id);
            Assert.AreEqual(fleetOperator, null);
        }
    }
}
