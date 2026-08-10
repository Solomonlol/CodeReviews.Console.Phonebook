using Backend.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Backend.Validation
{
    public static class Validation
    {
        public static bool IsPhoneNumber(string number)
        {
            var pattern = @"^\+?(\(\d{1,4}\)[\s]?|\d{1,4}[\s]?)?(\(\d{1,4}\)[\s]?|\d{1,4}[\s]?)?(\d{3}\-\d{2}\-\d{2}|\d{3}\-\d{4}|\d{7})$"; 
            if (Regex.IsMatch(number, pattern))
            {
                return true;
            }
            else return false;
        }
        public static bool IsEmailAddress(string email)
        {
            var pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (Regex.IsMatch(email, pattern))
            {
                return true;
            }
            else return false;
        }
    }   

}
