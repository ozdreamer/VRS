using System.ComponentModel.DataAnnotations.Schema;

namespace OZD.VRS.DataInterface.Models.Admin
{
    /// <summary>
    /// Schedule of each vehicle.
    /// </summary>
    /// <seealso cref="OZD.VRS.DataInterface.Models.BaseDto" />
    [Table("VehicleSchedule", Schema = "Admin")]
    public class VehicleSchedule : BaseDto
    {
        public long OperatorId { get; set; }

        public long VehicleId { get; set; }

        public long RouteScheduleId { get; set; }
    }
}
