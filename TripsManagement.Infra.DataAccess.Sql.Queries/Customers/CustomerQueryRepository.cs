using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripsManagement.Core.DomainModels.Contracts.Customers;
using TripsManagement.Core.DomainModels.Customers;
using TripsManagement.Infra.DataAccess.Sql.Queries.Common;

namespace TripsManagement.Infra.DataAccess.Sql.Queries.Customers
{
    public class CustomerQueryRepository : ICustomerQueryRepository
    {
        private readonly TripsQueryDbContext _context;

        public CustomerQueryRepository(TripsQueryDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerQuery> GetAsync(Guid id)
        {
            return await _context.Customers
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CustomerQuery>> GetAllAsync()
        {
            return await _context.Customers
                .AsNoTracking()
                .ToListAsync();
        }
    }
}