using FluentValidation;

namespace TripsManagement.Core.ApplicationServices.Customers.Commands.EditCustomer
{
    public class EditCustomerValidator : AbstractValidator<EditCustomerCommand>
    {
        public EditCustomerValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty()
                .WithMessage("Please enter First Name");

            RuleFor(x => x.LastName).NotEmpty()
                .WithMessage("Please enter Last Name");
        }
    }
}
