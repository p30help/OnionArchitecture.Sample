using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.DomainModels.Contracts;
using TripsManagement.Core.DomainModels.Exceptions;
using TripsManagement.Core.DomainModels.ValueObjects;

namespace TripsManagement.Core.ApplicationServices.Customers.Commands.EditCustomer
{
    public class EditCustomerHandler : IRequestHandler<EditCustomerCommand, RequestResult>
    {
        private readonly ITripsUnitOfWork _unitOfWork;

        public EditCustomerHandler(ITripsUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResult> Handle(EditCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetAsync(request.CustomerId);

            if (customer == null)
            {
                throw new InvalidEntityStateException("Customer not found");
            }

            customer.SetFirstName(request.FirstName);
            customer.SetLastName(request.LastName);
            customer.SetAge(new HumanAgeField(request.Age));
            customer.SetMobileNumber(new MobileNumber(request.Mobile));

            await _unitOfWork.CommitAsync();

            return RequestResult.Ok();
        }
    }
}
