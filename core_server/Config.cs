using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace config
{
    public class Config
    {
        public class Server
        {
            public static string Adress { get; set; } = "0.0.0.0";
            public static int Port { get; set; } = 8080;
        }

        public class Database
        {
            public static string DataSource { get; set; } = "WIN-50GP30FGO75";
            public static string InitialCatalog { get; set; } = "Demodb";
            public static string UserID { get; set; } = "sa";
            public static string Password { get; set; } = "demol23";
        }
    }
}
