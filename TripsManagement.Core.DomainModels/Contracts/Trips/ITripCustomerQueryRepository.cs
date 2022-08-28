using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripsManagement.Core.DomainModels.Trips;

namespace TripsManagement.Core.DomainModels.Contracts.Trips
{
    public interface ITripCustomerQueryRepository
    {
        Task<List<TripCustomerQuery>> GetByTripIdAsync(Guid tripId);
    }
}