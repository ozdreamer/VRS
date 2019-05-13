using OZD.VRS.DataInterface.Models.Admin;
using OZD.VRS.DataInterface.Models.User;

namespace OZD.VRS.DataInterface
{
    /// <summary>
    /// A static class to update data model objects.
    /// </summary>
    public static class DataModelUpdater
    {
        /// <summary>
        /// Updates the user credentials.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public static void UpdateUserCredentials(UserCredential from, ref UserCredential to)
        {
            to.Password = from.Password;
            to.Active = from.Active;
        }

        /// <summary>
        /// Updates the user details.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public static void UpdateUserDetails(UserDetail from, ref UserDetail to)
        {
            to.FirstName = from.FirstName;
            to.MiddleName = from.MiddleName;
            to.LastName = from.LastName;
            to.DateOfBirth = from.DateOfBirth;
            to.PrimaryContact = from.PrimaryContact;
            to.SecondaryContact = from.SecondaryContact;
            to.AlternateEmail = from.AlternateEmail;
            to.AddressLine1 = from.AddressLine1;
            to.AddressLine2 = from.AddressLine2;
            to.AddressArea = from.AddressArea;
            to.AddressCity = from.AddressCity;
            to.AddressState = from.AddressState;
            to.AddressPostCode = from.AddressPostCode;
            to.AddressCountry = from.AddressCountry;
            to.PostalLine1 = from.PostalLine1;
            to.PostalLine2 = from.PostalLine2;
            to.PostalArea = from.PostalArea;
            to.PostalCity = from.PostalCity;
            to.PostalState = from.PostalState;
            to.PostalPostCode = from.PostalPostCode;
            to.PostalCountry = from.PostalCountry;
            to.UseAddressAsPostal = from.UseAddressAsPostal;
        }

        /// <summary>
        /// Updates the seat layout.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public static void UpdateSeatLayout(SeatLayout from, ref SeatLayout to)
        {
            to.Rows = from.Rows;
            to.Columns = from.Columns;
            to.Layout = from.Layout;
            to.Active = from.Active;
        }

        /// <summary>
        /// Updates the vehicle.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public static void UpdateVehicle(Vehicle from, ref Vehicle to)
        {
            to.SeatLayoutId = from.SeatLayoutId;
            to.VIN = from.VIN;
            to.VehicleType = from.VehicleType;
            to.Manufacturer = from.Manufacturer;
            to.Model = from.Model;
            to.Year = from.Year;
            to.RegistrationState = from.RegistrationState;
            to.RegistrationNumber = from.RegistrationNumber;
            to.RegistrationExpiry = from.RegistrationExpiry;
            to.TotalSeats = from.TotalSeats;
            to.DriveType = from.DriveType;
            to.BaseStation = from.BaseStation;
            to.Active = from.Active;
        }

        /// <summary>
        /// Updates the destination.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public static void UpdateDestination(Destination from, ref Destination to)
        {
            to.City = from.City;
            to.State = from.State;
            to.PostCode = from.PostCode;
            to.Active = from.Active;
        }

        /// <summary>
        /// Updates the booking office.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public static void UpdateBookingOffice(BookingOffice from, ref BookingOffice to)
        {
            to.AddressLine1 = from.AddressLine1;
            to.AddressLine2 = from.AddressLine2;
            to.Area = from.Area;
            to.Email = from.Email;
            to.PrimaryContact = from.PrimaryContact;
            to.SecondaryContact = from.SecondaryContact;
            to.Active = from.Active;
        }

        /// <summary>
        /// Updates the route schedule.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public static void UpdateRouteSchedule(RouteSchedule from, ref RouteSchedule to)
        {
            to.FromDestinationId = from.FromDestinationId;
            to.ToDestinationId = from.ToDestinationId;
            to.Day = from.Day;
            to.Time = from.Time;
            to.Active = from.Active;
        }

        /// <summary>
        /// Updates the operator.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public static void UpdateOperator(Operator from, ref Operator to)
        {
            to.Name = from.Name;
            to.AddressLine1 = from.AddressLine1;
            to.AddressLine2 = from.AddressLine2;
            to.AddressArea = from.AddressArea;
            to.AddressCity = from.AddressCity;
            to.AddressState = from.AddressState;
            to.AddressPostCode = from.AddressPostCode;
            to.AddressCountry = from.AddressCountry;
            to.PrimaryContact = from.PrimaryContact;
            to.SecondaryContact = from.SecondaryContact;
            to.PrimaryEmail = from.PrimaryEmail;
            to.Active = from.Active;
        }

        /// <summary>
        /// Updates the vehicle schedule.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public static void UpdateVehicleSchedule(VehicleSchedule from, ref VehicleSchedule to)
        {
            to.RouteScheduleId = from.RouteScheduleId;
            to.VehicleId = from.VehicleId;
            to.Date = from.Date;
            to.Active = from.Active;
        }
    }
}