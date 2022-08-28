using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.DomainModels.Contracts;
using TripsManagement.Core.DomainModels.Exceptions;

namespace TripsManagement.Core.ApplicationServices.Trips.Commands.EditTrip
{
    public class EditTripHandler : IRequestHandler<EditTripCommand, RequestResult>
    {
        private readonly ITripsUnitOfWork _unitOfWork;

        public EditTripHandler(ITripsUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResult> Handle(EditTripCommand request, CancellationToken cancellationToken)
        {
            var trip = await _unitOfWork.TripRepository.GetAsync(request.TripId);

            if (trip == null)
            {
                throw new InvalidEntityStateException("Trip not found");
            }

            trip.SetDate(request.StartDate, request.FinishDate);
            trip.SetCanceled(request.IsCanceled);
            trip.SetTitle(request.Title);

            await _unitOfWork.CommitAsync();

            return RequestResult.Ok();
        }
    }
}
