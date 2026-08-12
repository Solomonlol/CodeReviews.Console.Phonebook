using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Backend.Validation
{
    public static class Validation
    {
        public static bool IsPhoneNumber(string number)
        {
            var pattern = @"^\+?(\(\d{1,4}\)[\s]?|\d{1,4}[\s]?)?(\(\d{1,4}\)[\s]?|\d{1,4}[\s]?)?(\d{3}\-\d{2}\-\d{2}|\d{3}[\s]\d{2}[\s]\d{2}|\d{3}\-\d{4}|\d{7})$"; 
            if (Regex.IsMatch(number, pattern))
            {
                return true;
            }
            else return false;
        }
        public static bool IsEmailAddress(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var address = new MailAddress(email);
                var toCheck = address.Host.Split('.');
                return toCheck.Length >= 2 && toCheck.All(p => !string.IsNullOrEmpty(p));
            }
            catch { return false; }
        }
    }   

}
