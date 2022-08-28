using System;
using TripsManagement.Core.DomainModels.Common;
using TripsManagement.Core.DomainModels.Customers;
using TripsManagement.Core.DomainModels.Exceptions;

namespace TripsManagement.Core.DomainModels.Trips
{
    public class TripCustomer : Entity
    {
        public DateTime RecordDate { get; private set; }

        public Customer Customer { get; private set; }

        public Trip Trip { get; private set; }

        private TripCustomer() { }

        public static TripCustomer Create(Trip trip, Customer customer)
        {
            if (trip == null)
            {
                throw new InvalidEntityStateException("Trip must be entered");
            }

            if (customer == null)
            {
                throw new InvalidEntityStateException("Customer must be entered");
            }

            var item = new TripCustomer()
            {
                RecordDate = DateTime.Now,
                Customer = customer,
                Trip = trip
            };

            return item;
        }

        
    }
}