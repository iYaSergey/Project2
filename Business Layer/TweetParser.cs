using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data_Access_Layer;
using Data_Layer;

namespace Business_Layer
{
    public class TweetParser
    {
        public TweetParser()
        {
            Sentiments = new SortedList<string, SortedList<string, double>>();
        }
        public SortedList<string, SortedList<string, double>> Load(string path)
        {

            return Sentiments;
        }
        public SortedList<string, SortedList<string, double>> Sentiments {get; set;}
    }
}
