using System.Collections.Generic;

using Business_Layer;
using Data_Layer;

using GMap.NET.WindowsForms;

namespace Service_Layer
{
    public class Service : IService
    {
        static readonly IBusiness business = new Business();
        public Service()
        {

        }
        public SortedList<string, string> GetFiles(string default_path)
        {
            return business.GetFiles(default_path);
        }
        public List<GMapPolygon> GetPolygons(string path)
        {
            return business.GetPolygons(path);
        }
        public List<Tweet> GetTweets()
        {
            return business.GetTweets();
        }
    }
}
