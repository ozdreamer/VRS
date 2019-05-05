using System.ComponentModel.DataAnnotations;

namespace OZD.VRS.DataInterface.Models
{
    public class BaseDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Required]
        public long Id { get; set; }
    }
}
