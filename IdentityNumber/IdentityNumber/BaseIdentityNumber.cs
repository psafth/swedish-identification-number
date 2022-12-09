using IdentityNumber.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityNumber
{
    public class BaseIdentityNumber : IIdentityNumber
    {
        protected string _value;

        public bool IsValid => throw new NotImplementedException();

        public bool Equals(IIdentityNumber other)
        {
            throw new NotImplementedException();
        }
    }
}
