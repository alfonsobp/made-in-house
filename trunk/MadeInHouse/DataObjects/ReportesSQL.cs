using MadeInHouse.Models;
using MadeInHouse.Models.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.DataObjects
{
    class ReportesSQL
    {
        public static List<Serviciorepor> GenerarReporServicios(string codigo, string razonSocial, string Ruc, string fechaIni, string fechaFin)
        {

            List<Serviciorepor> lstServ = new List<Serviciorepor>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT Producto.tipo Uso as Categoria, Servicio.descripcion as Nombre, ProveedorxProducto.IdProveedor as Proveedor"+
                               "FROM Proveedor, Producto, Servicio, DetalleVenta";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    Serviciorepor p = new Serviciorepor();
                    p.Categoria = reader["Categoria"].ToString();
                    p.Nombre = reader["Nombre"].ToString();
                    p.Precio = reader["contacto"].ToString();
                    p.Proveedor = reader["Proveedor"].ToString();
                    p.Fecha = Convert.ToDateTime(reader["fax"].ToString());
                    lstServ.Add(p);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }


            return lstServ;

        }

        public static List<Ventarepor> GenerarReporVentas(string codigo, string razonSocial, string Ruc, string fechaIni, string fechaFin)
        {

            List<Ventarepor> lstVenta = new List<Ventarepor>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT Producto.tipo Uso as Categoria, Servicio.descripcion as Nombre, ProveedorxProducto.IdProveedor as Proveedor" +
                               "FROM Proveedor, Producto, Servicio, DetalleVenta";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    Ventarepor p = new Ventarepor();
                    p.Codigo = reader["Categoria"].ToString();
                    p.Fecha = Convert.ToDateTime(reader["Nombre"].ToString());
                    p.Proveedor = reader["contacto"].ToString();
                    p.Producto = reader["Proveedor"].ToString();
                    p.MontoTotal = reader["fax"].ToString();
                    lstVenta.Add(p);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }


            return lstVenta;

        }

        public static List<StockRepor> GenerarReporStock(int idAlmacen, int idTienda, DateTime fechaini, DateTime fechafin)
        {

            List<StockRepor> lstVenta = new List<StockRepor>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            string where = "";

            if (idAlmacen!=0)
            {
                where += " AND ";
                where = where + " a.idAlmacen = @idAlmacen ";
                cmd.Parameters.Add(new SqlParameter("idAlmacen", idAlmacen));
            }

            if (idTienda!=-1)
            {
                where += " AND ";
                where = where + " c.idTienda = @idTienda ";
                cmd.Parameters.Add(new SqlParameter("idTienda", idTienda));
            }

            /*if (!String.IsNullOrEmpty(fechaini.ToString()))
            {
                where += " AND ";
                where = where + " convert (char, t.fechaReg, 103) >= @registroDesde ";
                cmd.Parameters.Add(new SqlParameter("registroDesde", fechaini.ToString()));
            }

            if (!String.IsNullOrEmpty(fechafin.ToString()))
            {
                where += " AND ";
                where = where + " convert (char, t.fechaReg, 103) <= @registroHasta ";
                cmd.Parameters.Add(new SqlParameter("registroHasta", fechafin.ToString()));
            }*/

            cmd.CommandText = " SELECT c.nombre as nTienda, t.nombre as nAlmacen, a.stockActual as stock, p.nombre as nombrepro FROM Almacen t, Tienda c, AlmacenxProducto a, Producto p where p.idProducto=a.idProducto and c.idTienda=t.idTienda and a.idAlmacen=t.idAlmacen and a.idTienda=c.idTienda " + where;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    StockRepor p = new StockRepor();
                    p.Almacen = reader["nAlmacen"].ToString();
                    p.Producto = reader["nombrepro"].ToString();
                    p.Stock = Int32.Parse(reader["stock"].ToString());
                    p.Tienda = reader["nTienda"].ToString();
                    lstVenta.Add(p);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }


            return lstVenta;

        }
    }
}
