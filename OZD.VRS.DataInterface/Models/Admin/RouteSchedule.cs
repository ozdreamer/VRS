using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace OZD.VRS.DataInterface.Models.Admin
{
    [Table("RouteSchedule", Schema = "Admin")]
    public class RouteSchedule : BaseDto
    {
        #region Mapped Properties

        /// <summary>
        /// Gets or sets the operator identifier.
        /// </summary>
        /// <value>The operator identifier.</value>
        [Required]
        public long OperatorId { get; set; }

        /// <summary>
        /// Gets or sets from destination identifier.
        /// </summary>
        /// <value>From destination identifier.</value>
        [Required]
        public long RouteId { get; set; }

        /// <summary>
        /// Gets or sets the day.
        /// </summary>
        /// <value>The day.</value>
        [Required]
        public DayOfWeek Day { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>The time.</value>
        [Required]
        public TimeSpan Time { get; set; }

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
        /// Gets or sets the operator.
        /// </summary>
        /// <value>The operator.</value>
        [ForeignKey("OperatorId")]
        [XmlIgnore, JsonIgnore]
        public virtual Operator Operator { get; set; }

        /// <summary>
        /// Gets or sets the route.
        /// </summary>
        /// <value>The route.</value>
        [ForeignKey("RouteId")]
        [XmlIgnore, JsonIgnore]
        public virtual Route Route { get; set; }

        #endregion

        #region Non-mapped Properties

        /// <summary>
        /// Gets the name of the operator.
        /// </summary>
        /// <value>The name of the operator.</value>
        [NotMapped]
        public string OperatorName { get; set; }

        #endregion
    }
}