using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace OZD.VRS.DataInterface.Models.Admin
{
    [Table("Vehicle", Schema = "Admin")]
    public class Vehicle : BaseDto
    {
        #region Mapped Properties

        /// <summary>
        /// Gets or sets the seat layout identifier.
        /// </summary>
        /// <value>The seat layout identifier.</value>
        public long SeatLayoutId { get; set; }

        /// <summary>
        /// Gets or sets the vehicle identification number.
        /// </summary>
        /// <value>
        /// The vehicle identification number.
        /// </value>
        public string VIN { get; set; }

        /// <summary>
        /// Gets or sets the type of the vehicle.
        /// </summary>
        /// <value>The type of the vehicle.</value>
        [Required]
        public string VehicleType { get; set; }

        /// <summary>
        /// Gets or sets the manufacturer.
        /// </summary>
        /// <value>The manufacturer.</value>
        [Required]
        public string Manufacturer { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>The year.</value>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the state of the registration.
        /// </summary>
        /// <value>The state of the registration.</value>
        [Required]
        public string RegistrationState { get; set; }

        /// <summary>
        /// Gets or sets the registration number.
        /// </summary>
        /// <value>The registration number.</value>
        [Required]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Gets or sets the registration expiry.
        /// </summary>
        /// <value>The registration expiry.</value>
        public DateTime RegistrationExpiry { get; set; }

        /// <summary>
        /// Gets or sets the total seats.
        /// </summary>
        /// <value>The total seats.</value>
        public int TotalSeats { get; set; }

        /// <summary>
        /// Gets or sets the type of the drive.
        /// </summary>
        /// <value>The type of the drive.</value>
        public string DriveType { get; set; }

        /// <summary>
        /// Gets or sets the base station.
        /// </summary>
        /// <value>The base station.</value>
        public string BaseStation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BookingOffice"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool Active { get; set; }

        #endregion

        #region Linked Properties

        /// <summary>
        /// Gets or sets the seat layout.
        /// </summary>
        /// <value>The seat layout.</value>
        [ForeignKey("SeatLayoutId")]
        [XmlIgnore, JsonIgnore]
        public virtual SeatLayout SeatLayout { get; set; }

        #endregion
    }
}