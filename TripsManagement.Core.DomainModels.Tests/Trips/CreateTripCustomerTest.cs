using FluentAssertions;
using System;
using System.Threading.Tasks;
using TripsManagement.Core.DomainModels.Customers;
using TripsManagement.Core.DomainModels.Exceptions;
using TripsManagement.Core.DomainModels.Trips;
using TripsManagement.Core.DomainModels.ValueObjects;
using Xunit;

namespace TripsManagement.Core.DomainModels.Tests.Trips
{
    public class CreateTripCustomerTest
    {
        [Fact]
        public async Task CustomerTrip_CreateWithNullTrip_ReturnError()
        {
            // arrange
            Trip trip = null;
            Customer customer = Customer.Create("a","b",new HumanAgeField(10), new MobileNumber("+4422121"));

            // act
            Action comparison = () =>
            {
                var tripCustomer = TripCustomer.Create(trip, customer);
            };

            // assert
            comparison.Should().Throw<InvalidEntityStateException>();
        }

        [Fact]
        public async Task CustomerTrip_CreateWithNullCusomter_ReturnError()
        {
            // arrange
            Trip trip = Trip.Create("aaa", DateTime.Now, DateTime.Now.AddDays(2));
            Customer customer = null;

            // act
            Action comparison = () =>
            {
                var tripCustomer = TripCustomer.Create(trip, customer);
            };

            // assert
            comparison.Should().Throw<InvalidEntityStateException>();
        }

    }
}
