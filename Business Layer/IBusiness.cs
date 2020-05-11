﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data_Access_Layer;
using Data_Layer;

namespace Business_Layer
{
    public interface IBusiness
    {
        SortedList<string, string> GetFiles(string default_files);
        Map ParseTweets(string path);
        Map GetMap(string path);
    }
}
