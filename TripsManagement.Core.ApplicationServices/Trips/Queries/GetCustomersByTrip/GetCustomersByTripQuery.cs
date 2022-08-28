using MediatR;
using System;
using System.Collections.Generic;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.DomainModels.Customers;

namespace TripsManagement.Core.ApplicationServices.Trips.Queries.GetCustomersByTrip
{
    /// <summary>
    /// <see cref="GetCustomersByTripHandler"/>
    /// </summary>
    public class GetCustomersByTripQuery : IRequest<RequestResult<List<CustomerQuery>>>
    {
        public Guid TripId { get; set; }
    }
}
