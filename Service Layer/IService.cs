using System.Collections.Generic;

using Data_Layer;

using GMap.NET.WindowsForms;

namespace Service_Layer
{
    public interface IService
    {
        SortedList<string, string> GetFiles(string default_path);
        List<GMapPolygon> GetPolygons(string path);
        List<Tweet> GetTweets();
    }
}
