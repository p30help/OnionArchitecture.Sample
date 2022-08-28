using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripsManagement.Core.DomainModels.Trips;

namespace TripsManagement.Core.DomainModels.Contracts.Trips
{
    public interface ITripQueryRepository
    {
        Task<TripQuery> GetAsync(Guid id);

        Task<List<TripQuery>> GetAllAsync();
    }
}