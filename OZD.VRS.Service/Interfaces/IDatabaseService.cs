using System.Collections.Generic;
using System.Threading.Tasks;
using OZD.VRS.DataInterface.Models.Admin;
using OZD.VRS.DataInterface.Models.User;

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

        #region SeatClass

        /// <summary>
        /// Gets the seat class.
        /// </summary>
        /// <param name="seatClassId">The seat class identifier.</param>
        /// <returns>The vehicle seat class.</returns>
        SeatClass GetSeatClass(long seatClassId);

        /// <summary>
        /// Creates the seat class.
        /// </summary>
        /// <param name="seat class">The seat class.</param>
        /// <returns>The new seat class.</returns>
        SeatClass CreateSeatClass(SeatClass seatClass);

        /// <summary>
        /// Updates the seat class.
        /// </summary>
        /// <param name="seat class">The seat class.</param>
        /// <returns>The updated seat class.</returns>
        SeatClass UpdateSeatClass(SeatClass seatClass);

        /// <summary>
        /// Deletes the seat class.
        /// </summary>
        /// <param name="seatClassId">The seat class identifier.</param>
        void DeleteSeatClass(long seatClassId);

        #endregion

        #region Seat Layout

        /// <summary>
        /// Gets the seat layout.
        /// </summary>
        /// <param name="seatLayoutId">The seat layout identifier.</param>
        /// <returns>The seat layout.</returns>
        SeatLayout GetSeatLayout(long seatLayoutId);

        /// <summary>
        /// Creates the seat layout.
        /// </summary>
        /// <param name="seatLayout">The seat layout.</param>
        /// <returns>The seat layout.</returns>
        SeatLayout CreateSeatLayout(SeatLayout seatLayout);

        /// <summary>
        /// Updates the seat layout.
        /// </summary>
        /// <param name="seatLayout">The seat layout.</param>
        /// <returns>The updated seat layout.</returns>
        SeatLayout UpdateSeatLayout(SeatLayout seatLayout);

        /// <summary>
        /// Deletes the seat layout.
        /// </summary>
        /// <param name="seatLayoutId">The seat layout identifier.</param>
        void DeleteSeatLayout(long seatLayoutId);

        #endregion

        #region Amenity

        /// <summary>
        /// Gets the amenity.
        /// </summary>
        /// <param name="amenityId">The amenity identifier.</param>
        /// <returns>The amenity.</returns>
        Amenity GetAmenity(long amenityId);

        /// <summary>
        /// Gets all amenities.
        /// </summary>
        /// <returns>Collection of amenities.</returns>
        ICollection<Amenity> GetAllAmenities();

        /// <summary>
        /// Creates the amenity.
        /// </summary>
        /// <param name="amenity">The amenity.</param>
        /// <returns>The amenity.</returns>
        Amenity CreateAmenity(Amenity amenity);

        /// <summary>
        /// Updates the amenity.
        /// </summary>
        /// <param name="amenity">The amenity.</param>
        /// <returns>The updated amenity.</returns>
        Amenity UpdateAmenity(Amenity amenity);

        /// <summary>
        /// Deletes the amenity.
        /// </summary>
        /// <param name="amenityId">The amenity identifier.</param>
        void DeleteAmenity(long amenityId);

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

        #region Waypoint

        /// <summary>
        /// Gets the waypoint.
        /// </summary>
        /// <param name="waypointId">The waypoint identifier.</param>
        /// <returns>The vehicle waypoint.</returns>
        Waypoint GetWaypoint(long waypointId);

        /// <summary>
        /// Creates the waypoint.
        /// </summary>
        /// <param name="waypoint">The waypoint.</param>
        /// <returns>The new waypoint.</returns>
        Waypoint CreateWaypoint(Waypoint waypoint);

        /// <summary>
        /// Updates the waypoint.
        /// </summary>
        /// <param name="waypoint">The waypoint.</param>
        /// <returns>The updated waypoint.</returns>
        Waypoint UpdateWaypoint(Waypoint waypoint);

        /// <summary>
        /// Deletes the waypoint.
        /// </summary>
        /// <param name="waypointId">The waypoint identifier.</param>
        void DeleteWaypoint(long waypointId);

        #endregion

        #region Pickup Point

        /// <summary>
        /// Gets the pickup point.
        /// </summary>
        /// <param name="pickupPointId">The pickup point identifier.</param>
        /// <returns>The vehicle pickup point.</returns>
        PickupPoint GetPickupPoint(long pickupPointId);

        /// <summary>
        /// Gets the pickup points by route.
        /// </summary>
        /// <param name="routeId">The route identifier.</param>
        /// <returns>Collection of pickup point.</returns>
        ICollection<PickupPoint> GetPickupPointsByRoute(long routeId);

        /// <summary>
        /// Creates the pickup point.
        /// </summary>
        /// <param name="pickupPoint">The pickup point.</param>
        /// <returns>The new pickup point.</returns>
        PickupPoint CreatePickupPoint(PickupPoint pickupPoint);

        /// <summary>
        /// Updates the pickup point.
        /// </summary>
        /// <param name="pickupPoint">The pickup point.</param>
        /// <returns>The updated pickup point.</returns>
        PickupPoint UpdatePickupPoint(PickupPoint pickupPoint);

        /// <summary>
        /// Deletes the pickup point.
        /// </summary>
        /// <param name="pickupPointId">The pickup point identifier.</param>
        void DeletePickupPoint(long pickupPointId);

        #endregion

        #region Drop-Off Point

        /// <summary>
        /// Gets the drop-off point.
        /// </summary>
        /// <param name="dropOffPointId">The drop-off point identifier.</param>
        /// <returns>The vehicle drop-off point.</returns>
        DropOffPoint GetDropOffPoint(long dropOffPointId);

        /// <summary>
        /// Gets the drop-off points by route.
        /// </summary>
        /// <param name="routeId">The route identifier.</param>
        /// <returns>Collection of drop-off point.</returns>
        ICollection<DropOffPoint> GetDropOffPointsByRoute(long routeId);

        /// <summary>
        /// Creates the drop-off point.
        /// </summary>
        /// <param name="dropOffPoint">The drop-off point.</param>
        /// <returns>The new drop-off point.</returns>
        DropOffPoint CreateDropOffPoint(DropOffPoint dropOffPoint);

        /// <summary>
        /// Updates the drop-off point.
        /// </summary>
        /// <param name="dropOffPoint">The drop-off point .</param>
        /// <returns>The updated drop-off point.</returns>
        DropOffPoint UpdateDropOffPoint(DropOffPoint dropOffPoint);

        /// <summary>
        /// Deletes the drop-off point.
        /// </summary>
        /// <param name="dropOffPointId">The drop-off point identifier.</param>
        void DeleteDropOffPoint(long dropOffPointId);

        #endregion

        #region Route

        /// <summary>
        /// Gets the route.
        /// </summary>
        /// <param name="routeId">The route identifier.</param>
        /// <returns>The vehicle route.</returns>
        Route GetRoute(long routeId);

        /// <summary>
        /// Creates the route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>The new route.</returns>
        Route CreateRoute(Route route);

        /// <summary>
        /// Updates the route .
        /// </summary>
        /// <param name="route">The route .</param>
        /// <returns>The updated route .</returns>
        Route UpdateRoute(Route route);

        /// <summary>
        /// Deletes the route.
        /// </summary>
        /// <param name="routeId">The route identifier.</param>
        void DeleteRoute(long routeId);

        #endregion

        #region Route Schedule

        /// <summary>
        /// Gets the route schedule.
        /// </summary>
        /// <param name="routeId">The route schedule identifier.</param>
        /// <returns>The vehicle route schedule.</returns>
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
        /// Deletes the route schedule.
        /// </summary>
        /// <param name="routeScheduleId">The route schedule identifier.</param>
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

        #region Vehicle Schedule

        /// <summary>
        /// Gets the vehicle.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>The vehicle vehicle.</returns>
        VehicleSchedule GetVehicleSchedule(long vehicleScheduleId);

        /// <summary>
        /// Creates the vehicle schedule.
        /// </summary>
        /// <param name="vehicleSchedule">The vehicle schedule.</param>
        /// <returns>The vehicle schedule.</returns>
        VehicleSchedule CreateVehicleSchedule(VehicleSchedule vehicleSchedule);

        /// <summary>
        /// Updates the vehicle schedule.
        /// </summary>
        /// <param name="vehicleSchedule">The vehicle schedule.</param>
        /// <returns>The updated vehicle schedule.</returns>
        VehicleSchedule UpdateVehicleSchedule(VehicleSchedule vehicleSchedule);

        /// <summary>
        /// Deletes the vehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        void DeleteVehicleSchedule(long vehicleScheduleId);

        #endregion
    }
}
