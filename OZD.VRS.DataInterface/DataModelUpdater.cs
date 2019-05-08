using OZD.VRS.DataInterface.Models;

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
        /// Updates the vehicle detail.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public static void UpdateVehicleDetail(VehicleDetail from, ref VehicleDetail to)
        {
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
        }
    }
}