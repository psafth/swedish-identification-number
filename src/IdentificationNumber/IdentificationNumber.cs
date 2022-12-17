using IdentificationNumber.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentificationNumber
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

        public abstract string ToFriendlyName();

        public abstract override string ToString();
    }
}
