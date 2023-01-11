using System;
using psafth.IdentificationNumber.Helpers;
using psafth.IdentificationNumber.Interfaces;
using psafth.IdentificationNumber.Swedish.Extensions;
using psafth.IdentificationNumber.Swedish.Helpers;

namespace psafth.IdentificationNumber.Swedish.Entities
{
    public class BusinessRegistrationNumber : IdentificationNumber<BusinessRegistrationNumber>, IEquatable<string>
    {
        private BusinessRegistrationNumber() { }

        public BusinessForm BusinessForm
        {
            get;
            private set;
        }

        public override bool IsValid
        {
            get
            {
                return Luhn.Validate(_value);
            }
        }

        public override string ToFormalString()
        {
            return _value.Insert(6, "-");
        }

        public static bool TryParse(string value, out BusinessRegistrationNumber businessRegistrationNumber)
        {
            try
            {
                businessRegistrationNumber = Parse(value);

                if (!businessRegistrationNumber.IsValid)
                    businessRegistrationNumber = null;

                return businessRegistrationNumber.IsValid;
            }
            catch
            {
                businessRegistrationNumber = null;
                return false;
            }
        }

        public static BusinessRegistrationNumber Parse(string value)
        {
            return new BusinessRegistrationNumber().ParseFromString(value);
        }

        public static implicit operator string(BusinessRegistrationNumber businessRegistrationNumber)
        {
            return businessRegistrationNumber.ToString();
        }

        protected override BusinessRegistrationNumber ParseFromString(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var match = CommonRegex.MatchBusiness(value);

            if (!match.Success)
                throw new FormatException("The input does not match a valid person identification number.");

            var group = int.Parse(match.Groups["group"]?.Value); // Business form
            var number = match.Groups["number"]?.Value;
            var serial = match.Groups["serial"]?.Value;
            var control = int.Parse(match.Groups["control"]?.Value);

            BusinessForm = (BusinessForm)group;

            _value = $"{group}{number}{serial}{control}";

            return this;
        }

        public bool Equals(string other)
        {
            return _value == other;
        }
    }
}
