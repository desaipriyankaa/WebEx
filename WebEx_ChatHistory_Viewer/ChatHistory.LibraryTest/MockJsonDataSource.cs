using System;
using System.IO;

namespace Service.Library
{
    public class MockJsonDataSource : IDataSource
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

        public string[] ReadUsers(string path)
        {
            throw new NotImplementedException();
        }
    }


}
