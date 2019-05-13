using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace OZD.VRS.DataInterface.Models.Admin
{
    /// <summary>
    /// The route of a vehicle.
    /// </summary>
    /// <seealso cref="OZD.VRS.DataInterface.Models.BaseDto" />
    [Table("Route", Schema = "Admin")]
    public class Route : BaseDto
    {
        #region Mapped Properties

        /// <summary>
        /// Gets or sets departure destination identifier.
        /// </summary>
        /// <value>Departure destination identifier.</value>
        [Required]
        public long DepartureId { get; set; }

        /// <summary>
        /// Gets or sets arrival destination identifier.
        /// </summary>
        /// <value>Arrival destination identifier.</value>
        [Required]
        public long ArrivalId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Route"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool Active { get; set; }

        #endregion

        #region Linked Properties

        /// <summary>
        /// Gets or sets the departure location.
        /// </summary>
        /// <value>The departure location.</value>
        [ForeignKey("DepartureId")]
        [XmlIgnore, JsonIgnore]
        public virtual Destination Departure { get; set; }

        /// <summary>
        /// Gets or sets the arrival location.
        /// </summary>
        /// <value>The arrival location.</value>
        [ForeignKey("ArrivalId")]
        [XmlIgnore, JsonIgnore]
        public virtual Destination Arrival { get; set; }

        #endregion

        #region Non-mapped Properties

        /// <summary>
        /// Gets the name of the route.
        /// </summary>
        /// <value>The name of the route.</value>
        [NotMapped]
        public string RouteName => $"{ this.Departure?.City } - { this.Arrival?.City }";

        #endregion
    }
}