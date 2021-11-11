using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Service.Library
{
    public class LoginCredentialData
    {
        static string _loginCredentialDataFilePath = @"D:\WPF Trial\WebExChatHistoryViewerTrial2\WebExChatHistoryViewer\WebEx_ChatHistory_Viewer\WebEx_Library\LoginCredentialDataFile.txt";

        public string EmailID { get; set; }
        public string BrowsePath { get; set; }

        public void SaveData()
        {
            File.Delete(_loginCredentialDataFilePath);
            FileInfo fileInfo = new FileInfo(_loginCredentialDataFilePath);
            StreamWriter writer = fileInfo.CreateText();

            string data = $"{EmailID},{BrowsePath}";
            writer.WriteLine(data);
            writer.Close();
        }

        public void ReadData()
        {
            FileInfo fileInfo = new FileInfo(_loginCredentialDataFilePath);
            if (fileInfo.Exists)
            {
                StreamReader reader = fileInfo.OpenText();
                string read = reader.ReadLine();
                while (!string.IsNullOrEmpty(read))
                {
                    string[] data = read.Split(",");
                    read.Trim();
                    EmailID = data[0];
                    BrowsePath = data[1];
                    read = reader.ReadLine();

                }
                reader.Close();
            }
        }
    }
}
