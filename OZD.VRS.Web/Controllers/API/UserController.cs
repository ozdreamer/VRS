using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using OZD.VRS.DataInterface.Models.User;
using OZD.VRS.Service.Interfaces;

namespace OZD.VRS.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// The database service.
        /// </summary>
        private readonly IDatabaseService databaseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserController(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        /// <summary>
        /// Gets the specified user information.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The specific user information.</returns>
        [HttpGet]
        [Route("userdetails/{userName}")]
        public string GetUserDetails(string userName)
        {
            return JsonConvert.SerializeObject(this.databaseService.GetUserDetail(userName));
        }
    }
}