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
    public class CreateTripTest
    {
        [Fact]
        public async Task Trip_CreateWithNoTitle_ReturnError()
        {
            // arrange
            var startDate = DateTime.Now;
            var finishDate = DateTime.Now.AddDays(5);

            // act
            Action comparison = () =>
            {
                var trip = Trip.Create(null, startDate, finishDate);
            };

            // assert
            comparison.Should().Throw<InvalidEntityStateException>();
        }

        [Theory]
        [InlineData("2020/01/20", "2020/01/20")]
        [InlineData("2020/01/20", "2020/01/10")]
        public void Trip_CreateWithWrongDate_ReturnError(string startDateStr, string finishDateStr)
        {
            // arrange
            string title = "test";
            var startDate = DateTime.Parse(startDateStr);
            var finishDate = DateTime.Parse(finishDateStr);

            // act
            Action comparison = () =>
            {
                var trip = Trip.Create(title, startDate, finishDate);
            };

            // assert
            comparison.Should().Throw<InvalidEntityStateException>();
        }

        [Fact]
        public async Task Trip_AddDuplicatedCustomer_ReturnError()
        {
            // arrange
            var startDate = DateTime.Now;
            var finishDate = DateTime.Now.AddDays(5);
            var trip = Trip.Create("test", startDate, finishDate);
            var customer = Customer.Create("name", "lastname", new HumanAgeField(10), new MobileNumber("+986542124") );
            trip.AddCustomer(customer);

            // act
            Action comparison = () =>
            {
                trip.AddCustomer(customer);
            };

            // assert
            comparison.Should().Throw<InvalidEntityStateException>();
        }

        [Fact]
        public async Task Trip_AddCustomerToCanceledTrip_ReturnError()
        {
            // arrange
            var startDate = DateTime.Now;
            var finishDate = DateTime.Now.AddDays(5);
            var trip = Trip.Create("test", startDate, finishDate);
            trip.SetCanceled(true);
            var customer = Customer.Create("name", "lastname", new HumanAgeField(10), new MobileNumber("+986542124"));
            
            // act
            Action comparison = () =>
            {
                trip.AddCustomer(customer);
            };

            // assert
            comparison.Should().Throw<InvalidEntityStateException>();
        }

        [Fact]
        public async Task Trip_RemoveCustomerFromTripWhenAlreadyRemoved_ReturnError()
        {
            // arrange
            var startDate = DateTime.Now;
            var finishDate = DateTime.Now.AddDays(5);
            var trip = Trip.Create("test", startDate, finishDate);
            var customer = Customer.Create("name", "lastname", new HumanAgeField(10), new MobileNumber("+986542124"));

            // act
            Action comparison = () =>
            {
                trip.RemoveCustomer(customer);
            };

            // assert
            comparison.Should().Throw<InvalidEntityStateException>();
        }
    }
}
