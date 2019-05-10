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

            var destination1 = new Destination { City = "Dhaka", State = "Dhaka", PostCode = 1000 };
            destination1 = databaseService.CreateDestination(destination1);
            var destination2 = new Destination { City = "Rajshahi", State = "Rajshahi", PostCode = 6000 };
            destination2 = databaseService.CreateDestination(destination2);

            destination1 = databaseService.GetDestination(destination1.Id);
            Assert.AreEqual(destination1.State, "Dhaka");
            destination2 = databaseService.GetDestination(destination2.Id);
            Assert.AreEqual(destination2.City, "Rajshahi");
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

        [TestMethod]
        public void TestRoutes()
        {
            var databaseService = this.container.Resolve<IDatabaseService>();

            var destination1 = new Destination { City = "Dhaka", State = "Dhaka", PostCode = 1000 };
            destination1 = databaseService.CreateDestination(destination1);
            var destination2 = new Destination { City = "Rajshahi", State = "Rajshahi", PostCode = 6000 };
            destination2 = databaseService.CreateDestination(destination2);

            var routes = databaseService.CreateRoute(destination1.Id, destination2.Id);
            Assert.AreEqual(routes.Count, 2);
            Assert.AreEqual(routes.First().FromDestinationId, routes.Last().ToDestinationId);
            Assert.AreEqual(routes.First().ToDestinationId, routes.Last().FromDestinationId);

            var destinationsBySource = databaseService.GetDestinationsBySource(destination1.Id);
            Assert.AreEqual(destinationsBySource.Count, 1);
            Assert.AreEqual(destinationsBySource.First().Id, destination2.Id);

            databaseService.DeleteRoute(routes.First());
            Assert.AreEqual(databaseService.GetRoute(routes.Last().Id), null);

            databaseService.DeleteDestination(destination1.Id);
            databaseService.DeleteDestination(destination2.Id);
            Assert.AreEqual(databaseService.GetDestination(destination1.Id), null);
            Assert.AreEqual(databaseService.GetDestination(destination2.Id), null);
        }
    }
}
