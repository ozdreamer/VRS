using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace OZD.VRS.DataInterface.Models.User
{
    /// <summary>
    /// The basic credentials to identify a user.
    /// </summary>
    /// <seealso cref="OZD.VRS.DataInterface.Models.BaseDto" />
    [Table("UserCredential", Schema = "User")]
    public class UserCredential : BaseDto
    {
        #region Mapped Properties

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [Required]
        [XmlIgnore, JsonIgnore]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the user detail identifier.
        /// </summary>
        /// <value>The user detail identifier.</value>
        public long? UserDetailId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SeatLayout"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool Active { get; set; }

        #endregion

        #region Linked Properties

        /// <summary>
        /// Gets or sets the user details.
        /// </summary>
        /// <value>The user details.</value>
        [ForeignKey("UserDetailId")]
        [XmlIgnore, JsonIgnore]
        public virtual UserDetail UserDetail { get; set; }

        #endregion
    }
}