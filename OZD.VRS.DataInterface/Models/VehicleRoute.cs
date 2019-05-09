using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace OZD.VRS.DataInterface.Models
{
    /// <summary>
    /// The route between two locations.
    /// </summary>
    /// <seealso cref="OZD.VRS.DataInterface.Models.BaseDto" />
    [Table("Route", Schema = "Route")]
    public class Route : BaseDto
    {
        /// <summary>
        /// Gets or sets from destination identifier.
        /// </summary>
        /// <value>From destination identifier.</value>
        [Required]
        public long FromDestinationId { get; set; }

        /// <summary>
        /// Gets or sets to destination identifier.
        /// </summary>
        /// <value>To destination identifier.]</value>
        [Required]
        public long ToDestinationId { get; set; }

        /// <summary>
        /// Gets or sets from destination.
        /// </summary>
        /// <value>from destination./// </value>
        [ForeignKey("FromDestinationId")]
        [XmlIgnore, JsonIgnore]
        public virtual Destination From { get; set; }

        /// <summary>
        /// Gets or sets to destination.
        /// </summary>
        /// <value>To.</value>
        [ForeignKey("ToDestinationId")]
        [XmlIgnore, JsonIgnore]
        public virtual Destination To { get; set; }
    }
}