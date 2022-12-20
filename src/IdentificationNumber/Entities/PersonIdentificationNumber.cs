using IdentificationNumber.Enums;
using IdentificationNumber.Extensions;
using IdentificationNumber.Helpers;
using IdentificationNumber.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace IdentificationNumber.Models
{
    public class PersonIdentificationNumber : IdentificationNumber, IEquatable<string>
    {
        public NumberType Type
        {
            get; private set;
        }

        /// <summary>
        /// The persons gender based on the 10th digit in the identification number
        /// </summary>
        public Gender Gender
        {
            get
            {
                var genderDigit = int.Parse(_value.ElementAt(10).ToString());

                return genderDigit % 2 == 0 ? Gender.Female : Gender.Male;
            }
        }

        /// <summary>
        /// The persons date of birth
        /// </summary>
        public DateTime DateOfBirth { get; private set; }

        /// <summary>
        /// Creates a person identification number from a valid string.
        /// Valid inputs are:
        /// YYMMDD-XXXX
        /// YYMMDD+XXXX
        /// YYYYMMDDXXXX
        /// YYYYMMDD-XXXX
        /// 
        /// Coordination numbers are also valid.
        /// DD must be 1-31 or 61-91.
        /// </summary>
        /// <param name="value">String to be parsed as an personal identification number.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public PersonIdentificationNumber(string value) : base(value)
        {
            // Check that we have anything.
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            // Parse the value and get the date of birth.
            var parsedValue = Parse(value, out DateTime dateOfBirth, out NumberType type);

            // Set the date of birth.
            DateOfBirth = dateOfBirth;

            // Set the number type
            Type = type;

            // Store the value to the backing field.
            _value = parsedValue;
        }

        /// <summary>
        /// Compares two person identification numbers by matching their backing field value.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(string other)
        {
            try
            {
                var otherPersonIdentificationNumber = other.ToIdentificationNumber();
                return _value == otherPersonIdentificationNumber.ToString();
            }
            catch { return false; }
        }

        /// <summary>
        /// Compares two person identification numbers by matching their backing field value.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(IIdentificationNumber other)
        {
            return _value == other.ToString();
        }

        /// <summary>
        /// Validates a person identification number. True if valid. False if not.
        /// </summary>
        public override bool IsValid
        {
            get
            {
                return Luhn.Validate(_value.Substring(2));
            }
        }

        /// <summary>
        /// Outputs the formal string of a person identificatio number
        /// </summary>
        /// <returns>Younger than 100 years: YYMMDD-XXXX. Older than 100 years: YYMMDD+XXXX</returns>
        public override string ToFormalString()
        {
            var birth = _value.Substring(2, 6);
            var lastFour = _value.Substring(8, 4);

            var separator = (DateTime.Today.AddYears(-100) <= DateOfBirth) ? "-" : "+";

            return $"{birth}{separator}{lastFour}";
        }

        public override string ToString()
        {
            return base._value;
        }

        /// <summary>
        /// Parses a string to a full valid person identification number and passes the date of birth as an out parameter.
        /// Valid inputs are:
        /// YYMMDD-XXXX
        /// YYMMDD+XXXX
        /// YYYYMMDDXXXX
        /// YYYYMMDD-XXXX
        /// 
        /// Coordination numbers are also valid.
        /// </summary>
        /// <param name="value">String input to be parsed</param>
        /// <param name="dateOfBirth">DateTime </param>
        /// <returns>String in the format of YYYYMMDDXXXX</returns>
        /// <exception cref="FormatException"></exception>
        private string Parse(string value, out DateTime dateOfBirth, out NumberType type)
        {
            var match = CommonRegex.MatchPerson(value);

            if (!match.Success)
                throw new FormatException("The input does not match a valid person identification number.");

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

            // Set the type. Coordination or Person.
            if (day > 0)
                type = day > 31 && day < 92 ? NumberType.Coordination : NumberType.Person;
            else
                type = NumberType.Unknown;

            return $"{dateOfBirth.Year:0000}{dateOfBirth.Month:00}{day:00}{individual:000}{control:0}";
        }

        private int GetDecade(int year)
        {
            return Math.DivRem(year, 100, out int rem) * 100;
        }

        public static bool IsMatching(string value)
        {
            return CommonRegex.MatchPerson(value).Success;
        }
    }
}
