using Microsoft.EntityFrameworkCore;
using OZD.VRS.DataInterface.Models;

namespace OZD.VRS.Repository
{
    public class VehicleContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public VehicleContext(DbContextOptions<VehicleContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the user credentials.
        /// </summary>
        /// <value>
        /// The user credentials.
        /// </value>
        public DbSet<UserCredential> UserCredentials { get; set; }
    }
}
