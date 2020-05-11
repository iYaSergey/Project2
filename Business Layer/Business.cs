using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data_Access_Layer;
using Data_Layer;

namespace Business_Layer
{
    public class Business : IBusiness
    {
        static readonly Db db = Db.GetInstance();
        public Business()
        {
            TweetWeightCalc();
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

        public Map GetMap(string path)
        {
            return null;
        }
        
        private void TweetWeightCalc()
        {
            foreach (Tweet tw in db.Tweets)
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
                            foreach (KeyValuePair<string, double> sent in db.Sentiments[subStr[0]])
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
                            foreach (KeyValuePair<string, double> sent in db.Sentiments['0'])
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
        }
        public Map ParseTweets(string path)
        {
            throw new NotImplementedException();
        }
    }
}