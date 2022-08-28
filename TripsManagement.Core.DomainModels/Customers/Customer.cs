using System;
using TripsManagement.Core.DomainModels.Common;
using TripsManagement.Core.DomainModels.Customers.Events;
using TripsManagement.Core.DomainModels.Exceptions;
using TripsManagement.Core.DomainModels.ValueObjects;

namespace TripsManagement.Core.DomainModels.Customers
{
    public class Customer : AggregateRoot
    {
        public DateTime RecordDate { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public HumanAgeField Age { get; private set; }

        public MobileNumber Mobile { get; private set; }

        private Customer() { }

        public static Customer Create(string firstName, string lastName, HumanAgeField age,
            MobileNumber mobile)
        {
            var item = new Customer()
            {
                RecordDate = DateTime.Now
            };

            item.SetFirstName(firstName);
            item.SetLastName(lastName);
            item.SetMobileNumber(mobile);
            item.SetAge(age);

            // event
            var createEvent = new CustomerCreated
            {
                CustomerId = item.Id.Value
            };
            item.AddEvent(createEvent);

            return item;
        }

        public void SetFirstName(string fname)
        {
            if (string.IsNullOrWhiteSpace(fname))
            {
                throw new InvalidEntityStateException("First name must be entered");
            }

            this.FirstName = fname;
        }

        public void SetLastName(string lname)
        {
            if (string.IsNullOrWhiteSpace(lname))
            {
                throw new InvalidEntityStateException("First name must be entered");
            }

            this.LastName = lname;
        }

        public void SetAge(HumanAgeField age)
        {
            if (age == null)
            {
                throw new InvalidEntityStateException("Age must be entered");
            }

            this.Age = age;
        }

        public void SetMobileNumber(MobileNumber mobile)
        {
            if (mobile == null)
            {
                throw new InvalidEntityStateException("Mobile must be entered");
            }

            this.Mobile = mobile;
        }

    }
}