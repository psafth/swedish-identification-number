using System;
using System.Text.RegularExpressions;

namespace psafth.IdentificationNumber.Swedish.Helpers
{
    public static class CommonRegex
    {
        /// <summary>
        /// Regex pattern for checking a swedish identification number with or without a seperator
        /// </summary>
        private const string _matchPersonPattern = @"^\s*(?<year>\d{4}|\d{2})(?<month>(0[1-9]|1[0-2]))(?<day>(0[1-9]|[12][0-9]|3[01])|(6[1-9]|[78][0-9]|9[01]))\s*(?<separator>[\-\+]?)\s*(?<individual>[0-9]{3})(?<control>[0-9]{1})\s*$";

        /// <summary>
        /// Regex pattern for checking a business identification number with or without a seperator
        /// </summary>
        private const string _matchBusinessPattern = @"^\s*(?<group>[1-9-[4]]{1})(?<number>[\d]{1}[2-9]{1}[\d]{1}[\d]{2})\s*(?<separator>[\-]{0,1})\s*(?<serial>[\d]{3})(?<control>[\d]{1})\s*$";

        /// <summary>
        /// Compiled Regex for matching a person
        /// </summary>
        private static Regex _matchPerson = new Regex(_matchPersonPattern, RegexOptions.Compiled | RegexOptions.Singleline, TimeSpan.FromMilliseconds(20));

        /// <summary>
        /// Compiled Regex for matching a business
        /// </summary>
        private static Regex _matchBusiness = new Regex(_matchBusinessPattern, RegexOptions.Compiled | RegexOptions.Singleline, TimeSpan.FromMilliseconds(20));

        public static Match MatchPerson(string input)
        {
            return _matchPerson.Match(input);
        }

        public static Match MatchBusiness(string input)
        {
            return _matchBusiness.Match(input);
        }

    }
}
