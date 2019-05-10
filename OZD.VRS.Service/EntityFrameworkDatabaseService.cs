using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OZD.VRS.DataInterface;
using OZD.VRS.DataInterface.Models;
using OZD.VRS.Repository;
using OZD.VRS.Service.Interfaces;

namespace OZD.VRS.Service
{
    public class EntityFrameworkDatabaseService : IDatabaseService
    {
        /// <summary>
        /// The context.
        /// </summary>
        private readonly VehicleContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrameworkDatabaseService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EntityFrameworkDatabaseService(VehicleContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns>The change results.</returns>
        public Task<int> SaveChanges() => this.context.SaveChangesAsync();

        #region User


        /// <summary>
        /// Gets the user credential.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>The user credential.</returns>
        public UserCredential GetUserCredential(string userName) => this.context.UserCredentials.FirstOrDefault(x => x.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));

        /// <summary>
        /// Creates the user credential.
        /// </summary>
        /// <param name="userCredential">The user credential.</param>
        /// <returns>The new user credential id.</returns>
        public UserCredential CreateUserCredential(UserCredential userCredential)
        {
            this.context.UserCredentials.Add(userCredential);
            this.context.SaveChanges();
            return userCredential;
        }

        /// <summary>
        /// Updates the user credential.
        /// </summary>
        /// <param name="userCredential">The user credential.</param>
        public UserCredential UpdateUserCredential(UserCredential userCredential)
        {
            var existingUserCredential = this.context.UserCredentials.FirstOrDefault(x => x.Id == userCredential.Id);
            if (existingUserCredential != null)
            {
                DataModelUpdater.UpdateUserCredentials(userCredential, ref existingUserCredential);
                this.context.UserCredentials.Update(existingUserCredential);
                this.context.SaveChanges();
            }

            return existingUserCredential;
        }

