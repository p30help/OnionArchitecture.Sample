using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.ApplicationServices.Common.Events;
using TripsManagement.Core.DomainModels.Contracts;
using TripsManagement.Core.DomainModels.Exceptions;

namespace TripsManagement.Core.ApplicationServices.Trips.Commands.AddCustomerToTrip
{
    public class AddCustomerToTripHandler : IRequestHandler<AddCustomerToTripCommand, RequestResult<Guid>>
    {
        private readonly ITripsUnitOfWork _unitOfWork;

        public AddCustomerToTripHandler(ITripsUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResult<Guid>> Handle(AddCustomerToTripCommand request, CancellationToken cancellationToken)
        {
            var trip = await _unitOfWork.TripRepository.GetAsync(request.TripId);
            var customer = await _unitOfWork.CustomerRepository.GetAsync(request.CustomerId);

            if (trip == null)
            {
                throw new InvalidEntityStateException("Trip not found");
            }

            if (customer == null)
            {
                throw new InvalidEntityStateException("Customer not found");
            }

            trip.AddCustomer(customer);

            await _unitOfWork.CommitAsync();

            return RequestResult<Guid>.Ok(trip.Id.Value);
        }
    }
}
