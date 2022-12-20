using System;
using System.Collections.Generic;
using System.Text;

namespace IdentificationNumber.Interfaces
{
    public interface IIdentificationNumber
    {
        string ToFormalString();
        bool Equals(IIdentificationNumber other);
        bool IsValid { get; }
    }
}
