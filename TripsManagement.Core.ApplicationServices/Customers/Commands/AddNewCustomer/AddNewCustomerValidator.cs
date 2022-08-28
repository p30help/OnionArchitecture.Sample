using System;
using FluentValidation;

namespace TripsManagement.Core.ApplicationServices.Customers.Commands.AddNewCustomer
{
    public class AddNewCustomerValidator : AbstractValidator<AddNewCustomerCommand>
    {
        public AddNewCustomerValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty()
                .WithMessage("Please enter First Name");

            RuleFor(x => x.LastName).NotEmpty()
                .WithMessage("Please enter Last Name");
        }
    }
}
