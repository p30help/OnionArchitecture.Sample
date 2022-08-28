namespace TripsManagement.Core.DomainModels.Exceptions
{
    public class InvalidValueObjectStateException : DomainStateException
    {
        public InvalidValueObjectStateException(string message, params string[] parameters) : base(message)
        {
            Parameters = parameters;
        }
    }
}