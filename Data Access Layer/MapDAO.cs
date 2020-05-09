using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class MapDAO : IMap
    {
        public MapDAO()
        {
            db = Db.GetInstance();
        }

        Db db;
    }
}
