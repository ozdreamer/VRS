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

        #region Route

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
        
        #region Vehicle

        /// <summary>
        /// Gets the vehicle.
        /// </summary>
        /// <param name="vehicleScheduleId">The vehicle schedule identifier.</param>
        /// <returns>The vehicle vehicle schedule.</returns>
        public VehicleSchedule GetVehicleSchedule(long vehicleScheduleId) => this.context.VehicleSchedules.FirstOrDefault(x => x.Id == vehicleScheduleId);

        /// <summary>
        /// Creates the vehicle schedule.
        /// </summary>
        /// <param name="vehicleSchedule">The vehicle schedule.</param>
        /// <returns>The vehicle schedule.</returns>
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
