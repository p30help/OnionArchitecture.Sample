using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TripsManagement.Core.DomainModels.Customers;
using TripsManagement.Endpoint.WPF.ViewModels;

namespace TripsManagement.Endpoint.WPF
{
    public partial class TripCustomersWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;

        private TripCustomersViewModel ViewModel { get; }

        public TripCustomersWindow( IServiceProvider serviceProvider, TripCustomersViewModel viewModel)
        {
            _serviceProvider = serviceProvider;
            ViewModel = viewModel;

            InitializeComponent();

            this.DataContext = viewModel;
        }

        private async void TripCustomersWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadCustomersAsync();

            await ViewModel.LoadTripCustomersAsync();
        }

        private async void btnAddCustomerToTrip_Click(object sender, RoutedEventArgs e)
        {
            if (await ViewModel.AddCustomerToTripAsync() == false)
            {
                return;
            }

            MessageBox.Show("Customer added to trip.");
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                MessageBoxResult.No)
            {
                return;
            }

            if (await ViewModel.RemoveCustomerFromTripAsync() == false)
            {
                return;
            }

            MessageBox.Show("Customer removed from trip.");
        }

        public static async Task<TripCustomersWindow> Create(IServiceProvider serviceProvider, Guid tripId)
        {
            var window = serviceProvider.GetRequiredService<TripCustomersWindow>();
            window.ViewModel.TripId = tripId;

            return window;
        }
    }
}
