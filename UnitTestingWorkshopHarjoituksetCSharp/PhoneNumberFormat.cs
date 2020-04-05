using System.Text.RegularExpressions;

namespace UnitTestingWorkshopHarjoituksetCSharp
{
    public sealed class PhoneNumberFormat
    {
        private readonly string m_number;

        public PhoneNumberFormat(string number)
        {
            m_number = number;
        }

        public string FormattedNumber()
        {
            return m_number == null ? string.Empty : FormatNumber();
        }

        private string FormatNumber()
        {
            const string internationalCallPrefix = "00";
            const int skipInternationalCallPrefixIndex = 2;
            const string removeExtraCharactersPattern = @"[\s-()]";

            var formatted = Regex.Replace(m_number, removeExtraCharactersPattern, string.Empty);

            if (formatted.StartsWith(internationalCallPrefix))
            {
                return "+" + formatted.Substring(skipInternationalCallPrefixIndex);
            }

            return formatted;
        }

        public bool ContainsOnlyAllowedCharacters()
        {
            if (m_number == null)
            {
                return true;
            }

            const string searchLettersPattern = @"[^\s-()+0-9]+";
            return !Regex.IsMatch(m_number, searchLettersPattern);
        }
    }
}