using System.Collections.Generic;

namespace Service.Library
{
    public interface IDataSource
    {
        public List<Messages> ReadMessage(string path);

        public List<string> ReadUsers(string path);

    }


}
