using System;
using System.Collections.Generic;
using System.Linq;

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
        public long CreateUserCredential(UserCredential userCredential)
        {
            this.context.UserCredentials.Add(userCredential);
            return userCredential.Id;
        }

        /// <summary>
        /// Updates the user credential.
        /// </summary>
        /// <param name="userCredential">The user credential.</param>
        public void UpdateUserCredential(UserCredential userCredential)
        {
            var existingUserCredential = this.context.UserCredentials.First(x => x.Id == userCredential.Id);
            if (existingUserCredential != null)
            {
                DataModelUpdater.UpdateUserCredentials(userCredential, ref existingUserCredential);
                this.context.UserCredentials.Update(existingUserCredential);
            }
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
                this.context.UserCredentials.Remove(existingUserCredential);
            }
        }

        /// <summary>
        /// Gets the user details.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>The user detail.</returns>
        public UserDetail GetUserDetails(string userName) => this.GetUserCredential(userName)?.UserDetails;

        /// <summary>
        /// Creates the user detail.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userDetail">The user detail.</param>
        /// <returns>The user detail identifier.</returns>
        public long CreateUserDetail(string userName, UserDetail userDetail)
        {
            var userCredential = this.GetUserCredential(userName);
            if (userCredential != null)
            {
                userDetail.UserId = userCredential.Id;
                this.context.UserDetails.Add(userDetail);

                return userDetail.Id;
            }

            return -1;
        }

        /// <summary>
        /// Updates the user detail.
        /// </summary>
        /// <param name="userDetail">The user detail.</param>
        public void UpdateUserDetail(UserDetail userDetail)
        {
            var existingUserDetail = this.context.UserDetails.FirstOrDefault(x => x.Id == userDetail.Id);
            if (existingUserDetail != null)
            {
                DataModelUpdater.UpdateUserDetails(userDetail, ref existingUserDetail);
                this.context.UserDetails.Update(existingUserDetail);
            }
        }

        #endregion

        #region Vehicle

        /// <summary>
        /// Gets all vehicle details.
        /// </summary>
        /// <returns>Collection of vehicle details.</returns>
        public ICollection<VehicleDetail> GetAllVehicleDetails() => this.context.VehicleDetails.ToList();

        /// <summary>
        /// Gets the vehicle detail.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>The vehicle detail.</returns>
        public VehicleDetail GetVehicleDetail(long vehicleId) => this.context.VehicleDetails.FirstOrDefault(x => x.Id == vehicleId);

        /// <summary>
        /// Creates the vehicle detail.
        /// </summary>
        /// <param name="vehicleDetail">The vehicle detail.</param>
        /// <returns>The new vehicle detail id.entifier</returns>
        public long CreateVehicleDetail(VehicleDetail vehicleDetail)
        {
            this.context.VehicleDetails.Add(vehicleDetail);
            return vehicleDetail.Id;
        }

        /// <summary>
        /// Updates the vehicle detail.
        /// </summary>
        /// <param name="vehicleDetail">The vehicle detail.</param>
        public void UpdateVehicleDetail(VehicleDetail vehicleDetail)
        {
            var existingVehicleDetail = this.GetVehicleDetail(vehicleDetail.Id);
            if (existingVehicleDetail != null)
            {
                DataModelUpdater.UpdateVehicleDetail(vehicleDetail, ref existingVehicleDetail);
                this.context.VehicleDetails.Update(existingVehicleDetail);
            }
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
        public long CreateDestination(Destination destination)
        {
            this.context.Destinations.Add(destination);
            return destination.Id;
        }

        /// <summary>
        /// Updates the destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        public void UpdateDestination(Destination destination)
        {
            var existingDestination = this.GetDestination(destination.Id);
            if (existingDestination != null)
            {
                DataModelUpdater.UpdateDestination(destination, ref existingDestination);
                this.context.Destinations.Update(existingDestination);
            }
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
        public long CreateBookingOffice(BookingOffice bookingOffice)
        {
            this.context.BookingOffices.Add(bookingOffice);
            return bookingOffice.Id;
        }

        /// <summary>
        /// Updates the booking office.
        /// </summary>
        /// <param name="bookingOffice">The booking office.</param>
        public void UpdateBookingOffice(BookingOffice bookingOffice)
        {
            var existingBookingOffice = this.GetBookingOffice(bookingOffice.Id);
            if (existingBookingOffice != null)
            {
                DataModelUpdater.UpdateBookingOffice(bookingOffice, ref existingBookingOffice);
                this.context.BookingOffices.Update(existingBookingOffice);
            }
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
            }
        }

        #endregion

        #region Route

        /// <summary>
        /// Gets the destinations by source.
        /// </summary>
        /// <param name="sourceId">The source identifier.</param>
        /// <returns>Collection of destinations.</returns>
        public ICollection<Destination> GetDestinationsBySource(long sourceId)
        {
            var destinations = new List<Destination>();
            var routes = this.context.VehicleRoutes.Where(x => x.FromDestinationId == sourceId);
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
