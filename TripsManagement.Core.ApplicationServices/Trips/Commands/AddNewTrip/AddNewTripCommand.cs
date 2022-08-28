using System;
using MediatR;
using TripsManagement.Core.ApplicationServices.Common;

namespace TripsManagement.Core.ApplicationServices.Trips.Commands.AddNewTrip
{
    /// <summary>
    /// <see cref="AddNewTripHandler"/>
    /// </summary>
    public class AddNewTripCommand : IRequest<RequestResult<Guid>>
    {
        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }
    }
}
