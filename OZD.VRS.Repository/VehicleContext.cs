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
        /// <para>
        /// Override this method to configure the database (and other options) to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// The base implementation does nothing.
        /// </para>
        /// <para>
        /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
        /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        /// the options have already been set, and skip some or all of the logic in
        /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        /// </para>
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
        /// typically define extension methods on this object that allow you to configure the context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLazyLoadingProxies();
        }

        /// <summary>
        /// Gets or sets the user credentials.
        /// </summary>
        /// <value>The user credentials.</value>
        public DbSet<UserCredential> UserCredentials { get; set; }

        /// <summary>
        /// Gets or sets the user details.
        /// </summary>
        /// <value>The user details.</value>
        public DbSet<UserDetail> UserDetails { get; set; }

        /// <summary>
        /// Gets or sets the vehicle details.
        /// </summary>
        /// <value>The vehicle details.</value>
        public DbSet<VehicleDetail> VehicleDetails { get; set; }

        /// <summary>
        /// Gets or sets the destination.
        /// </summary>
        /// <value>The destination.</value>
        public DbSet<Destination> Destinations { get; set; }

        /// <summary>
        /// Gets or sets the booking office.
        /// </summary>
        /// <value>The booking office.</value>
        public DbSet<BookingOffice> BookingOffices { get; set; }

        /// <summary>
        /// Gets or sets the vehicle routes.
        /// </summary>
        /// <value>The vehicle routes.</value>
        public DbSet<VehicleRoute> VehicleRoutes { get; set; }
    }
}
