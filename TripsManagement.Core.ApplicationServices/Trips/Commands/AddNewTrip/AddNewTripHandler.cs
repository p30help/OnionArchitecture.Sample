using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.ApplicationServices.Common.Events;
using TripsManagement.Core.DomainModels.Contracts;
using TripsManagement.Core.DomainModels.Trips;

namespace TripsManagement.Core.ApplicationServices.Trips.Commands.AddNewTrip
{
    public class AddNewTripHandler : IRequestHandler<AddNewTripCommand, RequestResult<Guid>>
    {
        private readonly ITripsUnitOfWork _unitOfWork;
        private readonly IEventDispatcher _eventDispatcher;

        public AddNewTripHandler(ITripsUnitOfWork unitOfWork, IEventDispatcher eventDispatcher)
        {
            _unitOfWork = unitOfWork;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<RequestResult<Guid>> Handle(AddNewTripCommand request, CancellationToken cancellationToken)
        {
            var trip = Trip.Create(request.Title, request.StartDate, request.FinishDate);

            await _unitOfWork.TripRepository.InsertAsync(trip);
            await _unitOfWork.CommitAsync();

             await _eventDispatcher.PublishDomainEventsAsync(trip.GetEvents());

            return RequestResult<Guid>.Ok(trip.Id.Value) ;
        }
    }
}
