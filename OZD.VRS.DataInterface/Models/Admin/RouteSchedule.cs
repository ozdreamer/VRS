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
        public long FromDestinationId { get; set; }

        /// <summary>
        /// Gets or sets to destination identifier.
        /// </summary>
        /// <value>To destination identifier.</value>
        [Required]
        public long ToDestinationId { get; set; }

        /// <summary>
        /// Gets or sets the day.
        /// </summary>
        /// <value>The day.</value>
        [Required]
        public string Day { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>The time.</value>
        [Required]
        public TimeSpan Time { get; set; }

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
        /// Gets or sets from destination.
        /// </summary>
        /// <value>From destination.
        /// </value>
        [ForeignKey("FromDestinationId")]
        [XmlIgnore, JsonIgnore]
        public virtual Destination From { get; set; }

        /// <summary>
        /// Gets or sets to destination.
        /// </summary>
        /// <value>To destination.
        /// </value>
        [ForeignKey("ToDestinationId")]
        [XmlIgnore, JsonIgnore]
        public virtual Destination To { get; set; }

        #endregion

        #region Non-mapped Properties

        /// <summary>
        /// Gets the name of the operator.
        /// </summary>
        /// <value>The name of the operator.</value>
        [NotMapped]
        public string OperatorName { get; set; }

        /// <summary>
        /// Gets from destination.
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

        #endregion
    }
}