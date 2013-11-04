using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.DataObjects
{
    class DBConexion
    {
        public SqlConnection conn { get; set; }
        public SqlCommand cmd { get; set; }
        public SqlTransaction trans { get; set; }

        public DBConexion()
        {
            conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
        }
    }
}
