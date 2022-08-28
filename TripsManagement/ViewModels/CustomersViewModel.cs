using MediatR;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using TripsManagement.Core.ApplicationServices.Customers.Queries.GetAllCustomers;
using TripsManagement.Core.DomainModels.Customers;
using TripsManagement.Endpoint.WPF.Tools;

namespace TripsManagement.Endpoint.WPF.ViewModels
{
    public class CustomersViewModel : INotifyPropertyChanged
    {
        private readonly IMediator _mediator;
        private readonly IWindowTools _windowTools;

        public CustomersViewModel(IMediator mediator, IWindowTools windowTools)
        {
            _mediator = mediator;
            _windowTools = windowTools;
        }

        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) =>
        {

        };

        public ObservableCollection<CustomerQuery> Items { get; set; } = new ObservableCollection<CustomerQuery>();

        public async Task LoadCustomersAsync()
        {
            var query = new GetAllCustomersQuery();
            var res = await _mediator.Send(query);
            if (_windowTools.HandleErrorIfExist(res) == true)
            {
                return;
            }

            Items.Clear();
            res.Data.ForEach(x => Items.Add(x));
        }

    }
}
