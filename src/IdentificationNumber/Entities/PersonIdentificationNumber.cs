using IdentificationNumber.Interfaces;
using System;

namespace IdentificationNumber.Models
{
    public class PersonIdentificationNumber : IdentificationNumber
    {
        public PersonIdentificationNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            if (value.Length < 10 && value.Length > 13)
                throw new FormatException("Input was not in a correct format");

            throw new NotImplementedException();
        }

        public override bool Equals(IIdentificationNumber other)
        {
            throw new NotImplementedException();
        }

        public override bool IsValid { get; }

        public override string ToFriendlyName()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
