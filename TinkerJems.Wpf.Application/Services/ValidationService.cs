using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace TinkerJems.Wpf.Application.Services
{
    public class ValidationService
    {
        public bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public bool ValidateQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                return false;
            }

            if(quantity > 50)
            {
                return false;
            }

            if (quantity.GetType().FullName != "System.Int32")
            {
                return false;
            }

            return true;
        }

        public List<string> GetAllSizesByCategory(string category)
        {
            if(category == "Ring")
            {
                return RingSizeList;
            }
            if (category == "Necklace")
            {
                return NecklaceSizeList;
            }
            if (category == "Bracelet")
            {
                return BraceletSizeList;
            }
            if (category == "Earring")
            {
                return EarringSizeList;
            }

            return RingSizeList;
        }

        private List<string> RingSizeList = new List<string>
            {
                "0.5", "1", "1.5", "2", "2.5", "3", "3.5", "4", "4.5", "5", "5.5", "6", "6.6",
                "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5",
                "13", "13.5", "14"
            };
        private List<string> BraceletSizeList = new List<string>
            {
                "Small: 17-19 cm",
                "Medium: 19-21 cm",
                "Large: 21-23 cm",
                "Extra Large: 23-25 cm"
            };
        private List<string> NecklaceSizeList = new List<string>
            {
                "14 inches",
                "16 inches",
                "18 inches",
                "20 inches",
                "22 inches",
                "24 inches",
                "30 inches",
                "36 inches"
            };
        private List<string> EarringSizeList = new List<string>
            {
                "Small",
                "Medium",
                "Large",
                "Extra Large"
            };
    }
}
