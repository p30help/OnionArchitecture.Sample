using System;
using TripsManagement.Core.DomainModels.Common;
using TripsManagement.Core.DomainModels.ValueObjects;

namespace TripsManagement.Core.DomainModels.Customers
{
    public class CustomerQuery
    {
        public Guid Id { get; set; }

        public DateTime RecordDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public short? Age { get; set; }

        public string Mobile { get; set; }

        public string FullName
        {
            get
            {
                return ($"{FirstName} {LastName}").Trim();
            }
        }
    }
}