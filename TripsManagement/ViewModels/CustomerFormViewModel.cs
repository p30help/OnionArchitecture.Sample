using MediatR;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using TripsManagement.Core.ApplicationServices.Customers.Commands.AddNewCustomer;
using TripsManagement.Core.ApplicationServices.Customers.Commands.EditCustomer;
using TripsManagement.Core.ApplicationServices.Customers.Queries.GetCustomer;
using TripsManagement.Endpoint.WPF.Tools;

namespace TripsManagement.Endpoint.WPF.ViewModels
{
    public class CustomerFormViewModel : INotifyPropertyChanged
    {
        private readonly IMediator _mediator;
        private readonly IWindowTools _windowTools;
        public CustomerFormViewModel(IMediator mediator, IWindowTools windowTools)
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
            }
        }

        private Guid _customerId;
        public Guid CustomerId
        {
            get
            {
                return _customerId;
            }
            set
            {
                _customerId = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(CustomerId)));
            }
        }

        private string _firstName;
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(FirstName)));
            }
        }

        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(LastName)));
            }
        }

        private string _mobile;
        public string Mobile
        {
            get
            {
                return _mobile;
            }
            set
            {
                _mobile = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Mobile)));
            }
        }

        private short? _age;
        public short? Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Age)));
            }
        }


        public async Task LoadAsEdit(Guid customerId)
        {
            var query = new GetCustomerQuery()
            {
                CustomerId = customerId
            };
            var res = await _mediator.Send(query);

            if (_windowTools.HandleErrorIfExist(res) == true)
            {
                return;
            }

            CustomerId = customerId;
            FirstName = res.Data.FirstName;
            LastName = res.Data.LastName;
            Mobile = res.Data.Mobile;
            Age = res.Data.Age;

            FormMode = FormModes.Edit;
        }

        public async Task NewCustomer()
        {
            FormMode = FormModes.Add;
        }

        public async Task<bool> SaveAsync()
        {
            if (FormMode == FormModes.Add)
            {
                var command = new AddNewCustomerCommand()
                {
                    Age = Age ?? 0,
                    FirstName = FirstName,
                    LastName = LastName,
                    Mobile = Mobile
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
                var command = new EditCustomerCommand()
                {
                    CustomerId = CustomerId,
                    Age = Age ?? 0,
                    FirstName = FirstName,
                    LastName = LastName,
                    Mobile = Mobile
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
