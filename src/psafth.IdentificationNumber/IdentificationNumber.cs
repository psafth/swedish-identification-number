using psafth.IdentificationNumber.Interfaces;
using System.Collections.Generic;
using System;

namespace psafth.IdentificationNumber
{
    public abstract class IdentificationNumber<T> : IIdentificationNumber<T>, IEqualityComparer<T>, IEquatable<T> where T : IdentificationNumber<T>
    {
        protected internal string _value;

        public abstract bool IsValid { get; }

        T IIdentificationNumber<T>.ParseFromString(string value)
        {
            throw new NotImplementedException();
        }

        protected abstract T ParseFromString(string value);

        public bool Equals(T x, T y)
        {
            try
            {
                return x.ToString() == y.ToString();
            }
            catch
            {
                return false;
            }
        }

        public int GetHashCode(T obj)
        {
            return (obj.ToString() ?? string.Empty).GetHashCode();
        }

        public bool Equals(T other)
        {
            return _value == other.ToString();
        }

        public abstract string ToFormalString();

        public override string ToString()
        {
            return _value;
        }


    }
}
