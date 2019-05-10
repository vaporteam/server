using config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core_server
{
    class Status : Common.Status
    {

        public void Insert(object data)
        {
            throw new NotImplementedException();
        }

        public void Select()
        {
            SqlConnection Conn = DatabaseConnection.GetConnection();
            // ToDo Implement Method
            string SQL = "SELECT * FROM STATUS";
            SqlCommand SqlQuery = new SqlCommand(SQL, Conn);
            SqlDataReader DataReader = SqlQuery.ExecuteReader();
            Conn.Close();
        }
    }
}
