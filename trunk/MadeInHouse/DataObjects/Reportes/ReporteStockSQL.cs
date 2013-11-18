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
    public class stock
    {
        int idproducto;
        string producto;
        string tienda;
        int stockactual;
        int stockmin;
        int stockmax;

        public int IdProducto { get { return idproducto; } set { idproducto = value; } }
        public string Tienda { get { return tienda; } set { tienda = value; } }
        public string Producto { get { return producto; } set { producto = value; } }
        public int Stockmin { get { return stockmin; } set { stockmin = value; } }
        public int Stockmax { get { return stockmax; } set { stockmax = value; } }
        public int Stockactual { get { return stockactual; } set { stockactual = value; } }
    }

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

        public static List<stock> BuscarStock(string tienda)
        {
            List<stock> lstStock = new List<stock>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "select p.idproducto, p.nombre 'producto', t.nombre 'tienda', pt.stockActual,pt.stockmax,pt.stockmin from producto p join productoxtienda pt on (p.idproducto = pt.idproducto) join tienda t on (t.idTienda = pt.idTienda) where t.nombre ='" + tienda + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    stock e = new stock();
                    e.IdProducto = Convert.ToInt32(reader["idproducto"]);
                    e.Producto = reader["producto"].ToString();
                    e.Tienda = reader["tienda"].ToString();
                    e.Stockactual = Convert.ToInt32(reader["stockActual"]);
                    e.Stockmax = Convert.ToInt32(reader["stockmax"]);
                    e.Stockmin = Convert.ToInt32(reader["stockmin"]);
                    lstStock.Add(e);

                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }

            return lstStock;
        }

        public static List<stock> BuscarStockCentral()
        {
            List<stock> lstStock = new List<stock>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "select * from producto";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    stock e = new stock();
                    e.IdProducto = Convert.ToInt32(reader["idproducto"]);
                    e.Producto = reader["nombre"].ToString();
                    e.Tienda = "ALMACEN CENTRAL";
                    e.Stockactual = reader.IsDBNull(reader.GetOrdinal("stockActual")) ? 0 : Convert.ToInt32(reader["stockActual"]);
                    e.Stockmax = reader.IsDBNull(reader.GetOrdinal("stockmax")) ? 0 : Convert.ToInt32(reader["stockmax"]);
                    e.Stockmin = reader.IsDBNull(reader.GetOrdinal("stockmin")) ? 0 : Convert.ToInt32(reader["stockmin"]);
                    lstStock.Add(e);

                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }

            return lstStock;
        }
    }
}
