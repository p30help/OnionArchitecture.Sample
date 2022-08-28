using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripsManagement.Core.DomainModels.Trips;
using TripsManagement.Core.DomainModels.ValueObjects;

namespace TripsManagement.Infra.DataAccess.Sql.Commands.Trips.Configs
{
    public class TripCustomerConfiguration : IEntityTypeConfiguration<TripCustomer>
    {
        public void Configure(EntityTypeBuilder<TripCustomer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value,
                    x => BusinessId.FromGuid(x));
        }
    }
}