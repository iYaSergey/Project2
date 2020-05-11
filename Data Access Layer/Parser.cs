using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Data_Access_Layer;
using Data_Layer;
using Newtonsoft.Json;

namespace Data_Access_Layer
{
    public class Parser
    {
        public Parser()
        {

        }
        public static Dictionary<char, Dictionary<string, double>> SentimentsParse()
        {
            Dictionary<char, Dictionary<string, double>> Sentiments = new Dictionary<char, Dictionary<string, double>>();
            StreamReader reader = new StreamReader("../../../../Data Access Layer/Data/sentiments.csv");
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
                        Dictionary<string, double> kekw = new Dictionary<string, double>();
                        kekw.Add(str.Split(',')[0], double.Parse(str.Split(',')[1].Replace('.', ',')));
                        Sentiments.Add('0', kekw);
                    }
                }
            }
            return Sentiments;
        }
        public static Dictionary<string, State> PolygonsDes()
        {
            string JsonString = new StreamReader(@"../../../../Data Access Layer/Data/states.json").ReadToEnd();
            return JsonConvert.DeserializeObject<Dictionary<string, State>>(JsonString);
        }
        public static List<Tweet> ParseFile(string path)
        {
            List<Tweet> tweets = new List<Tweet>();
            StreamReader reader = new StreamReader(path);
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
                foreach (char c in text)
                {
                    if (char.IsPunctuation(c))
                    {
                        text = text.Replace(c.ToString(),"");
                    }
                }
                Tweet tweet = new Tweet(double.Parse(d1), double.Parse(d2), text);
                tweets.Add(tweet);
            }
            return tweets;
        }
    }
}
