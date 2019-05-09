using System;
using System.IO;
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

            var user1 = new UserCredential { UserName = "ozdreamer.it@gmail.com", Password = "pass1" };
            var user2 = new UserCredential { UserName = "k.h.rajeeb@gmail.com", Password = "pass2" };
            databaseService.CreateUserCredential(user1);
            databaseService.CreateUserCredential(user2);

            user1 = databaseService.GetUserCredential("ozdreamer.it@gmail.com");
            Assert.AreEqual(user1.Password, "pass1");

            user2 = databaseService.GetUserCredential("k.h.rajeeb@gmail.com");
            Assert.AreEqual(user2.Password, "pass2");

            user2.Password = "pass3";
            databaseService.UpdateUserCredential(user2);

            user2 = databaseService.GetUserCredential("k.h.rajeeb@gmail.com");
            Assert.AreEqual(user2.Password, "pass3");

            databaseService.DeleteUserCredential("ozdreamer.it@gmail.com");
            databaseService.DeleteUserCredential("k.h.rajeeb@gmail.com");

            Assert.AreEqual(databaseService.GetUserCredential("ozdreamer.it@gmail.com"), null);
            Assert.AreEqual(databaseService.GetUserCredential("k.h.rajeeb@gmail.com"), null);
        }

        /// <summary>
        /// Tests the user details.
        /// </summary>
        [TestMethod]
        public void TestUserDetails()
        {
            var databaseService = this.container.Resolve<IDatabaseService>();

            var user = new UserCredential { UserName = "ozdreamer.it@gmail.com", Password = "pass1" };
            databaseService.CreateUserCredential(user);
            var userDetail = new UserDetail { FirstName = "Kamrul", LastName = "Hasan", DateOfBirth = new DateTime(1980, 11, 9), AddressLine1 = "58 Portree Cres", AddressCity = "Heathwood", AddressPostCode = "4110", AddressState = "QLD", AddressCountry = "Australia", PrimaryContact = "0411342791", UseAddressAsPostal = true };
            databaseService.CreateUserDetail("ozdreamer.it@gmail.com", userDetail);

            userDetail = databaseService.GetUserDetail("ozdreamer.it@gmail.com");
            Assert.AreEqual(userDetail.DateOfBirth, new DateTime(1980, 11, 9));
            Assert.AreEqual(userDetail.AddressCity, "Heathwood");
            Assert.AreEqual($"{userDetail.FirstName} {userDetail.LastName}", "Kamrul Hasan");
            Assert.IsTrue(userDetail.UseAddressAsPostal);

            userDetail.DateOfBirth = userDetail.DateOfBirth.AddMonths(1);
            userDetail.DateOfBirth = userDetail.DateOfBirth.AddDays(5);
            userDetail.LastName = "Islam";
            databaseService.UpdateUserDetail(userDetail);
            Assert.AreEqual(userDetail.DateOfBirth, new DateTime(1980, 12, 14));
            Assert.AreEqual($"{userDetail.FirstName} {userDetail.LastName}", "Kamrul Islam");

            databaseService.DeleteUserCredential("ozdreamer.it@gmail.com");
            Assert.AreEqual(databaseService.GetUserDetail("ozdreamer.it@gmail.com"), null);
            Assert.AreEqual(databaseService.GetUserCredential("ozdreamer.it@gmail.com"), null);
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

            Assert.AreEqual(databaseService.GetAllDestinations().Count, 2);

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
            Assert.AreEqual(databaseService.GetAllDestinations().Count, 0);
        }
    }
}