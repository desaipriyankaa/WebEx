namespace WebEx_Library
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
    }
}
