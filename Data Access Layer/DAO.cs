using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data_Layer;

namespace Data_Access_Layer
{
    public class DAO : IDAO
    {
        static readonly Db db = Db.GetInstance();
        public void SetTweets(string path)
        {
            db.Tweets = Parser.ParseFile(path);
        }
        public List<Tweet> GetTweets()
        {
            return db.Tweets;
        }
        public Dictionary<string, State> GetStates()
        {
            return db.States;
        }
        public Dictionary<char, Dictionary<string, double>> GetSentiments()
        {
            return db.Sentiments;
        }
    }
}
