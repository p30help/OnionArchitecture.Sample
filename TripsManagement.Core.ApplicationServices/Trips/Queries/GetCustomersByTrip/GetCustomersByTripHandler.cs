using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.DomainModels.Contracts.Trips;
using TripsManagement.Core.DomainModels.Customers;

namespace TripsManagement.Core.ApplicationServices.Trips.Queries.GetCustomersByTrip
{
    public class GetCustomersByTripHandler : IRequestHandler<GetCustomersByTripQuery, RequestResult<List<CustomerQuery>>>
    {
        private readonly ITripCustomerQueryRepository _queryRepository;

        public GetCustomersByTripHandler(ITripCustomerQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<RequestResult<List<CustomerQuery>>> Handle(GetCustomersByTripQuery request, CancellationToken cancellationToken)
        {
            var list = await _queryRepository.GetByTripIdAsync(request.TripId);

            return RequestResult<List<CustomerQuery>>.Ok(list.Select(x => x.Customer).ToList());
        }
    }
}
