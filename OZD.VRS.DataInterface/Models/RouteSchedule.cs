using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace OZD.VRS.DataInterface.Models
{
    [Table("RouteSchedule", Schema = "Config")]
    public class RouteSchedule : BaseDto
    {
        /// <summary>
        /// Gets or sets the route identifier.
        /// </summary>
        /// <value>The route identifier.</value>
        [Required]
        public long RouteId { get; set; }

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

        /// <summary>
        /// Gets or sets the route.
        /// </summary>
        /// <value>The route.</value>
        [ForeignKey("RouteId")]
        [XmlIgnore, JsonIgnore]
        public virtual Route Route { get; set; }
    }
}
