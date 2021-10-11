using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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
            //messages = MessageAlignment();
            return messages;

        }

        //public List<Messages> MessageAlignment()
        //{
        //    string email = "Sanket.Naik@klingelnberg.com";

        //    List<Messages> msg = new List<Messages>();
        
        //    foreach (var item in msg)
        //    {
        //        if (item.PersonEmail == email)
        //        {
        //           msg = email.PadRight(10);
        //        }
        //    }
        //    return msg;
        //}

        //public string MessagesWithoutPropertyFields(List<Messages> msg)
        //{
        //    string str = "";
            
        //    foreach (var item in msg)
        //    {
        //        str = str + "\n" + item.ToString() + "\n";
        //    }
        //    return str;
                                
        //}
        
       
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
        public DateTime DateTime { get; set; }

        //public string SplitEmail()
        //{
        //    String[] parts = PersonEmail.Split(new[] { '@' });
        //    String username = parts[0]; 
        //    String domain = parts[1];

        //    return username;
        //}

        //public override string ToString()
        //{
        //    return SplitEmail() + "         " + Created + " \n " + Text;
        //}
    }
}
