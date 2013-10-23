using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.DataObjects.Almacen
{
    class ColorSQL
    {

        private DBConexion db;

        public ColorSQL(){
            db = new DBConexion();
        }


        public List<Color> BuscarZona(int codigo)
        {
            List<Color> listaColor = new List<Color>();
            
            string where = "WHERE 1=1 ";

            where = where + " AND idColor=@idColor ";
            db.cmd.Parameters.Add(new SqlParameter("idColor",codigo.ToString()));
            

            db.cmd.CommandText = "SELECT * FROM Color " + where;

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {
                    Color p = new Color();
                    p.IdColor = reader.IsDBNull(reader.GetOrdinal("idColor")) ? -1 : int.Parse(reader["idColor"].ToString());
                    p.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();
                    p.CodHex = reader.IsDBNull(reader.GetOrdinal("codHex")) ? null : reader["codHex"].ToString();

                    listaColor.Add(p);
                }

                db.conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

            return listaColor;
        }

        public List<Color> BuscarZona()
        {
            List<Color> listaColor = new List<Color>();

            string where = "WHERE 1=1 ";

            db.cmd.CommandText = "SELECT * FROM Color " + where;

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {
                    Color p = new Color();
                    p.IdColor = reader.IsDBNull(reader.GetOrdinal("idColor")) ? -1 : int.Parse(reader["idColor"].ToString());
                    p.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();
                    p.CodHex = reader.IsDBNull(reader.GetOrdinal("codHex")) ? null : reader["codHex"].ToString();

                    listaColor.Add(p);
                }

                db.conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

            return listaColor;
        }


    }
}
