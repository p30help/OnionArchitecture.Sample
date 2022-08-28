using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TripsManagement.Core.ApplicationServices.Common;
using TripsManagement.Core.ApplicationServices.Trips.Commands.AddNewTrip;
using TripsManagement.Core.ApplicationServices.Trips.Commands.EditTrip;
using TripsManagement.Core.ApplicationServices.Trips.Queries.GetTrip;
using TripsManagement.Core.DomainModels.Trips;
using TripsManagement.Endpoint.WPF.Tools;
using TripsManagement.Endpoint.WPF.ViewModels;
using Xunit;

namespace TripsManagement.Endpoint.WPF.Tests
{
    /// <summary>
    /// <see cref="TripFormViewModel"/>
    /// </summary>
    public class TripFormViewModelTest
    {
        [Fact]
        public async Task TripFormViewModel_TestLoadAsEditMode_ReturnOk()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var tripQuery = new TripQuery()
            {
                Title = "La Trip",
                StartDate = DateTime.Now,
                FinishDate = DateTime.Now.AddDays(5),
                Id = Guid.NewGuid(),
                IsCanceled = false,
                RecordDate = DateTime.Now.AddDays(-2),
                TripCustomers = new List<TripCustomerQuery>()
            };

            var requestResult = RequestResult<TripQuery>.Ok(tripQuery);

            var mediator = fixture.Freeze<Mock<IMediator>>();
            mediator.Setup(x =>
                    x.Send(It.IsAny<GetTripQuery>(), CancellationToken.None))
                    .ReturnsAsync(requestResult);

            var windowsTools = fixture.Freeze<Mock<IWindowTools>>();
            windowsTools.Setup(x =>
                    x.HandleErrorIfExist(It.IsAny<RequestResult>()))
                .Returns(false);

            var viewModel = new TripFormViewModel(mediator.Object, windowsTools.Object);

            // action
            await viewModel.LoadAsEdit(tripQuery.Id);

            // assert
            viewModel.StartDate.Should().Be(tripQuery.StartDate);
            viewModel.FinishDate.Should().Be(tripQuery.FinishDate);
            viewModel.Title.Should().Be(tripQuery.Title);
            viewModel.IsCanceled.Should().Be(tripQuery.IsCanceled);
            viewModel.TripId.Should().Be(tripQuery.Id);
            viewModel.FormMode.Should().Be(FormModes.Edit);
        }

        [Fact]
        public async Task TripFormViewModel_TestNewTripMode_ReturnOk()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var tripQuery = new TripQuery()
            {
                Title = "La Trip",
                StartDate = DateTime.Now,
                FinishDate = DateTime.Now.AddDays(5),
                Id = Guid.NewGuid(),
                IsCanceled = false,
                RecordDate = DateTime.Now.AddDays(-2),
                TripCustomers = new List<TripCustomerQuery>()
            };

            var requestResult = RequestResult<TripQuery>.Ok(tripQuery);

            var mediator = fixture.Freeze<Mock<IMediator>>();
            mediator.Setup(x =>
                    x.Send(It.IsAny<GetTripQuery>(), CancellationToken.None))
                .ReturnsAsync(requestResult);

            var windowsTools = fixture.Freeze<Mock<IWindowTools>>();
            windowsTools.Setup(x =>
                    x.HandleErrorIfExist(It.IsAny<RequestResult>()))
                .Returns(false);

            var viewModel = new TripFormViewModel(mediator.Object, windowsTools.Object);

            // action
            await viewModel.NewTrip();

            // assert
            viewModel.StartDate.Should().Be(null);
            viewModel.FinishDate.Should().Be(null);
            viewModel.Title.Should().Be(null);
            viewModel.IsCanceled.Should().Be(false);
            viewModel.TripId.Should().Be(Guid.Empty);
            viewModel.FormMode.Should().Be(FormModes.Add);
        }

        [Fact]
        public async Task TripFormViewModel_SaveNewTrip_ReturnOk()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var requestResult = RequestResult<Guid>.Ok(Guid.NewGuid());

            var mediator = fixture.Freeze<Mock<IMediator>>();
            mediator.Setup(x =>
                    x.Send(It.IsAny<AddNewTripCommand>(), CancellationToken.None))
                .ReturnsAsync(requestResult);

            var windowsTools = fixture.Freeze<Mock<IWindowTools>>();
            windowsTools.Setup(x =>
                    x.HandleErrorIfExist(It.IsAny<RequestResult>()))
                .Returns(false);

            var viewModel = new TripFormViewModel(mediator.Object, windowsTools.Object);
            await viewModel.NewTrip();
            viewModel.StartDate =DateTime.Now;
            viewModel.FinishDate = DateTime.Now.AddDays(2);
            viewModel.Title = "LA";

            // action
            var res = await viewModel.SaveAsync();

            // assert
            res.Should().Be(true);
        }

        [Fact]
        public async Task TripFormViewModel_SaveEditedTrip_ReturnOk()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var tripQuery = new TripQuery()
            {
                Title = "La Trip",
                StartDate = DateTime.Now,
                FinishDate = DateTime.Now.AddDays(5),
                Id = Guid.NewGuid(),
                IsCanceled = false,
                RecordDate = DateTime.Now.AddDays(-2),
                TripCustomers = new List<TripCustomerQuery>()
            };
            var tripRequestResult = RequestResult<TripQuery>.Ok(tripQuery);
            
            var requestResult = RequestResult<Guid>.Ok(Guid.NewGuid());

            var mediator = fixture.Freeze<Mock<IMediator>>();
            mediator.Setup(x =>
                    x.Send(It.IsAny<EditTripCommand>(), CancellationToken.None))
                .ReturnsAsync(requestResult);

            mediator.Setup(x =>
                    x.Send(It.IsAny<GetTripQuery>(), CancellationToken.None))
                .ReturnsAsync(tripRequestResult);

            var windowsTools = fixture.Freeze<Mock<IWindowTools>>();
            windowsTools.Setup(x =>
                    x.HandleErrorIfExist(It.IsAny<RequestResult>()))
                .Returns(false);

            var viewModel = new TripFormViewModel(mediator.Object, windowsTools.Object);
            await viewModel.LoadAsEdit(Guid.NewGuid());
            viewModel.StartDate = DateTime.Now;
            viewModel.FinishDate = DateTime.Now.AddDays(2);
            viewModel.Title = "LA";

            // action
            var res = await viewModel.SaveAsync();

            // assert
            res.Should().Be(true);
        }

    }
}
