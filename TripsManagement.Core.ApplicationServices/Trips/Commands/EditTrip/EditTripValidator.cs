using FluentValidation;
using System;

namespace TripsManagement.Core.ApplicationServices.Trips.Commands.EditTrip
{
    public class EditTripValidator : AbstractValidator<EditTripCommand>
    {
        public EditTripValidator()
        {
            RuleFor(x => x.StartDate).NotEqual(new DateTime())
                .WithMessage("Please enter Start Date");

            RuleFor(x => x.FinishDate).NotEqual(new DateTime())
                .WithMessage("Please enter Finish Date");
        }
    }
}