        /// <summary>
        /// Deletes the user credential.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        public void DeleteUserCredential(string userName)
        {
            var existingUserCredential = this.context.UserCredentials.FirstOrDefault(x => x.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
            if (existingUserCredential != null)
            {
                var existingUserDetail = this.context.UserDetails.FirstOrDefault(x => x.UserId == existingUserCredential.Id);
                if (existingUserDetail != null)
                {
                    this.context.UserDetails.Remove(existingUserDetail);
                    existingUserCredential.UserDetailId = null;
                    this.context.SaveChanges();
                }

                this.context.UserCredentials.Remove(existingUserCredential);
                this.context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the user detail.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>The user detail.</returns>
        public UserDetail GetUserDetail(string userName) => this.GetUserCredential(userName)?.UserDetail;

        /// <summary>
        /// Creates the user detail.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userDetail">The user detail.</param>
        /// <returns>The user detail identifier.</returns>
        public UserDetail CreateUserDetail(string userName, UserDetail userDetail)
        {
            var userCredential = this.GetUserCredential(userName);
            userDetail.UserId = userCredential.Id;
            this.context.UserDetails.Add(userDetail);
            this.context.SaveChanges();

            userCredential.UserDetailId = userDetail.Id;
            this.context.Update(userCredential);
            this.context.SaveChanges();

            return userDetail;
        }

        /// <summary>
        /// Updates the user detail.
        /// </summary>
        /// <param name="userDetail">The user detail.</param>
        public UserDetail UpdateUserDetail(UserDetail userDetail)
        {
            var existingUserDetail = this.context.UserDetails.FirstOrDefault(x => x.Id == userDetail.Id);
            if (existingUserDetail != null)
            {
                DataModelUpdater.UpdateUserDetails(userDetail, ref existingUserDetail);
                this.context.UserDetails.Update(existingUserDetail);
                this.context.SaveChanges();
            }

            return existingUserDetail;
        }

        #endregion

        #region Vehicle

        /// <summary>
        /// Gets all vehicle details.
        /// </summary>
        /// <returns>Collection of vehicle details.</returns>
        public ICollection<Vehicle> GetAllVehicleDetails() => this.context.VehicleDetails.ToList();

        /// <summary>
        /// Gets the vehicle detail.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>The vehicle detail.</returns>
        public Vehicle GetVehicleDetail(long vehicleId) => this.context.VehicleDetails.FirstOrDefault(x => x.Id == vehicleId);

        /// <summary>
        /// Creates the vehicle detail.
        /// </summary>
        /// <param name="vehicleDetail">The vehicle detail.</param>
        /// <returns>The new vehicle detail id.entifier</returns>
        public Vehicle CreateVehicleDetail(Vehicle vehicleDetail)
        {
            this.context.VehicleDetails.Add(vehicleDetail);
            this.context.SaveChanges();
            return vehicleDetail;
        }

        /// <summary>
        /// Updates the vehicle detail.
        /// </summary>
        /// <param name="vehicleDetail">The vehicle detail.</param>
        public Vehicle UpdateVehicleDetail(Vehicle vehicleDetail)
        {
            var existingVehicleDetail = this.GetVehicleDetail(vehicleDetail.Id);
            if (existingVehicleDetail != null)
            {
                DataModelUpdater.UpdateVehicleDetail(vehicleDetail, ref existingVehicleDetail);
                this.context.VehicleDetails.Update(existingVehicleDetail);
                this.context.SaveChanges();
            }

            return existingVehicleDetail;
        }

        /// <summary>
        /// Deletes the vehicle detail.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        public void DeleteVehicleDetail(long vehicleId)
        {
            var existingVehicleDetail = this.GetVehicleDetail(vehicleId);
            if (existingVehicleDetail != null)
            {
                this.context.VehicleDetails.Remove(existingVehicleDetail);
                this.context.SaveChanges();
            }
        }

        #endregion

        #region Location

        /// <summary>
        /// Gets all destinations.
        /// </summary>
        /// <returns>Collection of destinations.</returns>
        public ICollection<Destination> GetAllDestinations() => this.context.Destinations.ToList();

        /// <summary>
        /// Gets the destination.
        /// </summary>
        /// <param name="destinationId">The destination identifier.</param>
        /// <returns>The destination.</returns>
        public Destination GetDestination(long destinationId) => this.context.Destinations.FirstOrDefault(x => x.Id == destinationId);

        /// <summary>
        /// Creates the destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <returns>The new destination identifier.</returns>
        public Destination CreateDestination(Destination destination)
        {
            this.context.Destinations.Add(destination);
            this.context.SaveChanges();
            return destination;
        }

        /// <summary>
        /// Updates the destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        public Destination UpdateDestination(Destination destination)
        {
            var existingDestination = this.GetDestination(destination.Id);
            if (existingDestination != null)
            {
                DataModelUpdater.UpdateDestination(destination, ref existingDestination);
                this.context.Destinations.Update(existingDestination);
                this.context.SaveChanges();
            }

            return existingDestination;
        }

        /// <summary>
        /// Deletes destination.
        /// </summary>
        /// <param name="destinationId">The destination identifier.</param>
        public void DeleteDestination(long destinationId)
        {
            var existingDestination = this.GetDestination(destinationId);
            if (existingDestination != null)
            {
                this.context.Destinations.Remove(existingDestination);
                this.context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all booking offices.
        /// </summary>
        /// <returns>Collection of booking offices.</returns>
        public ICollection<BookingOffice> GetAllBookingOffices() => this.context.BookingOffices.ToList();

        /// <summary>
        /// Gets the bookingOffice.
        /// </summary>
        /// <param name="bookingOfficeId">The booking office identifier.</param>
        /// <returns>The booking office.</returns>
        public BookingOffice GetBookingOffice(long bookingOfficeId) => this.context.BookingOffices.FirstOrDefault(x => x.Id == bookingOfficeId);

        /// <summary>
        /// Creates the bookingOffice.
        /// </summary>
        /// <param name="bookingOffice">The booking office.</param>
        /// <returns>The new booking office identifier.</returns>
        public BookingOffice CreateBookingOffice(BookingOffice bookingOffice)
        {
            this.context.BookingOffices.Add(bookingOffice);
            this.context.SaveChanges();
            return bookingOffice;
        }

        /// <summary>
        /// Updates the booking office.
        /// </summary>
        /// <param name="bookingOffice">The booking office.</param>
        public BookingOffice UpdateBookingOffice(BookingOffice bookingOffice)
        {
            var existingBookingOffice = this.GetBookingOffice(bookingOffice.Id);
            if (existingBookingOffice != null)
            {
                DataModelUpdater.UpdateBookingOffice(bookingOffice, ref existingBookingOffice);
                this.context.BookingOffices.Update(existingBookingOffice);
                this.context.SaveChanges();
            }

            return existingBookingOffice;
        }

        /// <summary>
        /// Deletes booking office.
        /// </summary>
        /// <param name="bookingOfficeId">The booking office identifier.</param>
        public void DeleteBookingOffice(long bookingOfficeId)
        {
            var existingBookingOffice = this.GetBookingOffice(bookingOfficeId);
            if (existingBookingOffice != null)
            {
                this.context.BookingOffices.Remove(existingBookingOffice);
                this.context.SaveChanges();
            }
        }

        #endregion

        #region Route

        /// <summary>
        /// Gets the route.
        /// </summary>
        /// <param name="routeId">The route identifier.</param>
        /// <returns>The vehicle route.</returns>
        public Route GetRoute(long routeId) => this.context.Routes.FirstOrDefault(x => x.Id == routeId);

        /// <summary>
        /// Creates the route.
        /// </summary>
        /// <param name="fromDestinationId">From destination identifier.</param>
        /// <param name="toDestinationId">To destination identifier.</param>
        /// <returns>The collection of newly create routes.</returns>
        public ICollection<Route> CreateRoute(long fromDestinationId, long toDestinationId)
        {
            var route1 = this.context.Routes.FirstOrDefault(x => x.FromDestinationId == fromDestinationId && x.ToDestinationId == toDestinationId);
            if (route1 == null)
            {
                route1 = new Route { FromDestinationId = fromDestinationId, ToDestinationId = toDestinationId };
                this.context.Routes.Add(route1);
            }

            var route2 = this.context.Routes.FirstOrDefault(x => x.FromDestinationId == toDestinationId && x.ToDestinationId == fromDestinationId);
            if (route2 == null)
            {
                route2 = new Route { FromDestinationId = toDestinationId, ToDestinationId = fromDestinationId };
                this.context.Routes.Add(route2);
            }

            this.context.SaveChanges();

            return new[] { route1, route2 };
        }

        /// <summary>
        /// Deletes the route.
        /// </summary>
        /// <param name="route">The route.</param>
        public void DeleteRoute(Route route)
        {
            var existingRoute = this.context.Routes.FirstOrDefault(x => x.Id == route.Id);
            if (existingRoute != null)
            {
                this.context.Remove(existingRoute);
                this.context.SaveChanges();
            }

            var reverseRoute = this.context.Routes.FirstOrDefault(x => x.FromDestinationId == route.ToDestinationId && x.ToDestinationId == route.FromDestinationId);
            if (reverseRoute != null)
            {
                this.context.Remove(reverseRoute);
                this.context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the destinations by source.
        /// </summary>
        /// <param name="sourceId">The source identifier.</param>
        /// <returns>Collection of destinations.</returns>
        public ICollection<Destination> GetDestinationsBySource(long sourceId)
        {
            var destinations = new List<Destination>();
            var routes = this.context.Routes.Where(x => x.FromDestinationId == sourceId);
            if (routes.Any())
            {
                var destinationIds = routes.Select(x => x.ToDestinationId);
                destinations.AddRange(this.context.Destinations.Where(x => destinationIds.Contains(x.Id)));
            }

            return destinations;
        }

        #endregion
    }
}
