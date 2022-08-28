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
    public class TripQueryRepository : ITripQueryRepository
    {
        private readonly TripsQueryDbContext _context;

        public TripQueryRepository(TripsQueryDbContext context)
        {
            _context = context;
        }

        public Task<TripQuery> GetAsync(Guid id)
        {
            return _context.Trips
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<TripQuery>> GetAllAsync()
        {
            return _context.Trips
                .AsNoTracking()
                .OrderByDescending(x => x.RecordDate)
                .ToListAsync();
        }
    }
}