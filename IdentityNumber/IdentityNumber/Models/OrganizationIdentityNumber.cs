using IdentityNumber.Enums;
using IdentityNumber.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityNumber.Models
{
    public class OrganizationIdentityNumber : BaseIdentityNumber
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

        public bool IsValid => throw new NotImplementedException();

        public bool Equals(IIdentityNumber other)
        {
            throw new NotImplementedException();
        }
    }
}
