using System.Threading.Tasks;
using TripsManagement.Core.DomainModels.Contracts.Customers;
using TripsManagement.Core.DomainModels.Contracts.Trips;

namespace TripsManagement.Core.DomainModels.Contracts
{
    public interface ITripsUnitOfWork 
    {
        ITripRepository TripRepository { get; set; }

        ICustomerRepository CustomerRepository { get; set; }

        Task<int> CommitAsync();
    }
}