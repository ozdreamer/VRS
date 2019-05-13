using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace OZD.VRS.DataInterface.Models.Admin
{
    /// <summary>
    /// Schedule of each vehicle.
    /// </summary>
    /// <seealso cref="OZD.VRS.DataInterface.Models.BaseDto" />
    [Table("VehicleSchedule", Schema = "Admin")]
    public class VehicleSchedule : BaseDto
    {
        #region Linked Properties

        /// <summary>
        /// Gets or sets the operator identifier.
        /// </summary>
        /// <value>The operator identifier.</value>
        [Required]
        public long OperatorId { get; set; }

        /// <summary>
        /// Gets or sets the vehicle identifier.
        /// </summary>
        /// <value>The vehicle identifier.</value>
        [Required]
        public long VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the route schedule identifier.
        /// </summary>
        /// <value>The route schedule identifier.</value>
        [Required]
        public long RouteScheduleId { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BookingOffice"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool Active { get; set; }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>The operator.</value>
        [XmlIgnore, JsonIgnore]
        public virtual Operator Operator { get; set; }

        /// <summary>
        /// Gets or sets the vehicle.
        /// </summary>
        /// <value>The vehicle.</value>
        [XmlIgnore, JsonIgnore]
        public virtual Vehicle Vehicle { get; set; }

        /// <summary>
        /// Gets or sets the route schedule.
        /// </summary>
        /// <value>The route schedule.</value>
        [XmlIgnore, JsonIgnore]
        public virtual RouteSchedule RouteSchedule { get; set; }

        #endregion

        #region Non-mapped Properties

        /// <summary>
        /// Gets or sets the name of the operator.
        /// </summary>
        /// <value>
        /// The name of the operator.
        /// </value>
        [NotMapped]
        public string OperatorName { get; set; }

        /// <summary>
        /// Gets or sets the type of the vehicle.
        /// </summary>
        /// <value>The type of the vehicle.</value>
        [NotMapped]
        public string VehicleType { get; set; }

        /// <summary>
        /// Gets or sets from destination.
        /// </summary>
        /// <value>From destination.</value>
        [NotMapped]
        public string FromDestination { get; set; }

        /// <summary>
        /// Gets or sets to destination.
        /// </summary>
        /// <value>To destination.</value>
        [NotMapped]
        public string ToDestination { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>The time.</value>
        [NotMapped]
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Gets or sets the day of week.
        /// </summary>
        /// <value>The day of week.</value>
        [NotMapped]
        public DayOfWeek Day { get; set; }

        #endregion
    }
}