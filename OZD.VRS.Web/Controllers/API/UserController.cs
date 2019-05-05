using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OZD.VRS.Repository;

namespace OZD.VRS.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// The context.
        /// </summary>
        private readonly VehicleContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserController(VehicleContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>The number of users.</returns>
        [HttpGet]
        public string Get() => this.context.UserCredentials.Count().ToString();
    }
}