using System.Collections.Generic;
using System.Threading.Tasks;
using OZD.VRS.DataInterface.Models;

namespace OZD.VRS.Service.Interfaces
{
    public interface IDatabaseService
    {
        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns>The change results.</returns>
        Task<int> SaveChanges();

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
        /// <returns>The new user credential.</returns>
        UserCredential CreateUserCredential(UserCredential userCredential);

        /// <summary>
        /// Updates the user credential.
        /// </summary>
        /// <param name="userCredential">The user credential.</param>
        UserCredential UpdateUserCredential(UserCredential userCredential);

        /// <summary>
        /// Deletes the user credential.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        void DeleteUserCredential(string userName);

        /// <summary>
        /// Gets the user detail.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>The user detail.</returns>
        UserDetail GetUserDetail(string userName);

        /// <summary>
        /// Creates the user detail.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userDetail">The user detail.</param>
        /// <returns>The user detail.</returns>
        UserDetail CreateUserDetail(string userName, UserDetail userDetail);

        /// <summary>
        /// Updates the user detail.
        /// </summary>
        /// <param name="userDetail">The user detail.</param>
        UserDetail UpdateUserDetail(UserDetail userDetail);

        #endregion

        #region Vehicle

        /// <summary>
        /// Gets all vehicles.
        /// </summary>
        /// <returns>Collection of vehicles.</returns>
        ICollection<Vehicle> GetAllVehicles();

        /// <summary>
        /// Gets the vehicle.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>The vehicle.</returns>
        Vehicle GetVehicle(long vehicleId);

        /// <summary>
        /// Creates the vehicle detail.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        /// <returns>The new vehicle.</returns>
        Vehicle CreateVehicle(Vehicle vehicle);

        /// <summary>
        /// Updates the vehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        Vehicle UpdateVehicle(Vehicle vehicle);

        /// <summary>
        /// Deletes the vehicle.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        void DeleteVehicle(long vehicleId);

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
        Destination CreateDestination(Destination destination);

        /// <summary>
        /// Updates the destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        Destination UpdateDestination(Destination destination);

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
        BookingOffice CreateBookingOffice(BookingOffice bookingOffice);

        /// <summary>
        /// Updates the bookingOffice.
        /// </summary>
        /// <param name="bookingOffice">The bookingOffice.</param>
        BookingOffice UpdateBookingOffice(BookingOffice bookingOffice);

        /// <summary>
        /// Deletes bookingOffice.
        /// </summary>
        /// <param name="bookingOfficeId">The bookingOffice identifier.</param>
        void DeleteBookingOffice(long bookingOfficeId);

        #endregion

        #region Route

        /// <summary>
        /// Gets the route.
        /// </summary>
        /// <param name="routeId">The route identifier.</param>
        /// <returns>The vehicle route.</returns>
        RouteSchedule GetRouteSchedule(long routeScheduleId);

        /// <summary>
        /// Creates the route schedule.
        /// </summary>
        /// <param name="routeSchedule">The route schedule.</param>
        /// <returns>The route schedule.</returns>
        RouteSchedule CreateRouteSchedule(RouteSchedule routeSchedule);

        /// <summary>
        /// Updates the route schedule.
        /// </summary>
        /// <param name="routeSchedule">The route schedule.</param>
        /// <returns>The updated route schedule.</returns>
        RouteSchedule UpdateRouteSchedule(RouteSchedule routeSchedule);

        /// <summary>
        /// Deletes the route.
        /// </summary>
        /// <param name="route">The route.</param>
        void DeleteRouteSchedule(long routeScheduleId);

        #endregion

        #region Operator

        /// <summary>
        /// Gets all operators.
        /// </summary>
        /// <returns>Collection of operators.</returns>
        ICollection<Operator> GetAllOperators();

        /// <summary>
        /// Gets the operator.
        /// </summary>
        /// <param name="operatorId">The operator identifier.</param>
        /// <returns>The operator.</returns>
        Operator GetOperator(long operatorId);

        /// <summary>
        /// Creates the operator.
        /// </summary>
        /// <param name="fleetOperator">The fleet operator.</param>
        /// <returns>The new operator.</returns>
        Operator CreateOperator(Operator fleetOperator);

        /// <summary>
        /// Updates the operator.
        /// </summary>
        /// <param name="fleetOperator">The fleet operator.</param>
        Operator UpdateOperator(Operator fleetOperator);

        /// <summary>
        /// Deletes the operator.
        /// </summary>
        /// <param name="operatorId">The operator identifier.</param>
        void DeleteOperator(long operatorId);

        #endregion
    }
}
