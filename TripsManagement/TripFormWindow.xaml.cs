using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;
using TripsManagement.Endpoint.WPF.ViewModels;

namespace TripsManagement.Endpoint.WPF
{
    /// <summary>
    /// Interaction logic for TripFormWindow.xaml
    /// </summary>
    public partial class TripFormWindow : Window
    {
        public TripFormViewModel ViewModel { get; }

        public TripFormWindow(TripFormViewModel viewModel)
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

            MessageBox.Show("New trip saved.");
            this.DialogResult = true;
            this.Close();
        }

        #region Windows Instance

        public static async Task<TripFormWindow> CreateAsNew(IServiceProvider serviceProvider)
        {
            var window = serviceProvider.GetRequiredService<TripFormWindow>();
            await window.ViewModel.NewTrip();
            window.Title = "New trip";

            return window;
        }

        public static async Task<TripFormWindow> CreateAsEdit(IServiceProvider serviceProvider, Guid tripId)
        {
            var window = serviceProvider.GetRequiredService<TripFormWindow>();
            await window.ViewModel.LoadAsEdit(tripId);
            window.Title = "Edit trip";

            return window;
        }

        #endregion



    }
}
