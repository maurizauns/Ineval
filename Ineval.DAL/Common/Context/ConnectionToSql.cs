using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.DAL
{
    public class ConnectionToSql
    {
        public static SqlConnection getConnection()
        {
            string conn = string.Empty;
            conn = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection aConnection = new SqlConnection(conn);
            return aConnection;
        }
    }
}
