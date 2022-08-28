using System;
using System.Threading.Tasks;
using TripsManagement.Core.DomainModels.Customers;
using TripsManagement.Core.DomainModels.ValueObjects;

namespace TripsManagement.Core.DomainModels.Contracts.Customers
{
    public interface ICustomerRepository
    {
        Task<Customer> GetAsync(BusinessId id);

        Task InsertAsync(Customer id);

        Task DeleteAsync(BusinessId id);
    }
}
