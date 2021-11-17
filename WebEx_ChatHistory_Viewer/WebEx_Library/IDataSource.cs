using System.Collections.Generic;

namespace Service.Library
{
    public interface IDataSource
    {
        public List<Messages> ReadMessage(string path);

        public string[] ReadUsers(string path);

    }


}
