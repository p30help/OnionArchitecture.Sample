using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls;
using TripsManagement.Core.DomainModels.Customers;
using TripsManagement.Endpoint.WPF.ViewModels;

namespace TripsManagement.Endpoint.WPF
{
    public partial class CustomersWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomersViewModel ViewModel { get; }

        public CustomersWindow(IServiceProvider serviceProvider,
            CustomersViewModel viewModel)
        {
            _serviceProvider = serviceProvider;
            ViewModel = viewModel;

            InitializeComponent();

            this.DataContext = ViewModel;
        }

        private async void CustomersWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadCustomersAsync();
        }

        private async void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var item = (CustomerQuery)((Button)e.Source).DataContext;

            var newForm = await CustomerFormWindow.CreateAsEdit(_serviceProvider, item.Id);
            if (newForm.ShowDialog() == true)
            {
                await ViewModel.LoadCustomersAsync();
            }
        }

        private async void btnNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            var newForm = await CustomerFormWindow.CreateAsNew(_serviceProvider);
            if (newForm.ShowDialog() == true)
            {
                await ViewModel.LoadCustomersAsync();
            }
        }

        #region Window Instances
        public static CustomersWindow CreateCustomerWindow(IServiceProvider serviceProvider)
        {
            var window = serviceProvider.GetRequiredService<CustomersWindow>();

            return window;
        }


        #endregion


    }
}
