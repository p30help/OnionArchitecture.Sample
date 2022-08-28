using MediatR;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using TripsManagement.Core.ApplicationServices.Common.Events;
using TripsManagement.Core.ApplicationServices.Trips.Queries.GetAllTrips;
using TripsManagement.Core.DomainModels.Trips;
using TripsManagement.Core.DomainModels.Trips.Events;
using TripsManagement.Endpoint.WPF.Tools;

namespace TripsManagement.Endpoint.WPF.ViewModels
{
    public class TripsViewModel : INotifyPropertyChanged, IDomainEventHandler<TripCreated>
    {
        private readonly IMediator _mediator;
        private readonly IWindowTools _windowTools;
        public TripsViewModel(IMediator mediator, IWindowTools windowTools)
        {
            _mediator = mediator;
            _windowTools = windowTools;
        }

        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) =>
        {

        };

        public ObservableCollection<TripQuery> Items { get; set; } = new ObservableCollection<TripQuery>();

        public async Task LoadTripsAsync()
        {
            var query = new GetAllTripsQuery();
            var res = await _mediator.Send(query);
            if (_windowTools.HandleErrorIfExist(res) == true)
            {
                return;
            }

            Items.Clear();
            res.Data.ForEach(x => Items.Add(x));
        }

        public async Task Handle(TripCreated Event)
        {
            await LoadTripsAsync();
        }
    }
}
