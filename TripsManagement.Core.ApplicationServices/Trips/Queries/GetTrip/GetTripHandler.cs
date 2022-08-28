using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.DomainModels.Contracts.Trips;
using TripsManagement.Core.DomainModels.Trips;

namespace TripsManagement.Core.ApplicationServices.Trips.Queries.GetTrip
{
    public class GetTripHandler : IRequestHandler<GetTripQuery, RequestResult<TripQuery>>
    {
        private readonly ITripQueryRepository _queryRepository;

        public GetTripHandler(ITripQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<RequestResult<TripQuery>> Handle(GetTripQuery request, CancellationToken cancellationToken)
        {
            var item = await _queryRepository.GetAsync(request.TripId);

            return RequestResult<TripQuery>.Ok(item);
        }
    }
}
