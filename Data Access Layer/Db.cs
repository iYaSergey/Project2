using Data_Layer;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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
            Sentiments = new Dictionary<char, Dictionary<string, double>>();
            PolygonsDes();
            SentimentsParse();
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

        private void SentimentsParse()
        {
            System.IO.StreamReader reader = new System.IO.StreamReader("../../../Data Access Layer/Data/sentiments.csv");
            string str;
            while (!reader.EndOfStream)
            {
                str = reader.ReadLine();
                if (Sentiments.Keys.Contains(str[0]))
                {
                    Dictionary<string, double> kekw = new Dictionary<string, double>();
                    Sentiments[str[0]].Add(str.Split(',')[0], double.Parse(str.Split(',')[1].Replace('.', ',')));
                }
                else if (str[0] >= 'a' && str[0] <= 'z')
                {
                    Dictionary<string, double> kekw = new Dictionary<string, double>();
                    kekw.Add(str.Split(',')[0], double.Parse(str.Split(',')[1].Replace('.', ',')));
                    Sentiments.Add(str[0], kekw);
                }
                else 
                {
                    if (Sentiments.Keys.Contains('0'))
                    {
                        Dictionary<string, double> kekw = new Dictionary<string, double>();
                        Sentiments['0'].Add(str.Split(',')[0], double.Parse(str.Split(',')[1].Replace('.', ',')));
                    }
                    else
                    {
                        Dictionary<string, double> kekw = new Dictionary<string,double>();
                        kekw.Add(str.Split(',')[0], double.Parse(str.Split(',')[1].Replace('.',',')));
                        Sentiments.Add('0', kekw);
                    }
                }
            }
        }
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
