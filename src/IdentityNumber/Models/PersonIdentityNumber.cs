using IdentityNumber.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityNumber.Models
{
    public class PersonIdentityNumber : IIdentityNumber
    {
        public bool IsValid => throw new NotImplementedException();

        public bool Equals(IIdentityNumber other)
        {
            throw new NotImplementedException();
        }
    }
}
