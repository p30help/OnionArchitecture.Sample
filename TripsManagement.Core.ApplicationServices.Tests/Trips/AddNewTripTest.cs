using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.ApplicationServices.Common.Events;
using TripsManagement.Core.ApplicationServices.Trips.Commands.AddNewTrip;
using TripsManagement.Core.DomainModels.Contracts;
using TripsManagement.Core.DomainModels.Exceptions;
using Xunit;

namespace TripsManagement.Core.ApplicationServices.Tests.Trips
{
    public class AddNewTripTest
    {
        [Fact]
        public async Task AddNewTrip_CorrectRequest_ReturnOk()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var unitOfWork = fixture.Freeze<Mock<ITripsUnitOfWork>>();
            var eventDispatcher = fixture.Freeze<Mock<IEventDispatcher>>();

            AddNewTripHandler handler = new AddNewTripHandler(unitOfWork.Object, eventDispatcher.Object);

            var request = new AddNewTripCommand()
            {
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
        public async Task AddNewTrip_NoTitleForRequest_ReturnError()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var unitOfWork = fixture.Freeze<Mock<ITripsUnitOfWork>>();
            var eventDispatcher = fixture.Freeze<Mock<IEventDispatcher>>();

            AddNewTripHandler handler = new AddNewTripHandler(unitOfWork.Object, eventDispatcher.Object);

            var request = new AddNewTripCommand()
            {
                StartDate = DateTime.Now,
                FinishDate = DateTime.Now.AddDays(5),
                Title = null
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
