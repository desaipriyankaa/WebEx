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
            bool inputBrowse = false; //browsePath.Contains("DirectChats");
            //return inputBrowse;
            JsonDataSource jsonData = new JsonDataSource();
            List<string> dirs = jsonData.ReadUsers(browsePath).ToList();
            foreach (var dir in dirs)
            {
                if (dir == @"D:\WPF Trial\WebExChatHistoryViewerTrial2\WebExChatHistoryViewer\WebexDump\DirectChats")
                {
                    inputBrowse = true;
                }
                else if (dir == @"D:\WPF Trial\WebExChatHistoryViewerTrial2\WebExChatHistoryViewer\WebexDump\Teams")
                {
                    inputBrowse = true;
                }
                else
                {
                    inputBrowse = false;
                }
            }
            return inputBrowse;
        }
    }
}
