﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data_Layer;

namespace Data_Access_Layer
{
    public class TweetDAO : ITweet
    {
        public TweetDAO()
        {
            db = Db.GetInstance();
        }

        Db db;
    }
}
