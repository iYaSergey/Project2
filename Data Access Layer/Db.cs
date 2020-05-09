using Data_Layer;
using System.Collections.Generic;

namespace Data_Access_Layer
{
    public class Db
    {
        private static Db db;
        private Db()
        {
            Map = new Map();
        }
        public static Db GetInstance()
        {
            if (db == null)
            {
                db = new Db();
            }
            return db;
        }
        public Map Map { get; set; }
    }
}
