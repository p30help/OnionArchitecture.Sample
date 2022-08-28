using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.DomainModels.Trips;

namespace TripsManagement.Core.ApplicationServices.Trips.Queries.GetAllTrips
{
    /// <summary>
    /// <see cref="GetAllTripsHandler"/>
    /// </summary>
    public class GetAllTripsQuery : IRequest<RequestResult<List<TripQuery>>>
    {
    }
}
