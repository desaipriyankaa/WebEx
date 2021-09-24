using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Service.Library
{
    public class JsonDataSource : IDataSource
    {
        //public string ReadData(string path)
        //{
        //    FileInfo fileInfo = new FileInfo(path);

        //    if (fileInfo.Exists)
        //    {
        //        StreamReader reader = fileInfo.OpenText();
        //        string str = reader.ReadToEnd();

        //        var b = JsonConvert.DeserializeObject<List<Messages>>(str);
        //        string a = JsonConvert.SerializeObject(b);

        //        List<string> chh = new List<string>() { "{", "},", "," };
        //        a = NewLine(a, chh);

        //        List<char> ch = new List<char>() { '{', '}', '[', ']', '/', '"' };
        //        a = Filter(a, ch);

        //        return a;
        //    }

        //    throw new Exception();

        //}

        public string ReadData(string path)
        {

                var a = SendMessage(path);

                List<string> chh = new List<string>() { "{", "},", "," };
                a = NewLine(a, chh);

                List<char> ch = new List<char>() { '{', '}', '[', ']', '/', '"' };
                a = Filter(a, ch);

                return a;
        }

        public string SendMessage(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                StreamReader reader = fileInfo.OpenText();
                string str = reader.ReadToEnd();

                var json = JsonConvert.DeserializeObject<List<Messages>>(str);
                string data = JsonConvert.SerializeObject(json);
                return data;
            }

            throw new Exception();
        }

        public string NewLine(string str, List<string> AddNewline)
        {
            AddNewline.ForEach(c => str = str.Replace(c.ToString(), "\n"));
            return str;
        }

        public string Filter(string str, List<char> charsToRemove)
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
