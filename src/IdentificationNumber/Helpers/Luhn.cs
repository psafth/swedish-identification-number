using System;
using System.Collections.Generic;
using System.Linq;

namespace IdentificationNumber.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class Luhn
    {
        /// <summary>
        /// Validates an given string (of integers) with the Luhn algorithm. The last character is used a the control number.
        /// </summary>
        /// <param name="input">String of integers ending with a control number</param>
        /// <returns>True if valid. False if not.</returns>
        public static bool Validate(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;

            if (input.Length < 2)
                throw new ArgumentOutOfRangeException(nameof(input));

            var charLastDigit = input.Last();

            var controlDigit = int.Parse(charLastDigit.ToString());

            return Luhn.Validate(input.Remove(input.Length - 1, 1), controlDigit);
        }

        /// <summary>
        /// Validates an given string (of integers) by comparing it to the given expected control number.
        /// </summary>
        /// <param name="input">String of integers without control number</param>
        /// <param name="expectedControlNumber">The expected control number</param>
        /// <returns>True if valid. False if not.</returns>
        public static bool Validate(string input, int expectedControlNumber)
        {
            var calculatedControlNumber = Luhn.GetControlNumber(input);

            return expectedControlNumber == calculatedControlNumber;
        }


        /// <summary>
        /// Calculates the control number of a given string with the Luhn algorithm
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static int GetControlNumber(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var digits = input.Select(c => c);

            return Luhn.GetControlNumber(digits);
        }

        /// <summary>
        /// Calculates the control number of a given list of characters with the Luhn algorithm
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int GetControlNumber(IEnumerable<char> input)
        {
            var numbers = input.Select(x => int.Parse(x.ToString()));
            return Luhn.GetControlNumber(numbers);
        }

        /// <summary>
        /// Calculates the control number of a given list of integers with the Luhn algorithm
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int GetControlNumber(IEnumerable<int> input)
        {
            var x = input.Reverse()
                .SelectMany((c, i) => Split(i % 2 == 0 ? c * 2 : c));

            var sum = x.Sum();

            return RoundUp(sum) - sum;
        }

        /// <summary>
        /// Calculates the control number of a given list of characters with the Luhn algoritm implementation gathered by the Rosetta Code project.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int RosettaControlNumber(IEnumerable<char> input)
        {
            return input.Select((d, i) => i % 2 == input.Count() % 2 ? ((2 * d) % 10) + d / 5 : d).Sum() % 10;
        }


        /// <summary>
        /// Splits any double digits into an array of on or two digits. Any number below 0 or higher than 99 will result in an ArgumentOutOfRangeException.
        /// </summary>
        /// <param name="input">Integer to split.</param>
        /// <returns>The input split into one or two digits.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static int[] Split(int input)
        {
            if (input < 0)
                throw new ArgumentOutOfRangeException(nameof(input));

            if (input > 99)
                throw new ArgumentOutOfRangeException(nameof(input));

            if (input < 10)
                return new int[] { input };

            var q = Math.DivRem(input, 10, out int rem);
            return new int[] { q, rem };
        }

        /// <summary>
        /// Rounds up to the nearest higher multiple of ten.
        /// </summary>
        /// <param name="toRound">The integer to round</param>
        /// <returns>the higher multiple of ten. Ex. 2 returns 10, 18 return 20</returns>
        private static int RoundUp(int toRound)
        {
            if (toRound % 10 == 0) return toRound;
            return (10 - toRound % 10) + toRound;
        }
    }
}