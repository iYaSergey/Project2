using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    public class State
    {
        public State()
        {
            Tweets = new List<Tweet>();
        }
        public string Name { get; set; }
        public double Weight { get; set; }
        public List<Tweet> Tweets { get; set; }


        //
        public List<Tweet> Polygon { get; set; }
        //
    }
}
