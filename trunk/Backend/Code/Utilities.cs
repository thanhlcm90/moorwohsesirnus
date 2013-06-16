using System;
using System.Text;
using System.Text.RegularExpressions;

namespace SunriseShowroom.Code
{
    public class Utilities
    {
        public static string ConvertToUnSign(string s)
        {
            var regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            var temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').Replace(' ', '-').Replace("---", "-").Replace("--", "-");
        }
    }
}