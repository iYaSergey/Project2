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
            States = Parser.PolygonsDes();
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
        public Dictionary<string, State> States { get; set; }
        public List<Tweet> Tweets { get; set;}
        public Dictionary<char, Dictionary<string, double>> Sentiments { get; set; }
    }
}
