using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OZD.VRS.DataInterface.Models.Admin
{
    /// <summary>
    /// The destination where the vehicles operate.
    /// </summary>
    /// <seealso cref="OZD.VRS.DataInterface.Models.BaseDto" />
    [Table("Destination", Schema = "Admin")]
    public class Destination : BaseDto
    {
        #region Mapped Properties

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the post code.
        /// </summary>
        /// <value>The post code.</value>
        public int PostCode { get; set; }

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
