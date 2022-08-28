using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TripsManagement.Core.DomainModels.Contracts.Trips;
using TripsManagement.Core.DomainModels.Trips;
using TripsManagement.Core.DomainModels.ValueObjects;
using TripsManagement.Infra.DataAccess.Sql.Commands.Common;

namespace TripsManagement.Infra.DataAccess.Sql.Commands.Trips
{
    public class TripRepository : ITripRepository
    {
        private readonly TripsDbContext _context;

        public TripRepository(TripsDbContext context)
        {
            _context = context;
        }

        public Task<Trip> GetAsync(BusinessId id)
        {
            return _context.Trips
                .Include(x => x.TripCustomers).ThenInclude(x => x.Customer)
                .Include(x => x.TripCustomers).ThenInclude(x => x.Trip)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(Trip trip)
        {
            await _context.Trips.AddAsync(trip);
        }

        public async Task DeleteAsync(BusinessId id)
        {
            var item = await _context.Trips
                .Include(x => x.TripCustomers).ThenInclude(x => x.Customer)
                .Include(x => x.TripCustomers).ThenInclude(x => x.Trip)
                .SingleOrDefaultAsync(x => x.Id == id);

            _context.Trips.Remove(item);
        }
    }
}