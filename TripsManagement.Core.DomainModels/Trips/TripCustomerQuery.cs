using System;
using TripsManagement.Core.DomainModels.Customers;

namespace TripsManagement.Core.DomainModels.Trips
{
    public class TripCustomerQuery
    {
        public Guid Id { get; set; }

        public  Guid CustomerId { get; set; }

        public Guid TripId { get; set; }

        public DateTime RecordDate { get; set; }

        public CustomerQuery Customer { get; set; }

        public TripQuery Trip { get; set; }


    }
}