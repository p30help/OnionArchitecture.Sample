using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripsManagement.Core.DomainModels.Common.Events;

namespace TripsManagement.Core.DomainModels.Customers.Events
{
    public class CustomerCreated : IDomainEvent
    {
        public Guid CustomerId { get; set; }
    }
}
