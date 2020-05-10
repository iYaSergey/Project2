using Data_Layer;
using System.Collections.Generic;

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

        public void ParseFile(string filename)
        {
            Tweets = TweetParser.ParseFile(filename);
        }
    }
}
