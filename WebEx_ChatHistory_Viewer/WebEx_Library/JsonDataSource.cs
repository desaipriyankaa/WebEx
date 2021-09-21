using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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

                var b = JsonConvert.DeserializeObject<List<Messages>>(str);
                string a = JsonConvert.SerializeObject(b);

                List<char> ch = new List<char>() { '{', '}', '[', ']', '/','"' };
                a = Filter(a, ch);

                a = a.Replace(",", "\n");
                return a;
            }

            throw new Exception();

        }

        public static string Filter(string str, List<char> charsToRemove)
        {
            charsToRemove.ForEach(c => str = str.Replace(c.ToString(), String.Empty));
            return str;
        }



        public string[] ReadUsers(string path)
        {
            string[] dirs = Directory.GetDirectories(path);
            return dirs;          
        }

        
    }

    public class Messages
    {
        public string PersonEmail { get; set; }
        public string Text { get; set; }
    }


}
