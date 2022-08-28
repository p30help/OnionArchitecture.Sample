using MediatR;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using TripsManagement.Core.ApplicationServices.Trips.Commands.AddNewTrip;
using TripsManagement.Core.ApplicationServices.Trips.Commands.EditTrip;
using TripsManagement.Core.ApplicationServices.Trips.Queries.GetTrip;
using TripsManagement.Endpoint.WPF.Tools;

namespace TripsManagement.Endpoint.WPF.ViewModels
{
    public class TripFormViewModel : INotifyPropertyChanged
    {
        private readonly IMediator _mediator;
        private readonly IWindowTools _windowTools;
        public TripFormViewModel(IMediator mediator, IWindowTools windowTools)
        {
            _mediator = mediator;
            _windowTools = windowTools;
        }

        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) =>
        {

        };

        private FormModes _formMode;
        public FormModes FormMode
        {
            get
            {
                return _formMode;
            }
            set
            {
                _formMode = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(FormMode)));
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ActiveIsCanceled)));
            }
        }

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

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Title)));
            }
        }

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(StartDate)));
            }
        }

        private DateTime? _finishDate;
        public DateTime? FinishDate
        {
            get
            {
                return _finishDate;
            }
            set
            {
                _finishDate = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(FinishDate)));
            }
        }

        private bool _isCanceled;
        public bool IsCanceled
        {
            get
            {
                return _isCanceled;
            }
            set
            {
                _isCanceled = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(IsCanceled)));
            }
        }

        public bool ActiveIsCanceled
        {
            get
            {
                if (FormMode == FormModes.Edit)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
        }


        public async Task LoadAsEdit(Guid tripId)
        {
            var query = new GetTripQuery()
            {
                TripId = tripId
            };
            var res = await _mediator.Send(query);
            if (_windowTools.HandleErrorIfExist(res) == true)
            {
                return;
            }

            TripId = tripId;
            FinishDate = res.Data.FinishDate;
            StartDate = res.Data.StartDate;
            IsCanceled = res.Data.IsCanceled;
            Title = res.Data.Title;

            FormMode = FormModes.Edit;
        }

        public async Task NewTrip()
        {
            FormMode = FormModes.Add;
        }

        public async Task<bool> SaveAsync()
        {
            if (FormMode == FormModes.Add)
            {
                var command = new AddNewTripCommand()
                {
                    FinishDate = FinishDate ?? new DateTime(),
                    StartDate = StartDate ?? new DateTime(),
                    Title = Title
                };
                var res = await _mediator.Send(command);
                if (_windowTools.HandleErrorIfExist(res) == true)
                {
                    return false;
                }

                return true;
            }
            else if (FormMode == FormModes.Edit)
            {
                var command = new EditTripCommand()
                {
                    TripId = TripId,
                    FinishDate = FinishDate ?? new DateTime(),
                    StartDate = StartDate ?? new DateTime(),
                    Title = Title,
                    IsCanceled = IsCanceled
                };
                var res = await _mediator.Send(command);
                if (_windowTools.HandleErrorIfExist(res) == true)
                {
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}
