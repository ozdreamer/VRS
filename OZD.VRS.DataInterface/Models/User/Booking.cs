using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Newtonsoft.Json;
using OZD.VRS.DataInterface.Models.Admin;

namespace OZD.VRS.DataInterface.Models.User
{
    [Table("Booking", Schema = "User")]
    public class Booking
    {
        #region Linked Properties

        /// <summary>
        /// Gets or sets the vehicle identifier.
        /// </summary>
        /// <value>The vehicle identifier.</value>
        [Required]
        public long VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the operator identifier.
        /// </summary>
        /// <value>The operator identifier.</value>
        [Required]
        public long OperatorId { get; set; }

        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        /// <value>The payment identifier.</value>
        public long PaymentId { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>The time.</value>
        [Required]
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Gets or sets the seat.
        /// </summary>
        /// <value>The seat.</value>
        public string Seat { get; set; }

        /// <summary>
        /// Gets or sets the booking date.
        /// </summary>
        /// <value>The booking date.</value>
        [Required]
        public DateTime BookingDate { get; set; }

        /// <summary>
        /// Gets or sets the cancellation date.
        /// </summary>
        /// <value>The cancellation date.</value>
        public DateTime CancellationDate { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [Required]
        public string Status { get; set; }

        #endregion

        #region Linked Properties

        /// <summary>
        /// Gets or sets the vehicle.
        /// </summary>
        /// <value>The vehicle.</value>
        [ForeignKey("VehicleId")]
        [XmlIgnore, JsonIgnore]
        public Vehicle Vehicle { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        [ForeignKey("UserId")]
        [XmlIgnore, JsonIgnore]
        public UserCredential User { get; set; }

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>The operator.</value>
        [ForeignKey("OperatorId")]
        [XmlIgnore, JsonIgnore]
        public Operator Operator { get; set; }

        #endregion
    }
}