using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.ApplicationServices.Trips.Commands.EditTrip;
using TripsManagement.Core.DomainModels.Contracts;
using TripsManagement.Core.DomainModels.Exceptions;
using TripsManagement.Core.DomainModels.Trips;
using TripsManagement.Core.DomainModels.ValueObjects;
using Xunit;

namespace TripsManagement.Core.ApplicationServices.Tests.Trips
{
    public class EditTripTest
    {
        [Fact]
        public async Task EditTrip_CorrectRequest_ReturnOk()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var tripResult = Trip.Create("hello", DateTime.Now, DateTime.Now.AddDays(5));

            var unitOfWork = fixture.Freeze<Mock<ITripsUnitOfWork>>();
            unitOfWork.Setup(x =>
                    x.TripRepository.GetAsync(It.IsAny<BusinessId>()))
                .ReturnsAsync(tripResult);

            EditTripHandler handler = new EditTripHandler(unitOfWork.Object);

            var request = new EditTripCommand()
            {
                TripId = tripResult.Id.Value,
                IsCanceled = true,
                StartDate = DateTime.Now,
                FinishDate =  DateTime.Now.AddDays(5),
                Title = "test"
            };

            // action
            var res = await handler.Handle(request, CancellationToken.None);

            // assert
            res.ResultType.Should().Be(RequestResultTypes.Ok);
        }

        [Fact]
        public async Task EditTrip_TripNotFound_ReturnError()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var unitOfWork = fixture.Freeze<Mock<ITripsUnitOfWork>>();
            unitOfWork.Setup(x =>
                    x.TripRepository.GetAsync(It.IsAny<BusinessId>()))
                .Returns(Task.FromResult<Trip>(null));

            EditTripHandler handler = new EditTripHandler(unitOfWork.Object);

            var request = new EditTripCommand()
            {
                TripId = Guid.NewGuid(),
                IsCanceled = true,
                StartDate = DateTime.Now,
                FinishDate = DateTime.Now.AddDays(5),
                Title = "test"
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
