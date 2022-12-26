using System;
using psafth.IdentificationNumber.Helpers;
using psafth.IdentificationNumber.Interfaces;
using psafth.IdentificationNumber.Swedish.Extensions;
using psafth.IdentificationNumber.Swedish.Helpers;

namespace psafth.IdentificationNumber.Swedish.Entities
{
    public class BusinessRegistrationNumber : IdentificationNumber, IEquatable<string>
    {
        public BusinessRegistrationNumber(string value) : base(value)
        {
            // Check that we have anything.
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            // Parse the value and get the business form
            var parsedValue = Parse(value, out BusinessForm businessForm);

            // Set the business form
            this.BusinessForm = businessForm;

            // Store the value to the backing field.
            _value = parsedValue;
        }

        public BusinessForm BusinessForm
        {
            get;
            private set;
        }

        public override bool Equals(IIdentificationNumber other)
        {
            return _value == other.ToString();
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

        public bool Equals(string other)
        {
            try
            {
                var otherBusinessRegistrationNumber = other.ToIdentificationNumber();
                return _value == otherBusinessRegistrationNumber.ToString();
            }
            catch { return false; }
        }


        public override string ToString()
        {
            return _value;
        }

        public static bool IsMatching(string value)
        {
            return CommonRegex.MatchBusiness(value).Success;
        }

        private string Parse(string value, out BusinessForm businessForm)
        {
            var match = CommonRegex.MatchBusiness(value);

            if (!match.Success)
                throw new FormatException("The input does not match a valid person identification number.");

            var group = int.Parse(match.Groups["group"]?.Value);        // Business form
            var number = match.Groups["number"]?.Value;
            var serial = match.Groups["serial"]?.Value;
            var control = int.Parse(match.Groups["control"]?.Value);

            businessForm = (BusinessForm)group;

            return $"{group}{number}{serial}{control}";
        }
    }
}
