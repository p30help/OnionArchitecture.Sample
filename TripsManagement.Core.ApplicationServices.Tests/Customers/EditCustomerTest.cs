using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.ApplicationServices.Customers.Commands.EditCustomer;
using TripsManagement.Core.ApplicationServices.Trips.Commands.EditTrip;
using TripsManagement.Core.DomainModels.Contracts;
using TripsManagement.Core.DomainModels.Customers;
using TripsManagement.Core.DomainModels.Exceptions;
using TripsManagement.Core.DomainModels.Trips;
using TripsManagement.Core.DomainModels.ValueObjects;
using Xunit;

namespace TripsManagement.Core.ApplicationServices.Tests.Customers
{
    public class EditCustomerTest
    {
        [Fact]
        public async Task EditCustomer_CorrectRequest_ReturnOk()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var customerResult = Customer.Create("Brad", "Pitt", new HumanAgeField(20), new MobileNumber("+14548572387") );

            var unitOfWork = fixture.Freeze<Mock<ITripsUnitOfWork>>();
            unitOfWork.Setup(x =>
                    x.CustomerRepository.GetAsync(It.IsAny<BusinessId>()))
                .ReturnsAsync(customerResult);

            EditCustomerHandler handler = new EditCustomerHandler(unitOfWork.Object);

            var request = new EditCustomerCommand()
            {
                Age = 20,
                FirstName = "Mahdi",
                LastName = "Radi",
                Mobile = "+98512212121",
                CustomerId = Guid.Empty
            };

            // action
            var res = await handler.Handle(request, CancellationToken.None);

            // assert
            res.ResultType.Should().Be(RequestResultTypes.Ok);
        }

        [Fact]
        public async Task EditCustomer_CustomerNotFound_ReturnError()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var unitOfWork = fixture.Freeze<Mock<ITripsUnitOfWork>>();
            unitOfWork.Setup(x =>
                    x.CustomerRepository.GetAsync(It.IsAny<BusinessId>()))
                .Returns(Task.FromResult<Customer>(null));

            var handler = new EditCustomerHandler(unitOfWork.Object);

            var request = new EditCustomerCommand()
            {
                Age = 20,
                FirstName = "Mahdi",
                LastName = "Radi",
                Mobile = "+98512212121",
                CustomerId = Guid.Empty
            };

            // action
            Action comparison = () =>
            {
                var res = handler.Handle(request, CancellationToken.None).Result;
            };

            // assert
            comparison.Should().Throw<InvalidEntityStateException>();
        }

    }
}
