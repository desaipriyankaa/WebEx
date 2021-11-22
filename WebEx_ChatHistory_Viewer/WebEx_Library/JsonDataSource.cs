using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Service.Library
{
    public class JsonDataSource : IDataSource
    {
        public List<Messages> ReadMessage(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            List<Messages> messages =new List<Messages>();

            if (fileInfo.Exists)
            {
                StreamReader reader = fileInfo.OpenText();
                string str = reader.ReadToEnd();

                messages = JsonConvert.DeserializeObject<List<Messages>>(str);
            }

            foreach (var item in messages)
            {
                item.PersonEmail = SplitEmail(item.PersonEmail);
            }
            return messages;

        }

        public string SplitEmail(string email)
        {
     
            String[] parts = email.Split(new[] { '@' });
            String username = parts[0];
            String domain = parts[1];

            return username;
        }

        public List<string> ReadUsers(string path)
        {
            var dirs = new List<string>();
            if (string.IsNullOrEmpty(path)) return dirs;
            dirs = Directory.GetDirectories(path).ToList();
            return dirs;
        }
        
    }
}
