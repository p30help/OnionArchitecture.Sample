using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.DomainModels.Contracts;
using TripsManagement.Core.DomainModels.Exceptions;

namespace TripsManagement.Core.ApplicationServices.Trips.Commands.RemoveCustomerFromTrip
{
    public class RemoveCustomerFromTripHandler : IRequestHandler<RemoveCustomerFromTripCommand, RequestResult>
    {
        private readonly ITripsUnitOfWork _unitOfWork;

        public RemoveCustomerFromTripHandler(ITripsUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResult> Handle(RemoveCustomerFromTripCommand request, CancellationToken cancellationToken)
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

            trip.RemoveCustomer(customer);

            await _unitOfWork.CommitAsync();

            return RequestResult.Ok();
        }
    }
}
