using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    public class State
    {
        public void Add(Tweet tweet)
        {
            Tweets.Add(tweet);
        }
        public State()
        {
            Tweets = new List<Tweet>();
        }
        public double Weight { get; set; }
        public List<Tweet> Tweets { get; set; }
        [JsonProperty("Polygons")]
        public List<List<List<double>>> Polygons { get; set; }
    }
}
