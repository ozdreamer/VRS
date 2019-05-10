using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OZD.VRS.DataInterface.Models
{
    [Table("VehicleSchedule", Schema = "Config")]
    public class VehicleSchedule : BaseDto
    {
        public long OperatorId { get; set; }

        public long VehicleId { get; set; }

        public long RouteId { get; set; }
    }
}
