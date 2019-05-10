using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace OZD.VRS.DataInterface.Models
{
    /// <summary>
    /// The full details of each user.
    /// </summary>
    /// <seealso cref="OZD.VRS.DataInterface.Models.BaseDto" />
    [Table("UserDetail", Schema = "Config")]
    public class UserDetail : BaseDto
    {
        #region Mapped Properties

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        [Required]
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>The name of the middle.</value>
        public string MiddleName{ get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>The date of birth.</value>
        [Required]
        public DateTime DateOfBirth { get; set; }

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
        /// Gets or sets the alternate email.
        /// </summary>
        /// <value>The alternate email.</value>
        public string AlternateEmail{ get; set; }

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
        /// Gets or sets the address state.
        /// </summary>
        /// <value>The address state.</value>
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
        /// Gets or sets the postal line1.
        /// </summary>
        /// <value>The postal line1.</value>
        public string PostalLine1 { get; set; }

        /// <summary>
        /// Gets or sets the postal line2.
        /// </summary>
        /// <value>The postal line2.</value>
        public string PostalLine2 { get; set; }

        /// <summary>
        /// Gets or sets the postal area.
        /// </summary>
        /// <value>The postal area.</value>
        public string PostalArea{ get; set; }

        /// <summary>
        /// Gets or sets the postal city.
        /// </summary>
        /// <value>The postal city. </value>
        public string PostalCity { get; set; }

        /// <summary>
        /// Gets or sets the postal state.
        /// </summary>
        /// <value>The postal state.</value>
        public string PostalState { get; set; }

        /// <summary>
        /// Gets or sets the postal post code.
        /// </summary>
        /// <value>The postal post code.</value>
        public string PostalPostCode { get; set; }

        /// <summary>
        /// Gets or sets the postal country.
        /// </summary>
        /// <value>The postal country.</value>
        public string PostalCountry { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user wants to use the primary address as postal address.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [the primary address is also the postal]; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool UseAddressAsPostal { get; set; }

        #endregion

        #region Linked Properties

        /// <summary>
        /// Gets or sets the user credential.
        /// </summary>
        /// <value>The user credential.</value>
        [XmlIgnore, JsonIgnore]
        [ForeignKey("UserId")]
        public virtual UserCredential UserCredential { get; set; }

        #endregion

        #region Non-mapped Properties

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName => this.UserCredential?.UserName;

        #endregion
    }
}