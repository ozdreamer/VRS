using System.Collections.Generic;
using OZD.VRS.DataInterface.Models;

namespace OZD.VRS.Service.Interfaces
{
    public interface IDatabaseService
    {
        #region User

        /// <summary>
        /// Gets the user credential.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>The user credential.</returns>
        UserCredential GetUserCredential(string userName);

        /// <summary>
        /// Creates the user credential.
        /// </summary>
        /// <param name="userCredential">The user credential.</param>
        /// <returns>The new user credential id.</returns>
        long CreateUserCredential(UserCredential userCredential);

        /// <summary>
        /// Updates the user credential.
        /// </summary>
        /// <param name="userCredential">The user credential.</param>
        void UpdateUserCredential(UserCredential userCredential);

        /// <summary>
        /// Deletes the user credential.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        void DeleteUserCredential(string userName);

        /// <summary>
        /// Gets the user details.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>The user detail.</returns>
        UserDetail GetUserDetails(string userName);

        /// <summary>
        /// Creates the user detail.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userDetail">The user detail.</param>
        /// <returns>The user detail identifier.</returns>
        long CreateUserDetail(string userName, UserDetail userDetail);

        /// <summary>
        /// Updates the user detail.
        /// </summary>
        /// <param name="userDetail">The user detail.</param>
        void UpdateUserDetail(UserDetail userDetail);

        #endregion

        #region Vehicle

        /// <summary>
        /// Gets all vehicle details.
        /// </summary>
        /// <returns>Collection of vehicle details.</returns>
        ICollection<VehicleDetail> GetAllVehicleDetails();

        /// <summary>
        /// Gets the vehicle detail.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>The vehicle detail.</returns>
        VehicleDetail GetVehicleDetail(long vehicleId);

        /// <summary>
        /// Creates the vehicle detail.
        /// </summary>
        /// <param name="vehicleDetail">The vehicle detail.</param>
        /// <returns>The new vehicle detail id.entifier</returns>
        long CreateVehicleDetail(VehicleDetail vehicleDetail);

        /// <summary>
        /// Updates the vehicle detail.
        /// </summary>
        /// <param name="vehicleDetail">The vehicle detail.</param>
        void UpdateVehicleDetail(VehicleDetail vehicleDetail);

        /// <summary>
        /// Deletes the vehicle detail.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        void DeleteVehicleDetail(long vehicleId);

        #endregion

        #region Location

        /// <summary>
        /// Gets all destinations.
        /// </summary>
        /// <returns>Collection of destinations.</returns>
        ICollection<Destination> GetAllDestinations();

        /// <summary>
        /// Gets the destination.
        /// </summary>
        /// <param name="destinationId">The destination identifier.</param>
        /// <returns>The destination.</returns>
        Destination GetDestination(long destinationId);

        /// <summary>
        /// Creates the destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <returns>The new destination identifier.</returns>
        long CreateDestination(Destination destination);

        /// <summary>
        /// Updates the destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        void UpdateDestination(Destination destination);

        /// <summary>
        /// Deletes destination.
        /// </summary>
        /// <param name="destinationId">The destination identifier.</param>
        void DeleteDestination(long destinationId);

        /// <summary>
        /// Gets all bookingOffices.
        /// </summary>
        /// <returns>Collection of bookingOffices.</returns>
        ICollection<BookingOffice> GetAllBookingOffices();

        /// <summary>
        /// Gets the bookingOffice.
        /// </summary>
        /// <param name="bookingOfficeId">The bookingOffice identifier.</param>
        /// <returns>The bookingOffice.</returns>
        BookingOffice GetBookingOffice(long bookingOfficeId);

        /// <summary>
        /// Creates the bookingOffice.
        /// </summary>
        /// <param name="bookingOffice">The bookingOffice.</param>
        /// <returns>The new bookingOffice identifier.</returns>
        long CreateBookingOffice(BookingOffice bookingOffice);

        /// <summary>
        /// Updates the bookingOffice.
        /// </summary>
        /// <param name="bookingOffice">The bookingOffice.</param>
        void UpdateBookingOffice(BookingOffice bookingOffice);

        /// <summary>
        /// Deletes bookingOffice.
        /// </summary>
        /// <param name="bookingOfficeId">The bookingOffice identifier.</param>
        void DeleteBookingOffice(long bookingOfficeId);

        #endregion

        #region Route

        /// <summary>
        /// Gets the destinations by source.
        /// </summary>
        /// <param name="sourceId">The source identifier.</param>
        /// <returns>Collection of destinations.</returns>
        ICollection<Destination> GetDestinationsBySource(long sourceId);

        #endregion
    }
}
