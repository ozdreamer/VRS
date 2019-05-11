using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using OZD.VRS.DataInterface.Models.Admin;
using OZD.VRS.DataInterface.Models.User;

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
        /// Executes the stored procedures.
        /// </summary>
        /// <param name="storeProcedureName">Name of the store procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="outParameters">The out parameters.</param>
        /// <returns>The execution result.</returns>
        public object ExecuteStoredProcedure(string storeProcedureName, IReadOnlyDictionary<string, object> parameters = null, IDictionary<string, object> outParameters = null)
        {
            var connection = this.Database.GetDbConnection();
            try
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storeProcedureName;

                // Attach the parameters.
                if (parameters != null)
                {
                    foreach (var kvp in parameters)
                    {
                        var param = command.CreateParameter();
                        param.Direction = ParameterDirection.Input;
                        param.ParameterName = kvp.Key;
                        param.Value = kvp.Value;
                        command.Parameters.Add(param);
                    }
                }

                // Attach the out parameters.
                if (outParameters != null)
                {
                    foreach (var kvp in outParameters)
                    {
                        var param = command.CreateParameter();
                        param.Direction = ParameterDirection.Output;
                        param.ParameterName = kvp.Key;
                        param.Value = kvp.Value;
                        command.Parameters.Add(param);
                    }
                }

                connection.Open();

                var result = command.ExecuteScalar();

                // Populate the return values to the out parameters for the caller to be used.
                if (outParameters != null)
                {
                    var commandParameters = command.Parameters.OfType<IDbDataParameter>().Where(x => x.Direction == ParameterDirection.Output);
                    foreach (var outParameter in outParameters.ToList())
                    {
                        var param = commandParameters.First(x => x.ParameterName == outParameter.Key);
                        outParameters[param.ParameterName] = param.Value;
                    }
                }

                connection.Close();

                return result;
            }
            catch (Exception ex)
            {
                connection.Close();
                throw ex;
            }
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
        public DbSet<Vehicle> Vehicles { get; set; }

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
        /// Gets or sets the route schedules.
        /// </summary>
        /// <value>The route schedules.</value>
        public DbSet<RouteSchedule> RouteSchedules { get; set; }

        /// <summary>
        /// Gets or sets the operators.
        /// </summary>
        /// <value>The operators.</value>
        public DbSet<Operator> Operators { get; set; }

        /// <summary>
        /// Gets or sets the vehicle schedules.
        /// </summary>
        /// <value>The vehicle schedules.</value>
        public DbSet<VehicleSchedule> VehicleSchedules { get; set; }
    }
}