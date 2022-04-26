using System;
using System.Text.RegularExpressions;

namespace StringHelpers
{
    public static class StringHelper
    {
        public static string ToFirstLetterUpper(this string str)
        {
            string firstLetter = str[0].ToString().ToUpper();
            string restStr = str[1..].ToLower();
            return firstLetter + restStr;
        }

        public static int WordsCount(this string str)
        {
            return new Regex(@"\w+").Matches(str).Count;
        }

        public static bool IsNumeric(this string str)
        {
            if (str == "NaN" || str == "Inf" || str == "-Inf" || str == "+Inf")
            {
                return true;
            }
            return double.TryParse(str, out _);
        }
    }
}
