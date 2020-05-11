using System.Collections.Generic;
using System.IO;

using Business_Layer;
using Data_Layer;

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
        public Map GetMap(string path)
        {
            return business.GetMap(path);
        }
    }
}
