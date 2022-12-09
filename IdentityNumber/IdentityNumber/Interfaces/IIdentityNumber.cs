using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityNumber.Interfaces
{
    public interface IIdentityNumber
    {
        string ToString();
        bool Equals(IIdentityNumber other);
        bool IsValid { get; }
    }
}
