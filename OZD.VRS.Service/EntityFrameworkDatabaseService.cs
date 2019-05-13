using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OZD.VRS.DataInterface;
using OZD.VRS.DataInterface.Models.Admin;
using OZD.VRS.DataInterface.Models.User;
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

        #region SeatClass

        /// <summary>
        /// Gets the seat class.
        /// </summary>
        /// <param name="seatClassId">The seat class identifier.</param>
        /// <returns>The vehicle seat class.</returns>
        public SeatClass GetSeatClass(long seatClassId) => this.context.SeatClasses.FirstOrDefault(x => x.Id == seatClassId);

        /// <summary>
        /// Creates the seat class.
        /// </summary>
        /// <param name="seat class">The seat class.</param>
        /// <returns>The new seat class.</returns>
        public SeatClass CreateSeatClass(SeatClass seatClass)
        {
            this.context.SeatClasses.Add(seatClass);
            this.context.SaveChanges();
            return seatClass;
        }

        /// <summary>
        /// Updates the seat class.
        /// </summary>
        /// <param name="seat class">The seat class.</param>
        /// <returns>The updated seat class.</returns>
        public SeatClass UpdateSeatClass(SeatClass seatClass)
        {
            var existingSeatClass = this.GetSeatClass(seatClass.Id);
            if (existingSeatClass != null)
            {
                DataModelUpdater.UpdateSeatClass(seatClass, ref existingSeatClass);
                this.context.Update(existingSeatClass);
                this.context.SaveChanges();
            }

            return existingSeatClass;
        }

        /// <summary>
        /// Deletes the seat class.
        /// </summary>
        /// <param name="seatClassId">The seat class identifier.</param>
        public void DeleteSeatClass(long seatClassId)
        {
            var existingSeatClass = this.GetSeatClass(seatClassId);
            if (existingSeatClass != null)
            {
                this.context.Remove(existingSeatClass);
                this.context.SaveChanges();
            }
        }

        #endregion

        #region Seat Layout

        /// <summary>
        /// Gets the seat layout.
        /// </summary>
        /// <param name="seatLayoutId">The seat layout identifier.</param>
        /// <returns>The seat layout.</returns>
        public SeatLayout GetSeatLayout(long seatLayoutId) => this.context.SeatLayouts.FirstOrDefault(x => x.Id == seatLayoutId);

        /// <summary>
        /// Creates the seat layout.
        /// </summary>
        /// <param name="seatLayout">The seat layout.</param>
        /// <returns>The seat layout.</returns>
        public SeatLayout CreateSeatLayout(SeatLayout seatLayout)
        {
            this.context.Add(seatLayout);
            this.context.SaveChanges();
            return seatLayout;
        }

        /// <summary>
        /// Updates the seat layout.
        /// </summary>
        /// <param name="seatLayout">The seat layout.</param>
        /// <returns>The updated seat layout.</returns>
        public SeatLayout UpdateSeatLayout(SeatLayout seatLayout)
        {
            var existingSeatLayout = this.GetSeatLayout(seatLayout.Id);
            if (existingSeatLayout != null)
            {
                DataModelUpdater.UpdateSeatLayout(seatLayout, ref existingSeatLayout);
                this.context.Update(existingSeatLayout);
                this.context.SaveChanges();
            }

            return existingSeatLayout;
        }

        /// <summary>
        /// Deletes the seat layout.
        /// </summary>
        /// <param name="seatLayoutId">The seat layout identifier.</param>
        public void DeleteSeatLayout(long seatLayoutId)
        {
            var existingSeatLayout = this.GetSeatLayout(seatLayoutId);
            if (existingSeatLayout != null)
            {
                this.context.Remove(existingSeatLayout);
                this.context.SaveChanges();
            }
        }

        #endregion

        #region Amenity

        /// <summary>
        /// Gets the amenity.
        /// </summary>
        /// <param name="amenityId">The amenity identifier.</param>
        /// <returns>The amenity.</returns>
        public Amenity GetAmenity(long amenityId) => this.context.Amenities.FirstOrDefault(x => x.Id == amenityId);

        /// <summary>
        /// Gets all amenities.
        /// </summary>
        /// <returns>Collection of amenities.</returns>
        public ICollection<Amenity> GetAllAmenities() => this.context.Amenities.ToList();

        /// <summary>
        /// Creates the amenity.
        /// </summary>
        /// <param name="amenity">The amenity.</param>
        /// <returns>The amenity.</returns>
        public Amenity CreateAmenity(Amenity amenity)
        {
            this.context.Amenities.Add(amenity);
            this.context.SaveChanges();
            return amenity;
        }

        /// <summary>
        /// Updates the amenity.
        /// </summary>
        /// <param name="amenity">The amenity.</param>
        /// <returns>The updated amenity.</returns>
        public Amenity UpdateAmenity(Amenity amenity)
        {
            var existingAmenity = this.GetAmenity(amenity.Id);
            if (existingAmenity != null)
            {
                DataModelUpdater.UpdateAmenity(amenity, ref existingAmenity);
                this.context.Update(existingAmenity);
                this.context.SaveChanges();
            }

            return existingAmenity;
        }

        /// <summary>
        /// Deletes the amenity.
        /// </summary>
        /// <param name="amenityId">The amenity identifier.</param>
        public void DeleteAmenity(long amenityId)
        {
            var existingAmenity = this.GetAmenity(amenityId);
            if (existingAmenity != null)
            {
                this.context.Remove(existingAmenity);
                this.context.SaveChanges();
            }
        }

        #endregion

        #region Vehicle

        /// <summary>
        /// Gets all vehicles.
        /// </summary>
        /// <returns>Collection of vehicles.</returns>
        public ICollection<Vehicle> GetAllVehicles() => this.context.Vehicles.ToList();

        /// <summary>
        /// Gets the vehicle.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>The vehicle.</returns>
        public Vehicle GetVehicle(long vehicleId) => this.context.Vehicles.FirstOrDefault(x => x.Id == vehicleId);

        /// <summary>
        /// Creates the vehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        /// <returns>The new vehicle identifier</returns>
        public Vehicle CreateVehicle(Vehicle vehicle)
        {
            this.context.Vehicles.Add(vehicle);
            this.context.SaveChanges();
            return vehicle;
        }

        /// <summary>
        /// Updates the vehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        public Vehicle UpdateVehicle(Vehicle vehicle)
        {
            var existingVehicleDetail = this.GetVehicle(vehicle.Id);
            if (existingVehicleDetail != null)
            {
                DataModelUpdater.UpdateVehicle(vehicle, ref existingVehicleDetail);
                this.context.Vehicles.Update(existingVehicleDetail);
                this.context.SaveChanges();
            }

            return existingVehicleDetail;
        }

        /// <summary>
        /// Deletes the vehicle.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        public void DeleteVehicle(long vehicleId)
        {
            var existingVehicleDetail = this.GetVehicle(vehicleId);
            if (existingVehicleDetail != null)
            {
                this.context.Vehicles.Remove(existingVehicleDetail);
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

        #region Waypoint

        /// <summary>
        /// Gets the waypoint.
        /// </summary>
        /// <param name="waypointId">The waypoint identifier.</param>
        /// <returns>The vehicle waypoint.</returns>
        public Waypoint GetWaypoint(long waypointId) => this.context.Waypoints.FirstOrDefault(x => x.Id == waypointId);

        /// <summary>
        /// Creates the waypoint.
        /// </summary>
        /// <param name="waypoint">The waypoint.</param>
        /// <returns>The new waypoint.</returns>
        public Waypoint CreateWaypoint(Waypoint waypoint)
        {
            this.context.Waypoints.Add(waypoint);
            this.context.SaveChanges();
            return waypoint;
        }

        /// <summary>
        /// Updates the waypoint.
        /// </summary>
        /// <param name="waypoint">The waypoint.</param>
        /// <returns>The updated waypoint.</returns>
        public Waypoint UpdateWaypoint(Waypoint waypoint)
        {
            var existingWaypoint = this.GetWaypoint(waypoint.Id);
            if (existingWaypoint != null)
            {
                DataModelUpdater.UpdateWaypoint(waypoint, ref existingWaypoint);
                this.context.Waypoints.Update(existingWaypoint);
                this.context.SaveChanges();
            }

            return existingWaypoint;
        }

        /// <summary>
        /// Deletes the waypoint.
        /// </summary>
        /// <param name="waypointId">The waypoint identifier.</param>
        public void DeleteWaypoint(long waypointId)
        {
            var existingWaypoint = this.GetWaypoint(waypointId);
            if (existingWaypoint != null)
            {
                this.context.Waypoints.Remove(existingWaypoint);
                this.context.SaveChanges();
            }
        }

        #endregion

        #region Pickup Point

        /// <summary>
        /// Gets the pickup point.
        /// </summary>
        /// <param name="pickupPointId">The pickup point identifier.</param>
        /// <returns>The vehicle pickup point.</returns>
        public PickupPoint GetPickupPoint(long pickupPointId) => this.context.PickupPoints.FirstOrDefault(x => x.Id == pickupPointId);

        /// <summary>
        /// Gets the pickup points by route.
        /// </summary>
        /// <param name="routeId">The route identifier.</param>
        /// <returns>Collection of pickup point.</returns>
        public ICollection<PickupPoint> GetPickupPointsByRoute(long routeId) => this.context.PickupPoints.Where(x => x.RouteId == routeId).ToList();

        /// <summary>
        /// Creates the pickup point.
        /// </summary>
        /// <param name="pickupPoint">The pickup point.</param>
        /// <returns>The new pickup point.</returns>
        public PickupPoint CreatePickupPoint(PickupPoint pickupPoint)
        {
            this.context.PickupPoints.Add(pickupPoint);
            this.context.SaveChanges();
            return pickupPoint;
        }

        /// <summary>
        /// Updates the pickup point.
        /// </summary>
        /// <param name="pickupPoint">The pickup point.</param>
        /// <returns>The updated pickup point.</returns>
        public PickupPoint UpdatePickupPoint(PickupPoint pickupPoint)
        {
            var existingPickupPoint = this.GetPickupPoint(pickupPoint.Id);
            if (existingPickupPoint != null)
            {
                DataModelUpdater.UpdatePickupPoint(pickupPoint, ref existingPickupPoint);
                this.context.PickupPoints.Update(existingPickupPoint);
                this.context.SaveChanges();
            }

            return existingPickupPoint;
        }

        /// <summary>
        /// Deletes the pickup point.
        /// </summary>
        /// <param name="pickupPointId">The pickup point identifier.</param>
        public void DeletePickupPoint(long pickupPointId)
        {
            var existingPickupPoint = this.GetPickupPoint(pickupPointId);
            if (existingPickupPoint != null)
            {
                this.context.PickupPoints.Remove(existingPickupPoint);
                this.context.SaveChanges();
            }
        }

        #endregion

        #region Drop-Off Point

        /// <summary>
        /// Gets the drop-off point.
        /// </summary>
        /// <param name="dropOffPointId">The drop-off point identifier.</param>
        /// <returns>The vehicle drop-off point.</returns>
        public DropOffPoint GetDropOffPoint(long dropOffPointId) => this.context.DropOffPoints.FirstOrDefault(x => x.Id == dropOffPointId);

        /// <summary>
        /// Gets the drop-off points by route.
        /// </summary>
        /// <param name="routeId">The route identifier.</param>
        /// <returns>Collection of drop-off point.</returns>
        public ICollection<DropOffPoint> GetDropOffPointsByRoute(long routeId) => this.context.DropOffPoints.Where(x => x.RouteId == routeId).ToList();

        /// <summary>
        /// Creates the drop-off point.
        /// </summary>
        /// <param name="dropOffPoint">The drop-off point.</param>
        /// <returns>The new drop-off point.</returns>
        public DropOffPoint CreateDropOffPoint(DropOffPoint dropOffPoint)
        {
            this.context.DropOffPoints.Add(dropOffPoint);
            this.context.SaveChanges();
            return dropOffPoint;
        }

        /// <summary>
        /// Updates the drop-off point.
        /// </summary>
        /// <param name="dropOffPoint">The drop-off point .</param>
        /// <returns>The updated drop-off point.</returns>
        public DropOffPoint UpdateDropOffPoint(DropOffPoint dropOffPoint)
        {
            var existingDropOffPoint = this.GetDropOffPoint(dropOffPoint.Id);
            if (existingDropOffPoint != null)
            {
                DataModelUpdater.UpdateDropOffPoint(dropOffPoint, ref existingDropOffPoint);
                this.context.DropOffPoints.Update(existingDropOffPoint);
                this.context.SaveChanges();
            }

            return existingDropOffPoint;
        }

        /// <summary>
        /// Deletes the drop-off point.
        /// </summary>
        /// <param name="dropOffPointId">The drop-off point identifier.</param>
        public void DeleteDropOffPoint(long dropOffPointId)
        {
            var existingDropOffPoint = this.GetDropOffPoint(dropOffPointId);
            if (existingDropOffPoint != null)
            {
                this.context.DropOffPoints.Remove(existingDropOffPoint);
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
        /// Creates the route .
        /// </summary>
        /// <param name="route">The route .</param>
        /// <returns>The route .</returns>
        public Route CreateRoute(Route route)
        {
            this.context.Routes.Add(route);
            this.context.SaveChanges();
            return route;
        }

        /// <summary>
        /// Updates the route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>The updated route.</returns>
        public Route UpdateRoute(Route route)
        {
            var existingRoute = this.GetRoute(route.Id);
            if (existingRoute != null)
            {
                DataModelUpdater.UpdateRoute(route, ref existingRoute);
                this.context.Routes.Update(existingRoute);
                this.context.SaveChanges();
            }

            return existingRoute;
        }

        /// <summary>
        /// Deletes the route.
        /// </summary>
        /// <param name="route">The route.</param>
        public void DeleteRoute(long routeId)
        {
            var existingRoute = this.GetRoute(routeId);
            if (existingRoute != null)
            {
                this.context.Routes.Remove(existingRoute);
                this.context.SaveChanges();
            }
        }

        #endregion

        #region Route Schedule

        /// <summary>
        /// Gets the route.
        /// </summary>
        /// <param name="routeScheduleId">The route schedule identifier.</param>
        /// <returns>The vehicle route schedule.</returns>
        public RouteSchedule GetRouteSchedule(long routeScheduleId) => this.context.RouteSchedules.FirstOrDefault(x => x.Id == routeScheduleId);

        /// <summary>
        /// Creates the route schedule.
        /// </summary>
        /// <param name="routeSchedule">The route schedule.</param>
        /// <returns>The route schedule.</returns>
        public RouteSchedule CreateRouteSchedule(RouteSchedule routeSchedule)
        {
            this.context.RouteSchedules.Add(routeSchedule);
            this.context.SaveChanges();
            return routeSchedule;
        }

        /// <summary>
        /// Updates the route schedule.
        /// </summary>
        /// <param name="routeSchedule">The route schedule.</param>
        /// <returns>The updated route schedule.</returns>
        public RouteSchedule UpdateRouteSchedule(RouteSchedule routeSchedule)
        {
            var existingRoadSchedule = this.GetRouteSchedule(routeSchedule.Id);
            if (existingRoadSchedule != null)
            {
                DataModelUpdater.UpdateRouteSchedule(routeSchedule, ref existingRoadSchedule);
                this.context.Update(existingRoadSchedule);
                this.context.SaveChanges();
            }

            return existingRoadSchedule;
        }

        /// <summary>
        /// Deletes the route schedule.
        /// </summary>
        /// <param name="routeScheduleId">The route schedule identifier.</param>
        public void DeleteRouteSchedule(long routeScheduleId)
        {
            var existingRoute = this.context.RouteSchedules.FirstOrDefault(x => x.Id == routeScheduleId);
            if (existingRoute != null)
            {
                this.context.Remove(existingRoute);
                this.context.SaveChanges();
            }
        }

        #endregion

        #region Operator

        /// <summary>
        /// Gets all operators.
        /// </summary>
        /// <returns>Collection of operators.</returns>
        public ICollection<Operator> GetAllOperators() => this.context.Operators.ToList();

        /// <summary>
        /// Gets the operator.
        /// </summary>
        /// <param name="operatorId">The operator identifier.</param>
        /// <returns>The operator.</returns>
        public Operator GetOperator(long operatorId) => this.context.Operators.FirstOrDefault(x => x.Id == operatorId);

        /// <summary>
        /// Creates the operator.
        /// </summary>
        /// <param name="fleetOperator">The fleet operator.</param>
        /// <returns>The new operator.</returns>
        public Operator CreateOperator(Operator fleetOperator)
        {
            this.context.Operators.Add(fleetOperator);
            this.context.SaveChanges();
            return fleetOperator;
        }

        /// <summary>
        /// Updates the operator.
        /// </summary>
        /// <param name="fleetOperator">The fleet operator.</param>
        public Operator UpdateOperator(Operator fleetOperator)
        {
            var existingOperator = this.context.Operators.FirstOrDefault(x => x.Id == fleetOperator.Id);
            if (existingOperator != null)
            {
                DataModelUpdater.UpdateOperator(fleetOperator, ref existingOperator);
                this.context.Update(existingOperator);
                this.context.SaveChanges();
            }

            return existingOperator;
        }

        /// <summary>
        /// Deletes the operator.
        /// </summary>
        /// <param name="operatorId">The operator identifier.</param>
        public void DeleteOperator(long operatorId)
        {
            var existingOperator = this.GetOperator(operatorId);
            if (existingOperator != null)
            {
                this.context.Operators.Remove(existingOperator);
                this.context.SaveChanges();
            }
        }

        #endregion
        
        #region Vehicle Schedule

        /// <summary>
        /// Gets the vehicle schedule.
        /// </summary>
        /// <param name="vehicleScheduleId">The vehicle schedule identifier.</param>
        /// <returns>The vehicle schedule.</returns>
        public VehicleSchedule GetVehicleSchedule(long vehicleScheduleId) => this.context.VehicleSchedules.FirstOrDefault(x => x.Id == vehicleScheduleId);

        /// <summary>
        /// Creates the vehicle schedule.
        /// </summary>
        /// <param name="vehicleSchedule">The vehicle schedule.</param>
        /// <returns>The new vehicle schedule.</returns>
        public VehicleSchedule CreateVehicleSchedule(VehicleSchedule vehicleSchedule)
        {
            this.context.VehicleSchedules.Add(vehicleSchedule);
            this.context.SaveChanges();
            return vehicleSchedule;
        }

        /// <summary>
        /// Updates the vehicle schedule.
        /// </summary>
        /// <param name="vehicleSchedule">The vehicle schedule.</param>
        /// <returns>The updated vehicle schedule.</returns>
        public VehicleSchedule UpdateVehicleSchedule(VehicleSchedule vehicleSchedule)
        {
            var existingRoadSchedule = this.GetVehicleSchedule(vehicleSchedule.Id);
            if (existingRoadSchedule != null)
            {
                DataModelUpdater.UpdateVehicleSchedule(vehicleSchedule, ref existingRoadSchedule);
                this.context.Update(existingRoadSchedule);
                this.context.SaveChanges();
            }

            return existingRoadSchedule;
        }

        /// <summary>
        /// Deletes the vehicle schedule.
        /// </summary>
        /// <param name="vehicleScheduleId">The vehicle schedule identifier.</param>
        public void DeleteVehicleSchedule(long vehicleScheduleId)
        {
            var existingVehicle = this.context.VehicleSchedules.FirstOrDefault(x => x.Id == vehicleScheduleId);
            if (existingVehicle != null)
            {
                this.context.Remove(existingVehicle);
                this.context.SaveChanges();
            }
        }

        #endregion
    }
}
