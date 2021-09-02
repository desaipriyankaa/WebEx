using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WebEx_ChatHistory_Library
{
    public class WebEx_DataSource
    {
        public string ReadRightSideData()
        {
            var x = @"F:\PProject\WPF\WebexDump\DirectChats\Dhiren Goyal\messages.json";
            FileInfo FBD = new FileInfo(x);

            string[] files = Directory.GetFiles(x);
            string[] dirs = Directory.GetDirectories(x);

            return File.ReadAllText(x);

        }
    }


}
