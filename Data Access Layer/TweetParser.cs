using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Data_Access_Layer;
using Data_Layer;

namespace Data_Access_Layer
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

        public static List<Tweet> ParseFile(string file_name = @"../../../Data Access Layer/Data/cali_tweets2014.txt")
        {
            List<Tweet> tweets = new List<Tweet>();
            System.IO.StreamReader reader = new System.IO.StreamReader(file_name);
            string str;
            string text="";
            Regex regexLoc = new Regex(@"-?\d*\.\d*");
            Regex regexText = new Regex(@"\d\t");
            Regex regexsobaka = new Regex(@" ?(@\w*)[:]? ?");
            Regex regexlink = new Regex(@"http[s]?://[\w\./]+");
            Regex regexkoska = new Regex(", ");
            while (!reader.EndOfStream)
            {
                str = reader.ReadLine();
                MatchCollection LocCol = regexLoc.Matches(str);
                string d1 = LocCol[0].Value.Replace('.', ',');
                string d2 = LocCol[1].Value.Replace('.', ',');
                text = regexText.Split(str)[1];
                MatchCollection sobaka = regexsobaka.Matches(text);
                MatchCollection link = regexlink.Matches(text);
                foreach (Match s in sobaka)
                {
                    text = text.Replace(s.Value,"");
                }
                foreach (Match s in link)
                {
                    text = text.Replace(s.Value,"");
                }
                Tweet tweet = new Tweet(double.Parse(d1), double.Parse(d2), text);
            }
            return tweets;
        }
    }
}
