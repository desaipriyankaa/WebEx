using System;
using System.IO;

namespace Service.Library
{
    public class MockJsonDataSource : IDataSource
    {
        public string ReadData(string path)
        {
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
