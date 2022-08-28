using MediatR;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using TripsManagement.Core.ApplicationServices.Customers.Queries.GetAllCustomers;
using TripsManagement.Core.ApplicationServices.Trips.Commands.AddCustomerToTrip;
using TripsManagement.Core.ApplicationServices.Trips.Commands.RemoveCustomerFromTrip;
using TripsManagement.Core.ApplicationServices.Trips.Queries.GetCustomersByTrip;
using TripsManagement.Core.DomainModels.Customers;
using TripsManagement.Endpoint.WPF.Tools;

namespace TripsManagement.Endpoint.WPF.ViewModels
{
    public class TripCustomersViewModel : INotifyPropertyChanged
    {
        private readonly IMediator _mediator;
        private readonly IWindowTools _windowTools;
        public TripCustomersViewModel(IMediator mediator, IWindowTools windowTools)
        {
            _mediator = mediator;
            _windowTools = windowTools;
        }

        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) =>
        {
        };

        private Guid _tripId;
        public Guid TripId
        {
            get
            {
                return _tripId;
            }
            set
            {
                _tripId = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(TripId)));
            }
        }

        private CustomerQuery _selectedCustomer;
        public CustomerQuery SelectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }
            set
            {
                _selectedCustomer = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedCustomer)));
            }
        }

        private CustomerQuery _selectedGridCustomer;
        public CustomerQuery SelectedGridCustomer
        {
            get
            {
                return _selectedGridCustomer;
            }
            set
            {
                _selectedGridCustomer = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedGridCustomer)));
            }
        }

        public ObservableCollection<CustomerQuery> AllCustomers { get; set; } = new ObservableCollection<CustomerQuery>();

        public ObservableCollection<CustomerQuery> TripCustomers { get; set; } = new ObservableCollection<CustomerQuery>();

        public async Task LoadCustomersAsync()
        {
            var query = new GetAllCustomersQuery();
            var res = await _mediator.Send(query);
            if (_windowTools.HandleErrorIfExist(res) == true)
            {
                return;
            }

            //cmbCustomers.SelectedValuePath = "Id";
            //cmbCustomers.DisplayMemberPath = "FullName";
            //AllCustomers.ForEach(x =>
            //{
            //    cmbCustomers.Items.Add(x);
            //});

            AllCustomers.Clear();
            res.Data.ForEach(x => AllCustomers.Add(x));
        }

        public async Task LoadTripCustomersAsync()
        {
            var query = new GetCustomersByTripQuery()
            {
                TripId = TripId
            };
            var res = await _mediator.Send(query);
            if (_windowTools.HandleErrorIfExist(res) == true)
            {
                return;
            }

            TripCustomers.Clear();
            res.Data.ForEach(x => TripCustomers.Add(x));
        }

        public async Task<bool> RemoveCustomerFromTripAsync()
        {
            if (SelectedGridCustomer == null)
            {
                MessageBox.Show("Please select a row", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            var command = new RemoveCustomerFromTripCommand()
            {
                CustomerId = SelectedGridCustomer.Id,
                TripId = TripId
            };
            var res = await _mediator.Send(command);
            if (_windowTools.HandleErrorIfExist(res) == true)
            {
                return false;
            }

            await LoadTripCustomersAsync();

            return true;
        }

        public async Task<bool> AddCustomerToTripAsync()
        {
            if (SelectedCustomer == null)
            {
                MessageBox.Show("Please select a customer", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            var command = new AddCustomerToTripCommand()
            {
                CustomerId = SelectedCustomer.Id,
                TripId = TripId
            };
            var res = await _mediator.Send(command);
            if (_windowTools.HandleErrorIfExist(res) == true)
            {
                return false;
            }

            await LoadTripCustomersAsync();

            return true;
        }
    }
}
