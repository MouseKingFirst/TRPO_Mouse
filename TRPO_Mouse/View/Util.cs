using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRPO_Mouse.Model;

namespace TRPO_Mouse
{
    public static class Util
    {
        private static Entities database;
        public static Entities db
        {
            get
            {
                if (database == null)
                    database = new Entities();
                return database;
            }
        }
    }
}
