using System.Collections.Generic;

namespace Data_Layer
{
    public class Map
    {
        public Map()
        {
            States = new List<State>();
        }
        public List<State> States { get; set; }
    }
}
