using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FluentValidation;
using TripsManagement.Core.ApplicationServices.Common;

namespace TripsManagement.Endpoint.WPF.Tools
{
    public interface IWindowTools
    {
        public bool HandleErrorIfExist(RequestResult requestResult);
    }

    public class WindowTools : IWindowTools
    {
        public bool HandleErrorIfExist(RequestResult requestResult)
        {
            if (requestResult.ResultType == RequestResultTypes.Error)
            {
                if (requestResult.Exception is ValidationException)
                {
                    var valExp = requestResult.Exception as ValidationException;
                    var firstError = valExp.Errors.FirstOrDefault();

                    MessageBox.Show(firstError?.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                else if (requestResult.Exception != null)
                {
                    MessageBox.Show(requestResult.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("Unexpected error occurred.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                return true;
            }

            return false;
        }
    }
}
