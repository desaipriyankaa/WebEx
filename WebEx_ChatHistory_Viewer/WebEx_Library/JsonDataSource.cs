using System;
using System.IO;

namespace WebEx_Library
{
    public class JsonDataSource : IDataSource
    {
        public string ReadData(string path)
        {
            //var x = @"F:\PProject\WPF\WebexDump\DirectChats\Dhiren Goyal\messages.json";
            FileInfo FBD = new FileInfo(path);

            if (FBD.Exists)
            {
                StreamReader reader = FBD.OpenText();
                string str = reader.ReadToEnd();

                return str;
            }

            throw new Exception();

        }
    }


}
