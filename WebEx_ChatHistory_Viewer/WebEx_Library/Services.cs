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

        public string ReadUserChatData(string path)
        {
           return _dataSource.ReadData(path);
        }
        public string[] ReadUserName(string path)
        {
            return _dataSource.ReadUsers(path);
        }
       
    }
}
