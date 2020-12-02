using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MundiPagg.Domain.Shared
{
    public class ValidateOrder
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                static string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();

                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        internal static bool IsValidLine1(string line1)
        {
            if (line1.Split(',').Length == 3)
                return true;

            return false;
        }

        internal static bool IsValidAddressStatus(string status)
        {
            if (status == string.Empty)
                return true;

            if (status == "active")
                return true;

            if (status == "deleted")
                return true;

            return false;
        }

        internal static bool IsValidItemStatus(string status)
        {
            if (status == "active")
                return true;

            if (status == "deleted")
                return true;

            return false;
        }

        public static bool IsValidDocument(string document)
        {
            if (document.Length == 11 || document.Length == 14 || !IsNumeric(document))
                return true;

            return false;
        }

        public static bool IsValidCustomerType(string type)
        {
            if (type == "individual")
                return true;
            
            if (type == "company")
                return true;
            
            return false;
        }

        public static bool IsValidChargeStatus(string status)
        {
            if (status == "pending")
                return true;

            if (status == "paid")
                return true;

            if (status == "canceled")
                return true;

            if (status == "processing")
                return true;

            if (status == "failed")
                return true;

            if (status == "overpaid")
                return true;

            if (status == "underpaid")
                return true;

            return false;
        }

        public static bool IsValidDate(string date)
        {
            if (DateTime.TryParse(date, CultureInfo.GetCultureInfo("pt-BR"), DateTimeStyles.NoCurrentDateDefault, out _))
                return true;

            return false;
        }

        public static bool IsNumeric(string stringNumber)
        {
            if (long.TryParse(stringNumber, out _))
                return true;

            return false;
        }
    }
}
