﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Service.Library
{
    public class JsonDataSource : IDataSource
    {
        public string ReadData(string path)
        {
            FileInfo fileInfo = new FileInfo(path);

            if (fileInfo.Exists)
            {
                StreamReader reader = fileInfo.OpenText();
                string str = reader.ReadToEnd();

                return str;
            }

            throw new Exception();

        }

        public string[] ReadUsers(string path)
        {
            string[] dirs = Directory.GetDirectories(path);
            return dirs;            
        }
    }


}
