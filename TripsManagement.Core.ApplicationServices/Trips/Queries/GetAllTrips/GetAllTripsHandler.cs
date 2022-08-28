using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.DomainModels.Contracts.Trips;
using TripsManagement.Core.DomainModels.Trips;

namespace TripsManagement.Core.ApplicationServices.Trips.Queries.GetAllTrips
{
    public class GetAllTripsHandler : IRequestHandler<GetAllTripsQuery, RequestResult<List<TripQuery>>>
    {
        private readonly ITripQueryRepository _queryRepository;

        public GetAllTripsHandler(ITripQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<RequestResult<List<TripQuery>>> Handle(GetAllTripsQuery request, CancellationToken cancellationToken)
        {
            var list = await _queryRepository.GetAllAsync();

            return RequestResult<List<TripQuery>>.Ok(list);
        }
    }
}
