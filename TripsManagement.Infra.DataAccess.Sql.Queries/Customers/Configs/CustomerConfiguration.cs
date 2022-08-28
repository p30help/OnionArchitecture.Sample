using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripsManagement.Core.DomainModels.Customers;

namespace TripsManagement.Infra.DataAccess.Sql.Queries.Customers.Configs
{
    public class CustomerConfiguration : IEntityTypeConfiguration<CustomerQuery>
    {
        public void Configure(EntityTypeBuilder<CustomerQuery> builder)
        {
        }
    }
}