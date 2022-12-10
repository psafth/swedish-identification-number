using System;
using System.Collections.Generic;
using System.Linq;

namespace IdentityNumber.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class Luhn
    {
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

            return GetControlNumber(digits);
        }

        /// <summary>
        /// Calculates the control number of a given list of characters with the Luhn algorithm
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int GetControlNumber(IEnumerable<char> input)
        {
            var numbers = input.Select(x => int.Parse(x.ToString()));
            return GetControlNumber(numbers);
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
        /// Calculates the control number of a given list of characters with the Luhn algoritm implementation gathered by the Rosetta project.
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

            return new int[] { input / 10, input % 10 };
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