using System;
using MediatR;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.DomainModels.Customers;

namespace TripsManagement.Core.ApplicationServices.Customers.Queries.GetCustomer
{
    /// <summary>
    /// <see cref="GetCustomerHandler"/>
    /// </summary>
    public class GetCustomerQuery : IRequest<RequestResult<CustomerQuery>>
    {
        public Guid CustomerId { get; set; }
    }
}
