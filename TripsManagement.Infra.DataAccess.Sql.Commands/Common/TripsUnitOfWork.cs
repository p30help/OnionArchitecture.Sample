using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TripsManagement.Core.DomainModels.Contracts;
using TripsManagement.Core.DomainModels.Contracts.Customers;
using TripsManagement.Core.DomainModels.Contracts.Trips;
using TripsManagement.Infra.DataAccess.Sql.Commands.Customers;
using TripsManagement.Infra.DataAccess.Sql.Commands.Trips;

namespace TripsManagement.Infra.DataAccess.Sql.Commands.Common
{
    public class TripsUnitOfWork : ITripsUnitOfWork
    {
        private readonly TripsDbContext _context;

        public TripsUnitOfWork(TripsDbContext dbContext)
        {
            _context = dbContext;
            TripRepository = new TripRepository(dbContext);

            CustomerRepository = new CustomerRepository(dbContext);
        }

        public ITripRepository TripRepository { get; set; }

        public ICustomerRepository CustomerRepository { get; set; }
        
        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}

