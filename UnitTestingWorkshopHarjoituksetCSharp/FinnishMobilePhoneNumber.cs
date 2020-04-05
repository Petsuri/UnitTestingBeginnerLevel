using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestingWorkshopHarjoituksetCSharp
{
    public sealed class FinnishMobilePhoneNumber : IFinnishMobilePhoneNumber
    {
        private const int SkipFirstZeroCharacter = 1;
        private const string FinnishCountryCode = "+358";

        private readonly string m_number;

        public FinnishMobilePhoneNumber(string number)
        {
            if (!IsValid(number))
            {
                throw new ArgumentException("Given number is not valid Finnish consumer mobile number", nameof(number));
            }

            m_number = number;
        }

        public string GetFormattedNumber()
        {
            var formatted = FormatPhoneNumber(m_number);
            if (formatted.StartsWith(FinnishCountryCode))
            {
                return formatted;
            }

            return FinnishCountryCode + formatted.Substring(SkipFirstZeroCharacter);
        }

        public static bool IsValid(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return false;
            }

            if (!ContainsOnlyAllowedCharacters(number))
            {
                return false;
            }

            var formattedPhoneNumber = FormatPhoneNumber(number);
            if (!IsValidAreaCode(formattedPhoneNumber))
            {
                return false;
            }

            if (number.StartsWith(FinnishCountryCode))
            {
                var finnishPhoneNumberWithoutCountryCode = formattedPhoneNumber.Substring(FinnishCountryCode.Length);
                return IsValidLength(finnishPhoneNumberWithoutCountryCode);
            }

            var finnishPhoneNumberWithoutFirstZero = formattedPhoneNumber.Substring(SkipFirstZeroCharacter);
            return IsValidLength(finnishPhoneNumberWithoutFirstZero);
        }

        private static bool ContainsOnlyAllowedCharacters(string number)
        {
            var formatter = new PhoneNumberFormat(number);
            return formatter.ContainsOnlyAllowedCharacters();
        }

        private static string FormatPhoneNumber(string number)
        {
            var formatter = new PhoneNumberFormat(number);
            return formatter.FormattedNumber();
        }

        private static bool IsValidLength(string number)
        {
            const int numberMinimumLength = 5;
            const int numberMaximumLength = 12;

            return numberMinimumLength <= number.Length && number.Length <= numberMaximumLength;
        }

        public static bool IsValidAreaCode(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                return false;
            }

            var consumerAreaCodes = new List<string>() {"04", "05", "+3584", "+3585"};
            var businessAreaCodes = new List<string>() {"010", "020", "029", "030", "039", "071", "073", "075", "+35810", "+35820", "+35829", "+35830", "+35839", "+35871", "+35873", "+35875"};

            return consumerAreaCodes.Any(phoneNumber.StartsWith) || businessAreaCodes.Any(phoneNumber.StartsWith);
        }
    }
}
