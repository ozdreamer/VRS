using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace OZD.VRS.DataInterface.Models.Admin
{
    /// <summary>
    /// The pickup point of any route.
    /// </summary>
    /// <seealso cref="OZD.VRS.DataInterface.Models.BaseDto" />
    [Table("PickupPoint", Schema = "Admin")]
    public class PickupPoint : BaseDto
    {
        #region Mapped Properties

        /// <summary>
        /// Gets or sets the waypoint identifier.
        /// </summary>
        /// <value>The waypoint identifier.</value>
        [Required]
        public long WaypointId { get; set; }

        /// <summary>
        /// Gets or sets the route identifier.
        /// </summary>
        /// <value>The route identifier.</value>
        [Required]
        public long RouteId { get; set; }

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
        /// Gets or sets the waypoint.
        /// </summary>
        /// <value>The waypoint.</value>
        [ForeignKey("WaypointId")]
        [XmlIgnore, JsonIgnore]
        public virtual Waypoint Waypoint { get; set; }

        /// <summary>
        /// Gets or sets the route.
        /// </summary>
        /// <value>The route.</value>
        [ForeignKey("RouteId")]
        [XmlIgnore, JsonIgnore]
        public virtual Route Route { get; set; }

        #endregion
    }
}
