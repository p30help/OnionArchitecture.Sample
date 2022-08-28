using Microsoft.EntityFrameworkCore;
using TripsManagement.Core.DomainModels.Customers;
using TripsManagement.Core.DomainModels.Trips;

namespace TripsManagement.Infra.DataAccess.Sql.Queries.Common
{
    public class TripsQueryDbContext : DbContext
    {
        public TripsQueryDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<TripQuery> Trips { get; set; }

        public DbSet<CustomerQuery> Customers { get; set; }

        public DbSet<TripCustomerQuery> TripsCustomers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(builder);
        }
    }
}
