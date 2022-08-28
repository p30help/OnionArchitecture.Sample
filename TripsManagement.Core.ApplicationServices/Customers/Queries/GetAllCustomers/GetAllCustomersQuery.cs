using MediatR;
using System.Collections.Generic;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.DomainModels.Customers;

namespace TripsManagement.Core.ApplicationServices.Customers.Queries.GetAllCustomers
{
    /// <summary>
    /// <see cref="GetAllCustomersHandler"/>
    /// </summary>
    public class GetAllCustomersQuery : IRequest<RequestResult<List<CustomerQuery>>>
    {
    }
}
