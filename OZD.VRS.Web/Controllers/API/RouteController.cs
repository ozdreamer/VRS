using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OZD.VRS.DataInterface.Models;
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
        /// Gets the route schedule.
        /// </summary>
        /// <param name="routeScheduleId">The route schedule identifier.</param>
        /// <returns>The route schedule.</returns>
        [HttpGet]
        [Route("routeschedule/{routeScheduleId:long}")]
        public string GetRouteSchedule(long routeScheduleId)
        {
            var routeSchedule = this.databaseService.GetRouteSchedule(routeScheduleId);
            routeSchedule.OperatorName = routeSchedule.Operator?.Name;
            routeSchedule.FromDestination = routeSchedule.From?.City;
            routeSchedule.ToDestination = routeSchedule.To?.City;
            return JsonConvert.SerializeObject(routeSchedule);
        }

        /// <summary>
        /// Posts the create route.
        /// </summary>
        /// <param name="routeSchedule">The route schedule data.</param>
        /// <returns>Collection of create routes.</returns>
        [HttpPost]
        [Route("createrouteschedule")]
        public string PostCreateRouteSchedule(RouteSchedule routeSchedule)
        {
            return JsonConvert.SerializeObject(this.databaseService.CreateRouteSchedule(routeSchedule));
        }
    }
}