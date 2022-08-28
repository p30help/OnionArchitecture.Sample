using System;
using MediatR;
using TripsManagement.Core.ApplicationServices.Common;

namespace TripsManagement.Core.ApplicationServices.Trips.Commands.EditTrip
{
    /// <summary>
    /// <see cref="EditTripHandler"/>
    /// </summary>
    public class EditTripCommand : IRequest<RequestResult>
    {
        public Guid TripId { get; set; }

        public bool IsCanceled { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }
    }
}
