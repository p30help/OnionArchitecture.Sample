using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace TripsManagement.Core.ApplicationServices.Trips.Commands.AddNewTrip
{
    public class AddNewTripValidator : AbstractValidator<AddNewTripCommand>
    {
        public AddNewTripValidator()
        {
            RuleFor(x => x.StartDate).NotEqual(new DateTime())
                .WithMessage("Please enter Start Date");

            RuleFor(x => x.FinishDate).NotEqual(new DateTime())
                .WithMessage("Please enter Finish Date");
        }
    }
}
