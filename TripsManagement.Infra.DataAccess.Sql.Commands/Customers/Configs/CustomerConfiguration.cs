using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripsManagement.Core.DomainModels.Customers;
using TripsManagement.Core.DomainModels.ValueObjects;

namespace TripsManagement.Infra.DataAccess.Sql.Commands.Customers.Configs
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value,
                x => BusinessId.FromGuid(x));

            builder.Property(x => x.Mobile)
                .HasConversion(x => x.Value,
                    x => new MobileNumber(x));

            builder.Property(x => x.Age)
                .HasConversion(x => x.Value,
                    x => new HumanAgeField(x));

            builder.Property(x => x.FirstName).HasMaxLength(50);

            builder.Property(x => x.LastName).HasMaxLength(50);
        }
    }
}