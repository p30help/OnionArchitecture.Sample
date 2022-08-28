using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using TripsManagement.Core.DomainModels.Common;
using TripsManagement.Core.DomainModels.Customers;
using TripsManagement.Core.DomainModels.Exceptions;
using TripsManagement.Core.DomainModels.Trips.Events;
using TripsManagement.Core.DomainModels.ValueObjects;

namespace TripsManagement.Core.DomainModels.Trips
{
    public class Trip : AggregateRoot
    {
        public DateTime RecordDate { get; private set; }

        public bool IsCanceled { get; private set; }

        public string Title { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime FinishDate { get; private set; }

        private List<TripCustomer> _tripCustomers = new List<TripCustomer>();
        public IReadOnlyCollection<TripCustomer> TripCustomers
        {
            get
            {
                return _tripCustomers.AsReadOnly();
            }
        }

        private Trip() { }

        public static Trip Create(string title, DateTime start, DateTime finish)
        {
            var item = new Trip()
            {
                RecordDate = DateTime.Now,
                IsCanceled = false
            };

            item.SetTitle(title);
            item.SetDate(start, finish);

            // event 
            var tripCreated = new TripCreated()
            {
                TripId = item.Id.Value
            };
            item.AddEvent(tripCreated);

            return item;
        }

        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new InvalidEntityStateException("Title must be entered");
            }

            this.Title = title;
        }

        public void SetDate(DateTime start, DateTime finish)
        {
            if (start >= finish)
            {
                throw new InvalidEntityStateException("Start date of trip must be less than finish date");
            }

            this.StartDate = start;
            this.FinishDate = finish;
        }

        public void SetCanceled(bool canceled)
        {
            this.IsCanceled = canceled;
        }

        public void AddCustomer(Customer customer)
        {

            if (IsCanceled)
            {
                throw new InvalidEntityStateException("The trip is canceled");
            }

            if (_tripCustomers.Any(x => x.Customer.Id == customer.Id))
            {
                throw new InvalidEntityStateException("This customer has already been added to this trip");
            }


            var item = TripCustomer.Create(this, customer);

            _tripCustomers.Add(item);
        }

        public void RemoveCustomer(Customer customer)
        {
            var item = _tripCustomers.FirstOrDefault(x => x.Customer.Id == customer.Id);
            if (item == null)
            {
                throw new InvalidEntityStateException("This customer doesn't exist in this trip");
            }

            _tripCustomers.Remove(item);
        }
    }
}