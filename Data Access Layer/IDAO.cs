using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data_Layer;

namespace Data_Access_Layer
{
    public interface IDAO
    {
        void SetTweets(string path);
        List<Tweet> GetTweets();
        Dictionary<string, State> GetStates();
        Dictionary<char, Dictionary<string, double>> GetSentiments();
    }
}
