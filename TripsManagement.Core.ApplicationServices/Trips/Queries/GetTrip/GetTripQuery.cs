using System;
using System.Collections.Generic;
using MediatR;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.DomainModels.Trips;

namespace TripsManagement.Core.ApplicationServices.Trips.Queries.GetTrip
{
    /// <summary>
    /// <see cref="GetTripHandler"/>
    /// </summary>
    public class GetTripQuery : IRequest<RequestResult<TripQuery>>
    {
        public Guid TripId { get; set; }
    }
}
