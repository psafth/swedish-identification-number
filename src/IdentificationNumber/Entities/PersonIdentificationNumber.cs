using IdentificationNumber.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentificationNumber.Models
{
    public class PersonIdentificationNumber : IdentificationNumber
    {
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
