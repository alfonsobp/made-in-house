using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MadeInHouse.DataObjects
{
    public class UtilesSQL
    {
        private DBConexion db = null;
        public UtilesSQL()
        {
            db = new DBConexion();
        }

        public void LimpiarTabla(string tName)
        {
            db.cmd.CommandText = "TRUNCATE TABLE " + tName;
            try
            {
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }

        }

        public int ObtenerMaximoID (string nombreTabla, string nombreID) 
        {

            int id=0;
            SqlDataReader reader;
            db.cmd.CommandText = "SELECT MAX("+nombreID+") FROM "+nombreTabla;
            try
            {
                db.conn.Open();
                reader = db.cmd.ExecuteReader();
                if (reader.Read())
                    id = reader.GetInt32(0);
                db.conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

            return id;
        }



    }
}
