using config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core_server
{
     static class DatabaseConnection
     {
        static private string GetConnString()
        {
            string BaseStr = @"Data Source={0};Initial Catalog={1};User ID={2};Password={3}";
            return string.Format(BaseStr, Config.Database.DataSource, Config.Database.InitialCatalog, Config.Database.UserID, Config.Database.Password);
        }
        static public SqlConnection GetConnection()
        {
            SqlConnection Conn = new SqlConnection(GetConnString());
            Conn.Open();
            return Conn;
        }
    }
}
