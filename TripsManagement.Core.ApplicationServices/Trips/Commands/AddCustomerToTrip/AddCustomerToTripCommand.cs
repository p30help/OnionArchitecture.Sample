using System;
using MediatR;
using TripsManagement.Core.ApplicationServices.Common;

namespace TripsManagement.Core.ApplicationServices.Trips.Commands.AddCustomerToTrip
{
    /// <summary>
    /// <see cref="AddCustomerToTripHandler"/>
    /// </summary>
    public class AddCustomerToTripCommand : IRequest<RequestResult<Guid>>
    {
        public Guid TripId { get; set; }

        public Guid CustomerId { get; set; }
    }
}
