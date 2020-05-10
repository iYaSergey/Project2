using Data_Layer;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Data_Access_Layer
{
    public class Db
    {
        private static Db db;
        private Db()
        {
            Map = new Map();
            Tweets = TweetParser.ParseFile();
            States = new List<State>();
            PolygonsDes();
        }
        public static Db GetInstance()
        {
            if (db == null)
            {
                db = new Db();
            }
            return db;
        }
        public Map Map { get; set; }
        public List<Tweet> Tweets { get; set;}
        public List<State> States { get; set; }

        private void PolygonsDes()
        {
            string JsonString = new StreamReader(@"../../../Data Access Layer/Data/states.json").ReadToEnd();
            Map.States = JsonConvert.DeserializeObject<Dictionary<string, State>>(JsonString);
        }

        public void ParseFile(string filename)
        {
            Tweets = TweetParser.ParseFile(filename);
        }
    }
}
