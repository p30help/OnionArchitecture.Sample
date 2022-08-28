using System;

namespace TripsManagement.Core.ApplicationServices.Common
{
    public class RequestResult
    {
        public RequestResultTypes ResultType { get; set; }

        public Exception Exception { get; set; }

        public static RequestResult Ok()
        {
            return new RequestResult()
            {
                ResultType = RequestResultTypes.Ok
            };
        }

        public static RequestResult Error(Exception exp)
        {
            return new RequestResult()
            {
                Exception = exp,
                ResultType = RequestResultTypes.Error
            };
        }
    }

    public class RequestResult<TResponse> : RequestResult
    {
        public TResponse Data { get; set; }

        public static RequestResult<TResponse> Ok(TResponse value)
        {
            return  new RequestResult<TResponse>()
            {
                Data = value,
                ResultType = RequestResultTypes.Ok
            };
        }

        public static RequestResult<TResponse> Error(Exception exp)
        {
            return new RequestResult<TResponse>()
            {
                Exception = exp,
                ResultType = RequestResultTypes.Error
            };
        }
    }

    public enum RequestResultTypes
    {
        Ok = 0,
        Error = 1
    }
}
