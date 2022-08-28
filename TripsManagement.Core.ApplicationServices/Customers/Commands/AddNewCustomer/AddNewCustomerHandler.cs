using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.DomainModels.Contracts;
using TripsManagement.Core.DomainModels.Customers;
using TripsManagement.Core.DomainModels.ValueObjects;

namespace TripsManagement.Core.ApplicationServices.Customers.Commands.AddNewCustomer
{
    public class AddNewCustomerHandler : IRequestHandler<AddNewCustomerCommand, RequestResult<Guid>>
    {
        private readonly ITripsUnitOfWork _unitOfWork;

        public AddNewCustomerHandler(ITripsUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResult<Guid>> Handle(AddNewCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = Customer.Create(request.FirstName, request.LastName,
                new HumanAgeField(request.Age), new MobileNumber(request.Mobile));

            await _unitOfWork.CustomerRepository.InsertAsync(customer);
            await _unitOfWork.CommitAsync();

            return RequestResult<Guid>.Ok(customer.Id.Value);
        }
    }
}
