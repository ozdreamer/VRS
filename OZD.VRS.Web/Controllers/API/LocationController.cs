using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using OZD.VRS.DataInterface.Models;
using OZD.VRS.Service.Interfaces;

namespace OZD.VRS.Web.Controllers.API
{
    /// <summary>
    /// The controller to deal with the locations.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        /// <summary>
        /// The database service.
        /// </summary>
        private readonly IDatabaseService databaseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public LocationController(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        /// <summary>
        /// Gets the destinations.
        /// </summary>
        /// <returns>The collection of destinations.</returns>
        [HttpGet]
        [Route("destinations")]
        public string GetDestinations() => JsonConvert.SerializeObject(this.databaseService.GetAllDestinations());

        /// <summary>
        /// Creates the destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <returns>The destination identifier.</returns>
        [HttpPost]
        [Route("createdestination/{destination}")]
        public string CreateDestination(string destination)
        {
            var destinationDataModel = JsonConvert.DeserializeObject<Destination>(destination);
            return JsonConvert.SerializeObject(this.databaseService.CreateDestination(destinationDataModel));
        }

        /// <summary>
        /// Gets the booking offices.
        /// </summary>
        /// <returns>The collection of booking offices.</returns>
        [HttpGet]
        [Route("bookingoffices")]
        public string GetBookingOffices() => JsonConvert.SerializeObject(this.databaseService.GetAllBookingOffices());


        /// <summary>
        /// Create a booking offices.
        /// </summary>
        /// <returns>The booking office identifier.</returns>
        [HttpPost]
        [Route("createbookingoffice/{bookingOffice}")]
        public string CreatBookingOffice(string bookingOffice)
        {
            var bookingOfficeDataModel = JsonConvert.DeserializeObject<BookingOffice>(bookingOffice);
            return JsonConvert.SerializeObject(this.databaseService.CreateBookingOffice(bookingOfficeDataModel));
        }
    }
}