using IdentificationNumber.Helpers;
using IdentificationNumber.Interfaces;
using System;
using System.Text.RegularExpressions;

namespace IdentificationNumber.Models
{
    public class PersonIdentificationNumber : IdentificationNumber
    {
        public PersonIdentificationNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            var number = Parse(value);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private string Parse(string value)
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
                var assumedDateOfBirth = new DateTime(GetDecade(DateTime.Today.Year) + year, month, day > 31 ? day - 60 : day);
                var dateOfBirth = assumedDateOfBirth <= DateTime.Today ? assumedDateOfBirth : new DateTime(GetDecade(DateTime.Today.Year) + year - 100 + prevDecade, month, day > 31 ? day - 60 : day);
            }
            else
            {
                // Assume four digits
            }

            if (day > 31)
            {
                // Assume coordination number
            }


            throw new NotImplementedException();
        }


        private int GetDecade(int year)
        {
            return Math.DivRem(year, 100, out int rem) * 100;
        }

        public string ConstructCenturyFigure(int year, int month, int day, bool overHundred = false)
        {
            if ((year < 0 && year > 99) || (year < 1000 && year > 9999))
                throw new ArgumentOutOfRangeException(nameof(year), "Must be between 0 and 99 or 1000 and 9999");

            if (month < 1 || month > 12)
                throw new ArgumentOutOfRangeException(nameof(month), "Must be between 1 and 12");

            if (day < 1 || (day > 32 && day < 61) || day > 91)
                throw new ArgumentOutOfRangeException(nameof(day), "Must be between 1 and 31 or 61 and 92");

            var currentCentury = DateTime.Today.Year;



            throw new NotImplementedException();
        }
    }
}
