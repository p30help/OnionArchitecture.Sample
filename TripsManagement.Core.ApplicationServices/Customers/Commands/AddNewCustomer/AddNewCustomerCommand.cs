using MediatR;
using System;
using TripsManagement.Core.ApplicationServices.Common;

namespace TripsManagement.Core.ApplicationServices.Customers.Commands.AddNewCustomer
{
    /// <summary>
    /// <see cref="AddNewCustomerHandler"/>
    /// </summary>
    public class AddNewCustomerCommand : IRequest<RequestResult<Guid>>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public short Age { get; set; }

        public string Mobile { get; set; }
    }
}
