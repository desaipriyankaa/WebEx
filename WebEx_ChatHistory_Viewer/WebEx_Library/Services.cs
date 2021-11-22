using System.Collections.Generic;

namespace Service.Library
{
    public class Services
    {
        readonly IDataSource _dataSource;
        public Services(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public List<Messages> ReadUserChatData(string path)
        {
           return _dataSource.ReadMessage(path);
        }

        public List<string> ReadUserName(string path)
        {
            return _dataSource.ReadUsers(path);
        }
       
    }
}
