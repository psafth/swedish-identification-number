using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace psafth.IdentificationNumber.Swedish.Helpers
{
    public static class CommonRegex
    {
        public static Match MatchPerson(string input)
        {
            string pattern = @"^(?<year>\d{4}|\d{2})(?<month>(0[1-9]|1[0-2]))(?<day>(0[1-9]|[12][0-9]|3[01])|(6[1-9]|[78][0-9]|9[01]))(?<separator>[\-\+]?)(?<individual>[0-9]{3})(?<control>[0-9]{1})$";
            var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.Singleline, TimeSpan.FromMilliseconds(20));
            return regex.Match(input);
        }

        public static Match MatchBusiness(string input)
        {
            string pattern = @"^(?<group>[1-9-[4]]{1})(?<number>[\d]{1}[2-9]{1}[\d]{1}[\d]{2})(?<separator>[\-]{0,1})(?<serial>[\d]{3})(?<control>[\d]{1})$";
            var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.Singleline, TimeSpan.FromMilliseconds(20));
            return regex.Match(input);
        }
    }
}
