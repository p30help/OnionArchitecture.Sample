using System;
using MediatR;
using TripsManagement.Core.ApplicationServices.Common;

namespace TripsManagement.Core.ApplicationServices.Customers.Commands.EditCustomer
{
    /// <summary>
    /// <see cref="EditCustomerHandler"/>
    /// </summary>
    public class EditCustomerCommand : IRequest<RequestResult>
    {
        public Guid CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public short Age { get; set; }

        public string Mobile { get; set; }
    }
}
