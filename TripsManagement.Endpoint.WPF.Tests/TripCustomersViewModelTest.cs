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
using TripsManagement.Core.ApplicationServices.Customers.Queries.GetAllCustomers;
using TripsManagement.Core.ApplicationServices.Trips.Commands.AddCustomerToTrip;
using TripsManagement.Core.ApplicationServices.Trips.Queries.GetCustomersByTrip;
using TripsManagement.Core.DomainModels.Customers;
using TripsManagement.Endpoint.WPF.Tools;
using TripsManagement.Endpoint.WPF.ViewModels;
using Xunit;

namespace TripsManagement.Endpoint.WPF.Tests
{
    /// <summary>
    /// <see cref="TripCustomersViewModel"/>
    /// </summary>
    public class TripCustomersViewModelTest
    {
        [Fact]
        public async Task TripCustomersViewModel_LoadCustomers_ReturnOk()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var list = new List<CustomerQuery>();
            list.Add(new CustomerQuery()
            {
                Mobile = "+36212121",
                FirstName = "Mark",
                LastName = "Zakerburg",
                Age = 50,
                Id = Guid.NewGuid(),
                RecordDate = DateTime.Now.AddDays(-2),
            });
            list.Add(new CustomerQuery()
            {
                Mobile = "+36214542121",
                FirstName = "Tomas",
                LastName = "Edison",
                Age = 52,
                Id = Guid.NewGuid(),
                RecordDate = DateTime.Now.AddDays(-10),
            });

            var requestResult = RequestResult<List<CustomerQuery>>.Ok(list);

            var mediator = fixture.Freeze<Mock<IMediator>>();
            mediator.Setup(x =>
                    x.Send(It.IsAny<GetAllCustomersQuery>(), CancellationToken.None))
                    .ReturnsAsync(requestResult);

            var windowsTools = fixture.Freeze<Mock<IWindowTools>>();
            windowsTools.Setup(x =>
                    x.HandleErrorIfExist(It.IsAny<RequestResult>()))
                .Returns(false);

            var viewModel = new TripCustomersViewModel(mediator.Object, windowsTools.Object);

            // action
            await viewModel.LoadCustomersAsync();

            // assert
            viewModel.AllCustomers.Count.Should().Be(2);
        }

        [Fact]
        public async Task TripCustomersViewModel_LoadTripCustomers_ReturnOk()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var mediator = mockForGetCustomersByTripQuery(fixture);

            var windowsTools = fixture.Freeze<Mock<IWindowTools>>();
            windowsTools.Setup(x =>
                    x.HandleErrorIfExist(It.IsAny<RequestResult>()))
                .Returns(false);

            var viewModel = new TripCustomersViewModel(mediator.Object, windowsTools.Object);
            viewModel.TripId = Guid.NewGuid();

            // action
            await viewModel.LoadTripCustomersAsync();

            // assert
            viewModel.TripCustomers.Count.Should().Be(2);
        }

        [Fact]
        public async Task TripCustomersViewModel_AddCustomerToTrip_ReturnOk()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var mediator = mockForGetCustomersByTripQuery(fixture);

            var requestResult = RequestResult<Guid>.Ok(Guid.NewGuid());

            mediator.Setup(x =>
                    x.Send(It.IsAny<AddCustomerToTripCommand>(), CancellationToken.None))
                .ReturnsAsync(requestResult);

            var windowsTools = fixture.Freeze<Mock<IWindowTools>>();
            windowsTools.Setup(x =>
                    x.HandleErrorIfExist(It.IsAny<RequestResult>()))
                .Returns(false);

            var viewModel = new TripCustomersViewModel(mediator.Object, windowsTools.Object);
            viewModel.TripId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            viewModel.SelectedCustomer = new CustomerQuery()
            {
                Id = Guid.NewGuid()
            };

            // action
            var res = await viewModel.AddCustomerToTripAsync();

            // assert
            res.Should().Be(true);
        }

        [Fact]
        public async Task TripCustomersViewModel_RemoveCustomerFromTrip_ReturnOk()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var mediator = mockForGetCustomersByTripQuery(fixture);

            var requestResult = RequestResult<Guid>.Ok(Guid.NewGuid());

            mediator.Setup(x =>
                    x.Send(It.IsAny<AddCustomerToTripCommand>(), CancellationToken.None))
                .ReturnsAsync(requestResult);

            var windowsTools = fixture.Freeze<Mock<IWindowTools>>();
            windowsTools.Setup(x =>
                    x.HandleErrorIfExist(It.IsAny<RequestResult>()))
                .Returns(false);

            var viewModel = new TripCustomersViewModel(mediator.Object, windowsTools.Object);
            viewModel.TripId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            viewModel.SelectedGridCustomer = new CustomerQuery()
            {
                Id = Guid.NewGuid()
            };

            // action
            var res = await viewModel.RemoveCustomerFromTripAsync();

            // assert
            res.Should().Be(true);
        }

        private Mock<IMediator> mockForGetCustomersByTripQuery(IFixture fixture)
        {
            var list = new List<CustomerQuery>();
            list.Add(new CustomerQuery()
            {
                Mobile = "+36212121",
                FirstName = "Mark",
                LastName = "Zakerburg",
                Age = 50,
                Id = Guid.NewGuid(),
                RecordDate = DateTime.Now.AddDays(-2),
            });
            list.Add(new CustomerQuery()
            {
                Mobile = "+36214542121",
                FirstName = "Tomas",
                LastName = "Edison",
                Age = 52,
                Id = Guid.NewGuid(),
                RecordDate = DateTime.Now.AddDays(-10),
            });

            var requestResult = RequestResult<List<CustomerQuery>>.Ok(list);

            var mediator = fixture.Freeze<Mock<IMediator>>();
            mediator.Setup(x =>
                    x.Send(It.IsAny<GetCustomersByTripQuery>(), CancellationToken.None))
                .ReturnsAsync(requestResult);

            return mediator;
        }
    }
}
