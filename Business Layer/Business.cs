using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Ink;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data_Access_Layer;
using Data_Layer;

using GMap.NET.WindowsForms;
using GMap.NET;

namespace Business_Layer
{
    public class Business : IBusiness
    {
        static readonly IDAO dao = new DAO();
        public Business()
        {

        }
        public List<GMapPolygon> GetPolygons(string path)
        {
            dao.SetTweets(path);
            Dictionary<string, State> states = dao.GetStates();
            Dictionary<char, Dictionary<string, double>> sentiments = dao.GetSentiments();
            List<GMapPolygon> polygons = CreatePolygons(states);
            List<Tweet> tweets = dao.GetTweets();

            TweetWeightCalc(sentiments, tweets);
            GroupTweets(states, polygons, tweets);
            StateWeightCalc(states);

            return polygons;
        }
        private Dictionary<string, State> GroupTweets(Dictionary<string, State> states, List<GMapPolygon> polygons, List<Tweet> tweets)
        {
            foreach (Tweet tweet in tweets)
            {
                foreach (GMapPolygon polygon in polygons)
                {
                    if (polygon.IsInside(new PointLatLng(tweet.Location[0], tweet.Location[1])))
                    {
                        states[polygon.Name].Add(tweet);
                        break;
                    }
                }
            }
            return states;
        }
        private List<GMapPolygon> CreatePolygons(Dictionary<string, State> states)
        {
            List<GMapPolygon> polygons = new List<GMapPolygon>();
            foreach (var state in states)
            {
                Color color = GetColor(state.Value);
                foreach (var polygon in state.Value.Polygons)
                {
                    List<PointLatLng> gMapPolygon_coords = new List<PointLatLng>();
                    foreach (var coords in polygon)
                    {
                        double y = coords[0];
                        double x = coords[1];
                        PointLatLng point = new PointLatLng(x, y);
                        gMapPolygon_coords.Add(point);
                    }
                    GMapPolygon gMapPolygon = new GMapPolygon(gMapPolygon_coords, state.Key);
                    //gMapPolygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
                    gMapPolygon.Fill = new SolidBrush(color);
                    gMapPolygon.Stroke = new Pen(Color.Black, 1);
                    polygons.Add(gMapPolygon);
                }
            }
            return polygons;
        }
        public Color GetColor(State state)
        {
            int red = 255;
            int green = 255;
            Color color = Color.FromArgb(50, Color.FromArgb(red, green, 0));

            return color;
        }
        public SortedList<string, string> GetFiles(string default_path)
        {
            SortedList<string, string> files = new SortedList<string, string>();
            try
            {
                DirectoryInfo dir = new DirectoryInfo(default_path);
                foreach (FileInfo file in dir.GetFiles("*.txt"))
                {
                    files.Add(file.Name, file.FullName);
                }
            }
            catch
            {
                return null;
            }
            return files;
        }
        private List<Tweet> TweetWeightCalc(Dictionary<char, Dictionary<string, double>> sentiments, List<Tweet> tweets)
        {
            foreach (Tweet tw in tweets)
            {
                string str = tw.Text.ToLower();
                int j = 0;
                while(j<str.Length-1)
                {
                    bool isGet = false;
                    for (int i = str.Length-1; i > j;i--)
                    {
                        bool flag = false;
                        string subStr = str.Substring(j,i-j+1);
                        if (subStr[0] >= 'a' && subStr[0] <= 'z')
                        {
                            foreach (KeyValuePair<string, double> sent in sentiments[subStr[0]])
                            {
                                if (sent.Key == subStr)
                                {
                                    tw.Weight += sent.Value;
                                    j += subStr.Length;
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                while (str[i]!=' ' && i > j)
                                {
                                    i--;
                                }
                            }
                        }
                        else 
                        {
                            foreach (KeyValuePair<string, double> sent in sentiments['0'])
                            {
                                if (sent.Key == subStr)
                                {
                                    tw.Weight += sent.Value;
                                    j += subStr.Length;
                                    flag = true;
                                    break;
                                }
                            }
                        }

                        if (flag)
                        {
                            isGet = true;
                            break;
                        }
                    }
                    if (!isGet)
                    {
                        while (str[j] != ' ' && j != str.Length-1)
                        {
                            j++;
                        }
                        if (str[j] == ' ')
                        {
                            j++;
                        }
                    }
                }
            }
            return tweets;
        }
        private Dictionary<string, State> StateWeightCalc(Dictionary<string, State> states)
        {
            foreach (State state in states.Values)
            {
                double weight = 0;
                foreach (Tweet tweet in state.Tweets)
                {
                    weight += tweet.Weight;
                }
                state.Weight = weight;
            }
            return states;
        }
    }
}