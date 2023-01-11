
using psafth.IdentificationNumber.Entities;
using psafth.IdentificationNumber.Helpers;
using psafth.IdentificationNumber.Interfaces;
using psafth.IdentificationNumber.Swedish.Extensions;
using psafth.IdentificationNumber.Swedish.Helpers;
using System;
using System.Linq;

namespace psafth.IdentificationNumber.Swedish.Entities
{
    public class PersonIdentificationNumber : IdentificationNumber<PersonIdentificationNumber>, IEquatable<string>
    {
        private PersonIdentificationNumber() { }

        public PersonNumberType Type
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

        public static bool TryParse(string value, out PersonIdentificationNumber personIdentificationNumber)
        {
            try
            {
                personIdentificationNumber = Parse(value);

                if (!personIdentificationNumber.IsValid)
                    personIdentificationNumber = null;

                return personIdentificationNumber.IsValid;
            }
            catch
            {
                personIdentificationNumber = null;
                return false;
            }
        }

        public static PersonIdentificationNumber Parse(string value)
        {
            return new PersonIdentificationNumber().ParseFromString(value);
        }

        public static implicit operator string(PersonIdentificationNumber personIdentificationNumber)
        {
            return personIdentificationNumber.ToString();
        }

        /// <summary>
        /// Create a <see cref="PersonIdentificationNumber"/> that can be used for creating bogus data.
        /// </summary>
        /// <param name="yearMonthDay">The birthday of the person</param>
        /// <param name="gender">The gender to set</param>
        /// <param name="numberType">The numbertype to set</param>
        /// <returns>Returns the created <see cref="PersonIdentificationNumber"/></returns>
        public static PersonIdentificationNumber Create(DateTime yearMonthDay, Gender gender, PersonNumberType numberType)
        {
            var rnd = new Random();
            int genderNumber;

            if (gender == Gender.Male)
            {
                // 1, 3, 5, 7, 9
                genderNumber = rnd.Next(1, 5) * 2 - 1;
            }
            else
            {
                // 0, 2, 4, 6, 8
                genderNumber = rnd.Next(0, 4) * 2;
            }

            // Add 60 to day if coordination number
            int day = yearMonthDay.Day + (numberType == PersonNumberType.Coordination ? 60 : 0);

            // Create random 2 digit number
            int firstTwoRnd = rnd.Next(0, 99);

            // Create partial 9 numbers (all but the controlnumber)
            string partial = $"{yearMonthDay:yyMM}{day:00}{firstTwoRnd:00}{genderNumber}";

            PersonIdentificationNumber personIdentificationNumber = new PersonIdentificationNumber();

            // Get the controlnumber and add it to get the full
            personIdentificationNumber._value = $"{yearMonthDay.Year / 100}{partial}{Luhn.GetControlNumber(partial)}";

            personIdentificationNumber.Type = numberType;
            personIdentificationNumber.DateOfBirth = yearMonthDay;

            // Parse and return
            return personIdentificationNumber;
        }

        /// <summary>
        /// Parses a string to a full valid person identification number.
        /// Valid inputs are:
        /// YYMMDD-XXXX
        /// YYMMDD+XXXX
        /// YYYYMMDDXXXX
        /// YYYYMMDD-XXXX
        /// 
        /// Coordination numbers are also valid.
        /// </summary>
        /// <param name="value">String input to be parsed</param>
        /// <exception cref="ArgumentNullException">When value is null</exception>
        /// <exception cref="FormatException">If value doesn't pass the Regex check</exception>
        /// <exception cref="ArgumentOutOfRangeException">If value passes the Regex check but date is invalid</exception>
        protected override PersonIdentificationNumber ParseFromString(string value)
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
                DateOfBirth = assumedDateOfBirth <= DateTime.Today ? assumedDateOfBirth : new DateTime(GetDecade(DateTime.Today.Year) + year - 100 + prevDecade, month, day > 31 ? day - 60 : day);
            }
            else
            {
                // Assume four digits
                DateOfBirth = new DateTime(year, month, day > 31 ? day - 60 : day);
            }

            // Set the type. Coordination or Person.
            if (day > 0)
                Type = day > 31 && day < 92 ? PersonNumberType.Coordination : PersonNumberType.Person;
            else
                Type = PersonNumberType.Unknown;

            _value = $"{DateOfBirth.Year:0000}{DateOfBirth.Month:00}{day:00}{individual:000}{control:0}";

            return this;
        }

        private int GetDecade(int year)
        {
            return Math.DivRem(year, 100, out _) * 100;
        }

        public bool Equals(string other)
        {
            return _value == other;
        }
    }
}
