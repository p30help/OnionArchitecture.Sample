using System.Text.RegularExpressions;
using TripsManagement.Core.DomainModels.Exceptions;

namespace TripsManagement.Core.DomainModels.ValueObjects
{
    public class MobileNumber : BaseValueObject<MobileNumber>
    {
        public MobileNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidValueObjectStateException("Please enter the mobile number");
            }

            if (value.StartsWith("+") == false)
            {
                throw new InvalidValueObjectStateException("Please enter the mobile number with country code");
            }

            if (IsNumeric(value.Remove(0, 1)) == false)
            {
                throw new InvalidValueObjectStateException("The mobile number was not entered correctly");
            }

            Value = value;
        }

        public string Value { get; }

        public override bool ObjectIsEqual(MobileNumber otherObject)
        {
            return Value == otherObject.Value;
        }

        public override int ObjectGetHashCode()
        {
            return GetHashCode();
        }

        private  bool IsNumeric(string str)
        {
            if (Regex.IsMatch(str, @"^\d+$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static MobileNumber CreateIfNotEmpty(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
            {
                return null;
            }

            return new MobileNumber(mobile);
        }
    }
}