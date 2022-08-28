using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.DomainModels.Contracts.Customers;
using TripsManagement.Core.DomainModels.Customers;

namespace TripsManagement.Core.ApplicationServices.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, RequestResult<List<CustomerQuery>>>
    {
        private readonly ICustomerQueryRepository _queryRepository;

        public GetAllCustomersHandler(ICustomerQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<RequestResult<List<CustomerQuery>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var list = await _queryRepository.GetAllAsync();

            return RequestResult<List<CustomerQuery>>.Ok(list);
        }
    }
}
