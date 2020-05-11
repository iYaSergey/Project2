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
        static readonly IParser parser = new Parser();
        public Business()
        {

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
        public Map ParseTweets(string path)
        {
            throw new NotImplementedException();
        }
    }
}