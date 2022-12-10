using IdentityNumber.Enums;
using IdentityNumber.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityNumber.Models
{
    public class OrganizationIdentityNumber : IdentityNumber
    {
        public CorporateForm Form
        {
            get
            {
                switch (base._value)
                {
                    case string x when x.StartsWith("2") && !x.StartsWith("20"):
                        return CorporateForm.ReligiousCommunity;
                    default:
                        return CorporateForm.Unknown;

                }
            }
        }

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
