using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TripsManagement.Endpoint.WPF.ViewModels;

namespace TripsManagement.Endpoint.WPF
{
    public partial class CustomerFormWindow : Window
    {
        public CustomerFormViewModel ViewModel { get; }

        public CustomerFormWindow(CustomerFormViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();

            this.DataContext = ViewModel;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private async void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (await ViewModel.SaveAsync() == false)
            {
                return;
            }

            MessageBox.Show("Customer saved.");
            this.DialogResult = true;
            this.Close();
        }

        public static async Task<CustomerFormWindow> CreateAsNew(IServiceProvider serviceProvider)
        {
            var window = serviceProvider.GetRequiredService<CustomerFormWindow>();
            await window.ViewModel.NewCustomer();
            window.Title = "New customer";

            return window;
        }

        public static async Task<CustomerFormWindow> CreateAsEdit(IServiceProvider serviceProvider, Guid customerId)
        {
            var window = serviceProvider.GetRequiredService<CustomerFormWindow>();
            await window.ViewModel.LoadAsEdit(customerId);
            window.Title = "Edit customer";

            return window;
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
