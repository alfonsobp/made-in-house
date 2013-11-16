using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.DataObjects.Reportes
{
    class reporteStock
    {
        public static List<Almacenes> BuscarAlmacen()
        {
            List<Almacenes> lstAlmacen = new List<Almacenes>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT nombre FROM Almacen order by 1";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Almacenes e = new Almacenes();
                    e.Nombre = reader["nombre"].ToString();
                    lstAlmacen.Add(e);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }

            return lstAlmacen;
        }

        public static List<Almacenes> BuscarAlmacenesxTienda(string tienda)
        {
            List<Almacenes> lstAlmacenes = new List<Almacenes>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "select * from almacen a join tienda t on t.idtienda = a.idTienda where t.nombre ='" + tienda + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Almacenes e = new Almacenes();
                    e.Nombre = reader["nombre"].ToString();
                    lstAlmacenes.Add(e);

                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }

            return lstAlmacenes;
        }
    }
}
