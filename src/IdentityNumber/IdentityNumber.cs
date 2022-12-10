using IdentityNumber.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityNumber
{
    public abstract class IdentityNumber : IIdentityNumber
    {
        protected string _value;

        public abstract bool IsValid { get; }

        public abstract bool Equals(IIdentityNumber other);

        public abstract string ToFriendlyName();

        public abstract override string ToString();
    }
}
