using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WebEx_Library
{
    public interface IDataSource
    {
        public string ReadData(string path);

    }


}
