using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripsManagement.Core.DomainModels.Trips;

namespace TripsManagement.Infra.DataAccess.Sql.Queries.Trips.Configs
{
    public class TripConfiguration : IEntityTypeConfiguration<TripQuery>
    {
        public void Configure(EntityTypeBuilder<TripQuery> builder)
        {
            
        }
    }
}