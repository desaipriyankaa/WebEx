using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Service.Library
{
    public class Validation
    {
        public bool isValidEmail(string inputEmail)
        {
            string pattern = @"^[a-zA-Z0-9]{3,20}.[a-zA-Z0-9]{3,20}@(klingelnberg).(com)$";
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

        public bool isValidBrowsePath(string browsePath)
        {
            bool inputBrowse = browsePath.Contains("WebexDump");
            return inputBrowse;
        }
    }
}
