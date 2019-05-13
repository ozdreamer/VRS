using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OZD.VRS.DataInterface.Models.Admin
{
    [Table("Operator", Schema = "Admin")]
    public class Operator : BaseDto
    {
        #region Mapped Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Required]
        public string Name { get; set; }

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
        /// Gets or sets the address area.
        /// </summary>
        /// <value>The address area.</value>
        public string AddressArea { get; set; }

        /// <summary>
        /// Gets or sets the address city.
        /// </summary>
        /// <value>The address city.</value>
        [Required]
        public string AddressCity { get; set; }

        /// <summary>
        /// Gets or sets the state of the address.
        /// </summary>
        /// <value>The state of the address.</value>
        [Required]
        public string AddressState { get; set; }

        /// <summary>
        /// Gets or sets the address post code.
        /// </summary>
        /// <value>The address post code.</value>
        [Required]
        public string AddressPostCode { get; set; }

        /// <summary>
        /// Gets or sets the address country.
        /// </summary>
        /// <value>The address country.</value>
        [Required]
        public string AddressCountry { get; set; }

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
        /// Gets or sets the primary email.
        /// </summary>
        /// <value>The primary email.</value>
        [Required]
        public string PrimaryEmail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BookingOffice"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool Active { get; set; }

        #endregion
    }
}