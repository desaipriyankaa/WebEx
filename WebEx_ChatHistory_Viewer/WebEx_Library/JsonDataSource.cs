using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            string a = SendMessage(path);
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

                //string data = JsonConvert.SerializeObject(json);
                string data = MessagesWithoutPropertyFields(json);
                
                return data;
            }

            throw new Exception();
        }

        public string MessagesWithoutPropertyFields(List<Messages> msg)
        {
            string str = "";

            foreach (var item in msg)
            {
                str = str + "\n" + item.ToString() + "\n";
            }
            return str;
                                
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
        public string Created { get; set; }

        public string SplitEmail()
        {
            String[] parts = PersonEmail.Split(new[] { '@' });
            String username = parts[0]; 
            String domain = parts[1];

            return username;
        }
        public override string ToString()
        {
            return SplitEmail() + "         " + Created + " \n " + Text;
        }
    }


}
