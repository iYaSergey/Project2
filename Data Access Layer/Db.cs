using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using Data_Layer;

using Newtonsoft.Json;

namespace Data_Access_Layer
{
    public class Db
    {
        private static Db db;
        private Db()
        {
            Map = new Map();
            Map.States = Parser.PolygonsDes();
            Tweets = Parser.ParseFile("../../../../Data Access Layer/Data/cali_tweets2014.txt");
            States = new List<State>();
            Sentiments = Parser.SentimentsParse();
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
        public Dictionary<char, Dictionary<string, double>> Sentiments { get; set; }
        public void ParseFile(string filename)
        {
            Tweets = Parser.ParseFile(filename);
        }
    }
}
