using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Service.Library
{
    public interface IDataSource
    {
        public string ReadData(string path);

        public string[] ReadUsers(string path);
        

    }


}
