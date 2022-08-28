using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TripsManagement.Core.DomainModels.Contracts.Customers;
using TripsManagement.Core.DomainModels.Customers;
using TripsManagement.Core.DomainModels.ValueObjects;
using TripsManagement.Infra.DataAccess.Sql.Commands.Common;

namespace TripsManagement.Infra.DataAccess.Sql.Commands.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TripsDbContext _context;

        public CustomerRepository(TripsDbContext context)
        {
            _context = context;
        }

        public Task<Customer> GetAsync(BusinessId id)
        {
            return _context.Customers
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public async Task DeleteAsync(BusinessId id)
        {
            var item = await _context.Customers
                .SingleOrDefaultAsync(x => x.Id == id);

            _context.Customers.Remove(item);
        }
    }
}