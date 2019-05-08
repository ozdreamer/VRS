using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using OZD.VRS.Service.Interfaces;

namespace OZD.VRS.Web.Controllers.API
{
    /// <summary>
    /// The controller to deal the vehicle routes.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        /// <summary>
        /// The database service.
        /// </summary>
        private readonly IDatabaseService databaseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteController" /> class.
        /// </summary>
        /// <param name="databaseService">The database service.</param>
        public RouteController(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        /// <summary>
        /// Gets the destination by source.
        /// </summary>
        /// <param name="sourceId">The source identifier.</param>
        /// <returns>Collection of destinations.</returns>
        [HttpGet]
        [Route("destinationsbysource/{sourceId:long}")]
        public string GetDestinationsBySource(long sourceId)
        {
            return JsonConvert.SerializeObject(this.databaseService.GetDestinationsBySource(sourceId));
        }
    }
}