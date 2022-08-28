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
using TripsManagement.Core.DomainModels.Customers;
using TripsManagement.Endpoint.WPF.Tools;
using TripsManagement.Endpoint.WPF.ViewModels;
using Xunit;

namespace TripsManagement.Endpoint.WPF.Tests
{
    /// <summary>
    /// <see cref="CustomersViewModel"/>
    /// </summary>
    public class CustomersViewModelTest
    {
        [Fact]
        public async Task CustomersViewModel_TestLoadItems_ReturnOk()
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

            var viewModel = new CustomersViewModel(mediator.Object, windowsTools.Object);

            // action
            await viewModel.LoadCustomersAsync();

            // assert
            viewModel.Items.Count.Should().Be(2);
        }

        [Fact]
        public async Task CustomersViewModel_TestLoadItemsMultiTimes_ReturnOk()
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

            var viewModel = new CustomersViewModel(mediator.Object, windowsTools.Object);

            // action
            await viewModel.LoadCustomersAsync();
            await viewModel.LoadCustomersAsync();
            await viewModel.LoadCustomersAsync();

            // assert
            viewModel.Items.Count.Should().Be(2);
        }

    }
}
