using System;
using System.Windows;
using System.Windows.Controls;
using TripsManagement.Core.DomainModels.Trips;
using TripsManagement.Endpoint.WPF.ViewModels;

namespace TripsManagement.Endpoint.WPF
{
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;

        public TripsViewModel ViewModel { get; }

        public MainWindow(IServiceProvider serviceProvider, 
            TripsViewModel viewModel)
        {
            _serviceProvider = serviceProvider;
            ViewModel = viewModel;

            InitializeComponent();

            this.DataContext = ViewModel;
        }

        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadTripsAsync();
        }

        private async void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var tripQry = (TripQuery)((Button)e.Source).DataContext;

            var newForm = await TripFormWindow.CreateAsEdit(_serviceProvider, tripQry.Id);
            if (newForm.ShowDialog() == true)
            {
                await ViewModel.LoadTripsAsync();
            }
        }

        private async void btnShowCustomers_Click(object sender, RoutedEventArgs e)
        {
            var tripQry = (TripQuery)((Button)e.Source).DataContext;

            var newForm = await TripCustomersWindow.Create(_serviceProvider, tripQry.Id);
            newForm.ShowDialog();
        }

        private async void btnNewTrip_Click(object sender, RoutedEventArgs e)
        {
            var newForm = await TripFormWindow.CreateAsNew(_serviceProvider);
            if (newForm.ShowDialog() == true)
            {
                await ViewModel.LoadTripsAsync();
            }
        }

        private async void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            var form = CustomersWindow.CreateCustomerWindow(_serviceProvider);
            form.ShowDialog();
        }

        //public async Task Handle(TripCreated Event)
        //{
        //    await ViewModel.LoadTripsAsync();
        //}

        private void MainWindow_OnClosed(object? sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
