using System.Linq;
using Microsoft.EntityFrameworkCore;
using TripsManagement.Core.DomainModels.Customers;
using TripsManagement.Core.DomainModels.Trips;

namespace TripsManagement.Infra.DataAccess.Sql.Commands.Common
{
    public class TripsDbContext : DbContext
    {
        public TripsDbContext(DbContextOptions<TripsDbContext> options) : base(options)
        {
        }

        public TripsDbContext()
        {
        }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<TripCustomer> TripsCustomers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        }
    }
}
