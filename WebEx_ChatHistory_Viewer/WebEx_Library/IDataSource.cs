using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Service.Library
{
    public interface IDataSource
    {
        public List<Messages> ReadMessage(string path);

        public string[] ReadUsers(string path);
        
    }


}
