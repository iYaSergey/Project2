using System.Collections.Generic;

using Data_Layer;

namespace Service_Layer
{
    public interface IService
    {
        SortedList<string, string> GetFiles(string default_path);
        Map ParseTweets(string path);
        Map GetMap(string path);
    }
}
