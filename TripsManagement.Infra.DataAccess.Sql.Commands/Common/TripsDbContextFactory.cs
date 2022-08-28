using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TripsManagement.Infra.DataAccess.Sql.Commands.Common
{
    public class MainDbContextFactory : IDesignTimeDbContextFactory<TripsDbContext>
    {
        public TripsDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TripsDbContext>();
            builder.UseSqlServer("Data Source=.;Initial Catalog=TripsMngDev;Trusted_Connection=True;MultipleActiveResultSets=True;");
            return new TripsDbContext(builder.Options);
        }
    }
}