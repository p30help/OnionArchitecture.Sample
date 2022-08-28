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
using TripsManagement.Core.ApplicationServices.Trips.Queries.GetAllTrips;
using TripsManagement.Core.DomainModels.Trips;
using TripsManagement.Endpoint.WPF.Tools;
using TripsManagement.Endpoint.WPF.ViewModels;
using Xunit;

namespace TripsManagement.Endpoint.WPF.Tests
{
    /// <summary>
    /// <see cref="TripsViewModel"/>
    /// </summary>
    public class TripsViewModelTest
    {
        [Fact]
        public async Task TripsViewModel_TestLoadItems_ReturnOk()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var list = new List<TripQuery>();
            list.Add(new TripQuery()
            {
                Title = "La Trip",
                StartDate = DateTime.Now,
                FinishDate = DateTime.Now.AddDays(5),
                Id = Guid.NewGuid(),
                IsCanceled = false,
                RecordDate = DateTime.Now.AddDays(-2),
                TripCustomers = new List<TripCustomerQuery>()
            });
            list.Add(new TripQuery()
            {
                Title = "Good Trip",
                StartDate = DateTime.Now.AddDays(1),
                FinishDate = DateTime.Now.AddDays(2),
                Id = Guid.NewGuid(),
                IsCanceled = true,
                RecordDate = DateTime.Now.AddDays(-5),
                TripCustomers = new List<TripCustomerQuery>()
            });

            var requestResult = RequestResult<List<TripQuery>>.Ok(list);

            var mediator = fixture.Freeze<Mock<IMediator>>();
            mediator.Setup(x =>
                    x.Send(It.IsAny<GetAllTripsQuery>(), CancellationToken.None))
                    .ReturnsAsync(requestResult);

            var windowsTools = fixture.Freeze<Mock<IWindowTools>>();
            windowsTools.Setup(x =>
                    x.HandleErrorIfExist(It.IsAny<RequestResult>()))
                .Returns(false);

            var viewModel = new TripsViewModel(mediator.Object, windowsTools.Object);

            // action
            await viewModel.LoadTripsAsync();

            // assert
            viewModel.Items.Count.Should().Be(2);
        }

        [Fact]
        public async Task TripsViewModel_TestLoadItemsMultiTime_ReturnOk()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var list = new List<TripQuery>();
            list.Add(new TripQuery()
            {
                Title = "La Trip",
                StartDate = DateTime.Now,
                FinishDate = DateTime.Now.AddDays(5),
                Id = Guid.NewGuid(),
                IsCanceled = false,
                RecordDate = DateTime.Now.AddDays(-2),
                TripCustomers = new List<TripCustomerQuery>()
            });
            list.Add(new TripQuery()
            {
                Title = "Good Trip",
                StartDate = DateTime.Now.AddDays(1),
                FinishDate = DateTime.Now.AddDays(2),
                Id = Guid.NewGuid(),
                IsCanceled = true,
                RecordDate = DateTime.Now.AddDays(-5),
                TripCustomers = new List<TripCustomerQuery>()
            });

            var requestResult = RequestResult<List<TripQuery>>.Ok(list);

            var mediator = fixture.Freeze<Mock<IMediator>>();
            mediator.Setup(x =>
                    x.Send(It.IsAny<GetAllTripsQuery>(), CancellationToken.None))
                .ReturnsAsync(requestResult);

            var windowsTools = fixture.Freeze<Mock<IWindowTools>>();
            windowsTools.Setup(x =>
                    x.HandleErrorIfExist(It.IsAny<RequestResult>()))
                .Returns(false);

            var viewModel = new TripsViewModel(mediator.Object, windowsTools.Object);

            // action
            await viewModel.LoadTripsAsync();
            await viewModel.LoadTripsAsync();
            await viewModel.LoadTripsAsync();

            // assert
            viewModel.Items.Count.Should().Be(2);
        }

    }
}
