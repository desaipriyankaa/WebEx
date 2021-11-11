using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Service.Library
{
    public class Validation
    {
        public bool isValidEmail(string inputEmail)
        {
            string pattern = @"^[a-zA-Z0-9]{3,20}@[A-Za-z]{3,10}.(com|co.in|co.us)$";
            Regex regex = new Regex(pattern);
            bool IsValid = false;

            if (regex.IsMatch(inputEmail))
            {
                IsValid = true;
                return IsValid;
            }
            else
            {
                IsValid = false;
                return IsValid;
            }
        }
    }
}
