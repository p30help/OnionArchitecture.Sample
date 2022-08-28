using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripsManagement.Core.DomainModels.Common.Events;

namespace TripsManagement.Core.DomainModels.Trips.Events
{
    public class TripCreated : IDomainEvent
    {
        public Guid TripId { get; set; }
    }
}
