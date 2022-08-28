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
using TripsManagement.Core.ApplicationServices.Customers.Commands.AddNewCustomer;
using TripsManagement.Core.ApplicationServices.Customers.Commands.EditCustomer;
using TripsManagement.Core.ApplicationServices.Customers.Queries.GetCustomer;
using TripsManagement.Core.ApplicationServices.Trips.Commands.AddNewTrip;
using TripsManagement.Core.ApplicationServices.Trips.Queries.GetTrip;
using TripsManagement.Core.DomainModels.Customers;
using TripsManagement.Core.DomainModels.Trips;
using TripsManagement.Endpoint.WPF.Tools;
using TripsManagement.Endpoint.WPF.ViewModels;
using Xunit;

namespace TripsManagement.Endpoint.WPF.Tests
{
    /// <summary>
    /// <see cref="CustomerFormViewModel"/>
    /// </summary>
    public class CustomerFormViewModelTest
    {
        [Fact]
        public async Task CustomerFormViewModel_TestLoadAsEditMode_ReturnOk()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var customerQuery = new CustomerQuery()
            {
                Id = Guid.NewGuid(),
                RecordDate = DateTime.Now,
                FirstName = "Johnny",
                LastName = "Deep",
                Age = 50,
                Mobile = "+13854526565",
            };

            var requestResult = RequestResult<CustomerQuery>.Ok(customerQuery);

            var mediator = fixture.Freeze<Mock<IMediator>>();
            mediator.Setup(x =>
                    x.Send(It.IsAny<GetCustomerQuery>(), CancellationToken.None))
                    .ReturnsAsync(requestResult);

            var windowsTools = fixture.Freeze<Mock<IWindowTools>>();
            windowsTools.Setup(x =>
                    x.HandleErrorIfExist(It.IsAny<RequestResult>()))
                .Returns(false);

            var viewModel = new CustomerFormViewModel(mediator.Object, windowsTools.Object);

            // action
            await viewModel.LoadAsEdit(customerQuery.Id);

            // assert
            viewModel.CustomerId.Should().Be(customerQuery.Id);
            viewModel.Age.Should().Be(customerQuery.Age);
            viewModel.Mobile.Should().Be(customerQuery.Mobile);
            viewModel.FirstName.Should().Be(customerQuery.FirstName);
            viewModel.LastName.Should().Be(customerQuery.LastName);
            viewModel.FormMode.Should().Be(FormModes.Edit);
        }

        [Fact]
        public async Task CustomerFormViewModel_TestNewTripMode_ReturnOk()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var customerQuery = new CustomerQuery()
            {
                Id = Guid.NewGuid(),
                RecordDate = DateTime.Now,
                FirstName = "Johnny",
                LastName = "Deep",
                Age = 50,
                Mobile = "+13854526565",
            };

            var requestResult = RequestResult<CustomerQuery>.Ok(customerQuery);

            var mediator = fixture.Freeze<Mock<IMediator>>();
            mediator.Setup(x =>
                    x.Send(It.IsAny<GetCustomerQuery>(), CancellationToken.None))
                .ReturnsAsync(requestResult);

            var windowsTools = fixture.Freeze<Mock<IWindowTools>>();
            windowsTools.Setup(x =>
                    x.HandleErrorIfExist(It.IsAny<RequestResult>()))
                .Returns(false);

            var viewModel = new CustomerFormViewModel(mediator.Object, windowsTools.Object);

            // action
            await viewModel.NewCustomer();

            // assert
            viewModel.FirstName.Should().Be(null);
            viewModel.LastName.Should().Be(null);
            viewModel.Age.Should().Be(null);
            viewModel.Mobile.Should().Be(null);
            viewModel.CustomerId.Should().Be(Guid.Empty);
            viewModel.FormMode.Should().Be(FormModes.Add);
        }

        [Fact]
        public async Task CustomerFormViewModel_SaveNewTrip_ReturnOk()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var requestResult = RequestResult<Guid>.Ok(Guid.NewGuid());

            var mediator = fixture.Freeze<Mock<IMediator>>();
            mediator.Setup(x =>
                    x.Send(It.IsAny<AddNewCustomerCommand>(), CancellationToken.None))
                .ReturnsAsync(requestResult);

            var windowsTools = fixture.Freeze<Mock<IWindowTools>>();
            windowsTools.Setup(x =>
                    x.HandleErrorIfExist(It.IsAny<RequestResult>()))
                .Returns(false);

            var viewModel = new CustomerFormViewModel(mediator.Object, windowsTools.Object);
            await viewModel.NewCustomer();
            viewModel.FirstName = "Alex";
            viewModel.LastName = "Grahambel";
            viewModel.Age = 30;
            viewModel.Mobile = "+12746786565";

            // action
            var res = await viewModel.SaveAsync();

            // assert
            res.Should().Be(true);
        }

        [Fact]
        public async Task CustomerFormViewModel_SaveEditedTrip_ReturnOk()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            var customerQuery = new CustomerQuery()
            {
                Id = Guid.NewGuid(),
                RecordDate = DateTime.Now,
                FirstName = "Johnny",
                LastName = "Deep",
                Age = 50,
                Mobile = "+13854526565",
            };
            var customerRequestResult = RequestResult<CustomerQuery>.Ok(customerQuery);

            var requestResult = RequestResult<Guid>.Ok(Guid.NewGuid());

            var mediator = fixture.Freeze<Mock<IMediator>>();
            mediator.Setup(x =>
                    x.Send(It.IsAny<EditCustomerCommand>(), CancellationToken.None))
                .ReturnsAsync(requestResult);

            mediator.Setup(x =>
                    x.Send(It.IsAny<GetCustomerQuery>(), CancellationToken.None))
                .ReturnsAsync(customerRequestResult);

            var windowsTools = fixture.Freeze<Mock<IWindowTools>>();
            windowsTools.Setup(x =>
                    x.HandleErrorIfExist(It.IsAny<RequestResult>()))
                .Returns(false);

            var viewModel = new CustomerFormViewModel(mediator.Object, windowsTools.Object);
            await viewModel.LoadAsEdit(Guid.NewGuid());
            viewModel.FirstName = "Alex";
            viewModel.LastName = "Grahambel";
            viewModel.Age = 30;
            viewModel.Mobile = "+12746786565";

            // action
            var res = await viewModel.SaveAsync();

            // assert
            res.Should().Be(true);
        }

    }
}
