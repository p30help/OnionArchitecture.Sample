using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.DomainModels.Contracts.Customers;
using TripsManagement.Core.DomainModels.Customers;

namespace TripsManagement.Core.ApplicationServices.Customers.Queries.GetCustomer
{
    public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, RequestResult<CustomerQuery>>
    {
        private readonly ICustomerQueryRepository _queryRepository;

        public GetCustomerHandler(ICustomerQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<RequestResult<CustomerQuery>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var item = await _queryRepository.GetAsync(request.CustomerId);

            return RequestResult<CustomerQuery>.Ok(item);
        }
    }
}
