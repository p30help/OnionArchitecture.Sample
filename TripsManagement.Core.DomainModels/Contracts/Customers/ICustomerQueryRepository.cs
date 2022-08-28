using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripsManagement.Core.DomainModels.Customers;

namespace TripsManagement.Core.DomainModels.Contracts.Customers
{
    public interface ICustomerQueryRepository
    {
        Task<CustomerQuery> GetAsync(Guid id);

        Task<List<CustomerQuery>> GetAllAsync();
    }
}