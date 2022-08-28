using System;
using System.Collections.Generic;

namespace TripsManagement.Core.DomainModels.Trips
{
    public class TripQuery
    {
        public Guid Id { get; set; }

        public DateTime RecordDate { get; set; }

        public bool IsCanceled { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public ICollection<TripCustomerQuery> TripCustomers { get; set; }

    }
}