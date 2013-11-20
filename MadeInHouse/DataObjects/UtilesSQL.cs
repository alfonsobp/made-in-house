using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MadeInHouse.DataObjects
{
    class UtilesSQL
    {
        private DBConexion db = null;
        private bool tipo = true;

        public UtilesSQL (DBConexion db = null)
        {

            if (db == null)
            {
                this.db = new DBConexion();
            }
            else
            {
                this.db = db;
                tipo = false;
            }

        }

        public void LimpiarTabla(string tName)
        {
            db.cmd.CommandText = "TRUNCATE TABLE " + tName;
            try
            {
               if(tipo) db.conn.Open();
                db.cmd.ExecuteNonQuery();
                if (tipo) db.conn.Close();
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
                if (tipo) db.conn.Open();
                reader = db.cmd.ExecuteReader();
                if (reader.Read())
                    id = reader.GetInt32(0);
                if (tipo)  db.conn.Close();
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
