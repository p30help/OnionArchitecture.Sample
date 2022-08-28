using TripsManagement.Core.DomainModels.Exceptions;

namespace TripsManagement.Core.DomainModels.ValueObjects
{
    public class HumanAgeField : BaseValueObject<HumanAgeField>
    {

        public HumanAgeField(short value)
        {
            if (value <= 0 || value > 120)
            {
                throw new InvalidValueObjectStateException("Age must be more than zero and less than 120");
            }

            this.Value = value;
        }

        public short Value { get; }

        public override bool ObjectIsEqual(HumanAgeField otherObject)
        {
            return Value == otherObject.Value;
        }

        public override int ObjectGetHashCode()
        {
            return GetHashCode();
        }
    }
}