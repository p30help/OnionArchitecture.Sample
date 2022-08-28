using System;
using System.Threading.Tasks;
using FluentAssertions;
using TripsManagement.Core.DomainModels.Customers;
using TripsManagement.Core.DomainModels.Exceptions;
using TripsManagement.Core.DomainModels.Trips;
using TripsManagement.Core.DomainModels.ValueObjects;
using Xunit;

namespace TripsManagement.Core.DomainModels.Tests.Customers
{
    public class CreateCustomerTest
    {
        [Fact]
        public async Task Customer_CreateWithNullFirstName_ReturnError()
        {
            // arrange
            string firstName = null;
            string lastName = "pitt";
            var age = new HumanAgeField(50);
            var mobile = new MobileNumber("+989122351212");

            // act
            Action comparison = () =>
            {
                var trip = Customer.Create(firstName, lastName, age, mobile);
            };

            // assert
            comparison.Should().Throw<InvalidEntityStateException>();
        }

        [Fact]
        public async Task Customer_CreateWithNullLastName_ReturnError()
        {
            // arrange
            string firstName = "james";
            string lastName = null;
            var age = new HumanAgeField(50);
            var mobile = new MobileNumber("+989122351212");

            // act
            Action comparison = () =>
            {
                var trip = Customer.Create(firstName, lastName, age, mobile);
            };

            // assert
            comparison.Should().Throw<InvalidEntityStateException>();
        }

        [Fact]
        public async Task Customer_CreateWithNullAge_ReturnError()
        {
            // arrange
            string firstName = "Mahdi";
            string lastName = "radi";
            HumanAgeField age = null;
            var mobile = new MobileNumber("+989122351212");

            // act
            Action comparison = () =>
            {
                var trip = Customer.Create(firstName, lastName, age, mobile);
            };

            // assert
            comparison.Should().Throw<InvalidEntityStateException>();
        }

        [Fact]
        public async Task Customer_CreateWithNullMobile_ReturnError()
        {
            // arrange
            string firstName = "Mahdi";
            string lastName = "radi";
            var age = new HumanAgeField(50);
            MobileNumber mobile = null;

            // act
            Action comparison = () =>
            {
                var trip = Customer.Create(firstName, lastName, age, mobile);
            };

            // assert
            comparison.Should().Throw<InvalidEntityStateException>();
        }

        [Fact]
        public async Task Customer_CreateAllFiledEntered_ReturnOk()
        {
            // arrange
            string firstName = "Mahdi";
            string lastName = "radi";
            var age = new HumanAgeField(50);
            var mobile = new MobileNumber("+986542124511");

            // act
            Action comparison = () =>
            {
                var trip = Customer.Create(firstName, lastName, age, mobile);
            };

            // assert
            comparison.Should().NotThrow<Exception>();
        }

    }
}

