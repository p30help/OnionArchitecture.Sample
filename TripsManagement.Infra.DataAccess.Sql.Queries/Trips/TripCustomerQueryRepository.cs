using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripsManagement.Core.DomainModels.Contracts.Trips;
using TripsManagement.Core.DomainModels.Trips;
using TripsManagement.Infra.DataAccess.Sql.Queries.Common;

namespace TripsManagement.Infra.DataAccess.Sql.Queries.Trips
{
    public class TripCustomerQueryRepository : ITripCustomerQueryRepository
    {
        private readonly TripsQueryDbContext _context;

        public TripCustomerQueryRepository(TripsQueryDbContext context)
        {
            _context = context;
        }

        public Task<List<TripCustomerQuery>> GetByTripIdAsync(Guid tripId)
        {
            return _context.TripsCustomers
                .AsNoTracking()
                .Include(x => x.Customer)
                .Include(x => x.Trip)
                .Where(x => x.TripId == tripId)
                .ToListAsync();
        }
    }
}