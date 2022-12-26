using psafth.IdentificationNumber.Interfaces;

namespace psafth.IdentificationNumber
{
    public abstract class IdentificationNumber : IIdentificationNumber
    {
        public IdentificationNumber(string value)
        {
            _value = value;
        }

        protected string _value;

        public abstract bool IsValid { get; }

        public abstract bool Equals(IIdentificationNumber other);

        public abstract string ToFormalString();

        public abstract override string ToString();
    }
}
