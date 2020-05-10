using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data_Access_Layer;
using Data_Layer;

namespace Business_Layer
{
    public interface IBusiness
    {
        void ParseTweets(string filename);
    }
}
