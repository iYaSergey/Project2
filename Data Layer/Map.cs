using Newtonsoft.Json;
using System.Collections.Generic;

namespace Data_Layer
{
    
    public class Map
    {
        public Map()
        {
            States = new Dictionary<string, State>();
        }

        public Dictionary<string,State> States { get; set; }
    }
}
