using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    public class Tweet
    {
        public Tweet()
        {
            Location = new double[2];
        }
        public Tweet(double d1, double d2, string text):this()
        {
            Location[0] = d1;
            Location[1] = d2;
            Text = text;
        }


        public string Text { get; set; }
        public double Weight { get; set; }
        public double[] Location { get; set; }
    }
}
