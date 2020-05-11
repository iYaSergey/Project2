using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Data_Access_Layer;
using Data_Layer;

using GMap.NET.WindowsForms;

namespace Business_Layer
{
    public interface IBusiness
    {
        SortedList<string, string> GetFiles(string default_files);
        List<GMapPolygon> GetPolygons(string path);
    }
}
