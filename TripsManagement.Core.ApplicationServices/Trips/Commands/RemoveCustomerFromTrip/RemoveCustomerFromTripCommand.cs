using System;
using MediatR;
using TripsManagement.Core.ApplicationServices.Common;

namespace TripsManagement.Core.ApplicationServices.Trips.Commands.RemoveCustomerFromTrip
{
    /// <summary>
    /// <see cref="RemoveCustomerFromTripHandler"/>
    /// </summary>
    public class RemoveCustomerFromTripCommand : IRequest<RequestResult>
    {
        public Guid TripId { get; set; }

        public Guid CustomerId { get; set; }
    }
}
