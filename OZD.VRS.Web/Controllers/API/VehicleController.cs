using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using OZD.VRS.Repository;

namespace OZD.VRS.Web.Controllers.API
{
    /// <summary>
    /// The vehicle API controller which deals with the vehicles.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly VehicleContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public VehicleController(VehicleContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get the collection of vehicles.
        /// </summary>
        /// <returns>The collection of vehicles.</returns>
        [HttpGet]
        public string Get() => JsonConvert.SerializeObject(this.context.Vehicles);
    }
}