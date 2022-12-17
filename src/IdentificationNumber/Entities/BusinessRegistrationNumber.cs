using IdentificationNumber.Enums;
using IdentificationNumber.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentificationNumber.Models
{
    public class BusinessRegistrationNumber : IdentificationNumber
    {
        public BusinessRegistrationNumber(string value) : base(value)
        {
            // TODO: Implement
        }

        public BusinessForm Form
        {
            get
            {
                switch (base._value)
                {
                    case string x when x.StartsWith("2") && !x.StartsWith("20"):
                        return BusinessForm.ReligiousCommunity;
                    default:
                        return BusinessForm.Unknown;

                }
            }
        }

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

        public static bool IsMatching(string value)
        {
            throw new NotImplementedException();
        }
    }
}
