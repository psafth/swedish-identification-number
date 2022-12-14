using IdentificationNumber.Helpers;
using IdentificationNumber.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace IdentificationNumber.Models
{
    public class PersonIdentificationNumber : IdentificationNumber, IEquatable<string>
    {
        public DateTime DateOfBirth { get; private set; }

        public PersonIdentificationNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            _value = Parse(value, out DateTime dateOfBirth);

            DateOfBirth = dateOfBirth;
        }

        public bool Equals(string other)
        {
            return _value == other;
        }

        public override bool Equals(IIdentificationNumber other)
        {
            throw new NotImplementedException();
        }

        public override bool IsValid
        {
            get
            {
                return Luhn.Validate(_value.Substring(2));
            }
        }

        public override string ToFriendlyName()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return base._value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private string Parse(string value, out DateTime dateOfBirth)
        {
            var match = CommonRegex.MatchPerson(value);

            if (!match.Success)
                throw new FormatException("The input does not match an valid person identification format.");

            var year = int.Parse(match.Groups["year"]?.Value);
            var month = int.Parse(match.Groups["month"]?.Value);
            var day = int.Parse(match.Groups["day"]?.Value);
            var separator = match.Groups["separator"]?.Value;
            var individual = match.Groups["individual"]?.Value;
            var control = int.Parse(match.Groups["control"]?.Value);


            if (year < 1000)
            {
                var prevDecade = separator == "+" ? -100 : 0;

                // Assume two digits
                var assumedDateOfBirth = new DateTime(GetDecade(DateTime.Today.Year) + year + prevDecade, month, day > 31 ? day - 60 : day);
                dateOfBirth = assumedDateOfBirth <= DateTime.Today ? assumedDateOfBirth : new DateTime(GetDecade(DateTime.Today.Year) + year - 100 + prevDecade, month, day > 31 ? day - 60 : day);
            }
            else
            {
                // Assume four digits
                dateOfBirth = new DateTime(year, month, day);
            }

            return $"{dateOfBirth.Year:0000}{dateOfBirth.Month:00}{day:00}{individual:000}{control:0}";
        }

        private int GetDecade(int year)
        {
            return Math.DivRem(year, 100, out int rem) * 100;
        }
    }
}
