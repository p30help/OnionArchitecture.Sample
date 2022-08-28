using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.ApplicationServices.Trips.Commands.AddCustomerToTrip;
using TripsManagement.Core.DomainModels.Contracts;
using TripsManagement.Core.DomainModels.Customers;
using TripsManagement.Core.DomainModels.Exceptions;
using TripsManagement.Core.DomainModels.Trips;
using TripsManagement.Core.DomainModels.ValueObjects;
using Xunit;

namespace TripsManagement.Core.ApplicationServices.Tests.Trips
{
    public class AddCustomerToTripTest
    {
        [Fact]
        public async Task AddCustomerToTrip_CorrectRequest_ReturnOk()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var customerResult = Customer.Create("Leonardo", "Dicaprio", new HumanAgeField(40),new MobileNumber("+1984212175") );
            var tripResult = Trip.Create("LA Trip", DateTime.Now, DateTime.Now.AddDays(5));

            var unitOfWork = fixture.Freeze<Mock<ITripsUnitOfWork>>();
            unitOfWork.Setup(x =>
                    x.TripRepository.GetAsync(It.IsAny<BusinessId>()))
                .ReturnsAsync(tripResult);
            unitOfWork.Setup(x =>
                    x.CustomerRepository.GetAsync(It.IsAny<BusinessId>()))
                .ReturnsAsync(customerResult);

            var handler = new AddCustomerToTripHandler(unitOfWork.Object);

            var request = new AddCustomerToTripCommand()
            {
                CustomerId = customerResult.Id.Value,
                TripId = tripResult.Id.Value
            };

            // action
            var res = await handler.Handle(request, CancellationToken.None);

            // assert
            res.ResultType.Should().Be(RequestResultTypes.Ok);

        }

        [Fact]
        public async Task AddCustomerToTrip_TripNotFound_ReturnError()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            Customer customerResult = Customer.Create("Leonardo", "Dicaprio", new HumanAgeField(40), new MobileNumber("+1984212175"));
            Trip tripResult = null;

            var unitOfWork = fixture.Freeze<Mock<ITripsUnitOfWork>>();
            unitOfWork.Setup(x =>
                    x.TripRepository.GetAsync(It.IsAny<BusinessId>()))
                .ReturnsAsync(tripResult);

            unitOfWork.Setup(x =>
                    x.CustomerRepository.GetAsync(It.IsAny<BusinessId>()))
                .ReturnsAsync(customerResult);

            var handler = new AddCustomerToTripHandler(unitOfWork.Object);

            var request = new AddCustomerToTripCommand()
            {
                CustomerId = customerResult.Id.Value,
                TripId = Guid.NewGuid()
            };

            // action
            Action comparison = () =>
            {
                var res = handler.Handle(request, CancellationToken.None).Result;
            };

            // assert
            comparison.Should().Throw<InvalidEntityStateException>();
        }

        [Fact]
        public async Task AddCustomerToTrip_CustomerNotFound_ReturnError()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            Customer customerResult = null;
            var tripResult = Trip.Create("LA Trip", DateTime.Now, DateTime.Now.AddDays(5));

            var unitOfWork = fixture.Freeze<Mock<ITripsUnitOfWork>>();
            unitOfWork.Setup(x =>
                    x.TripRepository.GetAsync(It.IsAny<BusinessId>()))
                .ReturnsAsync(tripResult);

            unitOfWork.Setup(x =>
                    x.CustomerRepository.GetAsync(It.IsAny<BusinessId>()))
                .ReturnsAsync(customerResult);

            var handler = new AddCustomerToTripHandler(unitOfWork.Object);

            var request = new AddCustomerToTripCommand()
            {
                CustomerId = Guid.NewGuid(),
                TripId = tripResult.Id.Value
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
