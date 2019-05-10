using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace OZD.VRS.DataInterface.Models.Admin
{
    /// <summary>
    /// The information about the ticket booking offices.
    /// </summary>
    /// <seealso cref="OZD.VRS.DataInterface.Models.BaseDto" />
    [Table("BookingOffice", Schema = "Admin")]
    public class BookingOffice : BaseDto
    {
        /// <summary>
        /// Gets or sets the destination identifier.
        /// </summary>
        /// <value>The destination identifier.</value>
        [Required]
	    public long DestinationId { get; set; }

        /// <summary>
        /// Gets or sets the address line1.
        /// </summary>
        /// <value>The address line1.</value>
        [Required]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the address line2.
        /// </summary>
        /// <value>The address line2.</value>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the area.
        /// </summary>
        /// <value>The area.</value>
        [Required]
        public string Area { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the primary contact.
        /// </summary>
        /// <value>The primary contact.</value>
        [Required]
        public string PrimaryContact { get; set; }

        /// <summary>
        /// Gets or sets the secondary contact.
        /// </summary>
        /// <value>The secondary contact.</value>
        public string SecondaryContact { get; set; }

        /// <summary>
        /// Gets or sets the destination.
        /// </summary>
        /// <value>The destination.</value>
        [XmlIgnore, JsonIgnore]
        [ForeignKey("DestinationId")]
        public virtual Destination Destination { get; set; }
    }
}