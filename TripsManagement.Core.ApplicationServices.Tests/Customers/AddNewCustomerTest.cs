using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.ApplicationServices.Common.Events;
using TripsManagement.Core.ApplicationServices.Customers.Commands.AddNewCustomer;
using TripsManagement.Core.DomainModels.Contracts;
using TripsManagement.Core.DomainModels.Exceptions;
using Xunit;

namespace TripsManagement.Core.ApplicationServices.Tests.Customers
{
    public class AddNewCustomerTest
    {
        [Fact]
        public async Task AddNewCustomer_CorrectRequest_ReturnOk()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var unitOfWork = fixture.Freeze<Mock<ITripsUnitOfWork>>();

            var handler = new AddNewCustomerHandler(unitOfWork.Object);

            var request = new AddNewCustomerCommand()
            {
                FirstName = "Mahdi",
                LastName = "radi",
                Age = 20,
                Mobile = "+45212416421"
            };

            // action
            var res = await handler.Handle(request, CancellationToken.None);

            // assert
            res.ResultType.Should().Be(RequestResultTypes.Ok);

        }

        [Fact]
        public async Task AddNewCustomer_NullFirstNameForRequest_ReturnError()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var unitOfWork = fixture.Freeze<Mock<ITripsUnitOfWork>>();
            var eventDispatcher = fixture.Freeze<Mock<IEventDispatcher>>();

            var handler = new AddNewCustomerHandler(unitOfWork.Object);

            var request = new AddNewCustomerCommand()
            {
                FirstName = null,
                LastName = "radi",
                Age = 20,
                Mobile = "+45212416421"
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
