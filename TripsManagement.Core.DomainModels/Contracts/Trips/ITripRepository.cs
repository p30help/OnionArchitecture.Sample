using System;
using System.Threading.Tasks;
using TripsManagement.Core.DomainModels.Trips;
using TripsManagement.Core.DomainModels.ValueObjects;

namespace TripsManagement.Core.DomainModels.Contracts.Trips
{
    public interface ITripRepository
    {
        Task<Trip> GetAsync(BusinessId id);

        Task InsertAsync(Trip trip);

        Task DeleteAsync(BusinessId id);
    }
}
