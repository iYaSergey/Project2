using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data_Access_Layer;
using Data_Layer;

namespace Business_Layer
{
    public class Business : IBusiness
    {
        public Business()
        {

        }

        static readonly IMap mapDAO = new MapDAO();
        static readonly IState stateDAO = new StateDAO();
        static readonly ITweet tweetDAO = new TweetDAO();

        public void ParseTweets(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
