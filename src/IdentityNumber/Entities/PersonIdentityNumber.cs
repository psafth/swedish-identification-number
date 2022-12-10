using IdentityNumber.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityNumber.Models
{
    public class PersonIdentityNumber : IdentityNumber
    {
        public override bool Equals(IIdentityNumber other)
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
