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
using System.Windows.Media.Animation;

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
            dao.Clear();
            dao.ClearStates();

            dao.SetTweets(path);
            Dictionary<string, State> states = dao.GetStates();
            Dictionary<char, Dictionary<string, double>> sentiments = dao.GetSentiments();
            List<GMapPolygon> polygons = CreatePolygons(states);
            List<Tweet> tweets = dao.GetTweets();

            TweetWeightCalc(sentiments, tweets);
            GroupTweets(states, polygons, tweets);
            StateWeightCalc(states);
            double[] extreme_values = GetExtremes(states);
            SetColors(polygons, states, extreme_values);

            return polygons;
        }
        private double[] GetExtremes(Dictionary<string, State> states)
        {
            double[] extreme_values = new double[2];
            extreme_values[0] = 0;
            extreme_values[1] = 0;
            int pos_count = 0, neg_count = 0;
            foreach (State state in states.Values)
            {
                if (state.Weight < 0)
                {
                    extreme_values[1] -= state.Weight;
                    neg_count++;
                }
                else if (state.Weight > 0)
                {
                    extreme_values[0] += state.Weight;
                    pos_count++;
                }
            }
            extreme_values[1] /= neg_count;
            extreme_values[0] /= pos_count;
            return extreme_values;
        }
        private List<GMapPolygon> SetColors(List<GMapPolygon> polygons, Dictionary<string, State> states, double[] extreme_values)
        {
            foreach (GMapPolygon polygon in polygons)
            {
                polygon.Fill = new SolidBrush(GetColor(states[polygon.Name], extreme_values));
            }
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
                    GMapPolygon gMapPolygon = new GMapPolygon(gMapPolygon_coords, state.Key)
                    {
                        Stroke = new Pen(Color.Black, 1)
                    };
                    polygons.Add(gMapPolygon);
                }
            }
            return polygons;
        }
        private Color GetColor(State state, double[] extreme_values)
        {
            int red = 255, green = 255;
            double x_red, x_green;
            
            if (extreme_values[1] != 0)
            {
                x_red = Math.Abs(255 / extreme_values[1]);
            }
            else x_red = 255;
            if (extreme_values[0] != 0)
            {
                x_green = 255 / extreme_values[0];
            }
            else x_green = 255;
            double weight = state.Weight;
            if (weight == 0)
            {
                red = 255;
                green = 255;
            }
            else if (weight > 0)
            {
                red = 255 - Convert.ToInt32(weight * x_green);
                green = 255;
            }
            else
            {
                red = 255;
                green = 255 - Convert.ToInt32(Math.Abs(weight) * x_red);
            }
            if (green > 255) green = 255;
            else if (green < 0) green = 0;
            if (red > 255) red = 255;
            else if (red < 0) red = 0;
            
            Color color = Color.FromArgb(100, Color.FromArgb(red, green, 0));
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
                while (j < str.Length - 1)
                {
                    bool isGet = false;
                    for (int i = str.Length - 1; i > j; i--)
                    {
                        bool flag = false;
                        string subStr = str.Substring(j, i - j + 1);
                        if (subStr[0] >= 'a' && subStr[0] <= 'z')
                        {
                            Dictionary<string, double> sent = sentiments[subStr[0]];
                            if (sent.Keys.Contains(subStr))
                            {
                                tw.Weight += sent[subStr];
                                j += subStr.Length-1;
                                flag = true;
                                break;
                            }
                            if (!flag)
                            {
                                while (str[i] != ' ' && i > j)
                                {
                                    i--;
                                }
                            }
                        }
                        else
                        {
                            foreach (KeyValuePair<string, double> sent in sentiments['0'])
                            {
                                Dictionary<string, double> senti = sentiments['0'];
                                if (senti.Keys.Contains(subStr))
                                {
                                    tw.Weight += senti[subStr];
                                    j += subStr.Length-1;
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
                        while (str[j] != ' ' && j != str.Length - 1)
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