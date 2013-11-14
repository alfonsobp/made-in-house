using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Ventas;

namespace MadeInHouse.DataObjects.Reportes
{
    class ReporteVentasSQL
    {
        public static List<Tienda> BuscarTienda()
        {
            List<Tienda> lstTienda = new List<Tienda>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT nombre FROM Tienda order by 1";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Tienda e = new Tienda();
                    e.Nombre = reader["nombre"].ToString();
                    lstTienda.Add(e);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }

            return lstTienda;
        }

        public static List<Cliente> BuscarCliente()
        {
            List<Cliente> lstCliente = new List<Cliente>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT nombre FROM Cliente order by 1";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cliente e = new Cliente();
                    e.Nombre = reader["nombre"].ToString();
                    lstCliente.Add(e);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }

            return lstCliente;
        }

        public static List<Producto> BuscarProducto()
        {
            List<Producto> lstProducto = new List<Producto>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT nombre FROM Producto order by 1";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Producto e = new Producto();
                    e.Nombre = reader["nombre"].ToString();
                    lstProducto.Add(e);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }

            return lstProducto;
        }

        public static List<Venta> BuscarVentaxTienda(string tienda)
        {
            List<Venta> lstVenta = new List<Venta>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM venta v join usuario u on (v.idUsuario = u.idUsuario) join tienda t on u.idTienda = t.idTienda where t.nombre = '" + tienda + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Venta e = new Venta();
                    e.IdVenta = Convert.ToInt32(reader["idVenta"]);
                    e.TipoDocPago = reader["tipoDocPago"].ToString();
                    e.NumDocPago = reader["numDocPagoProducto"].ToString();
                    e.IdUsuario = Convert.ToInt32(reader["idUsuario"]);
                    e.IdCliente = reader.IsDBNull(reader.GetOrdinal("idCliente")) ? 0 : Convert.ToInt32(reader["idCliente"]);
                    e.Monto = Convert.ToDouble(reader["monto"].ToString());
                    e.PtosGanados = Convert.ToInt32(reader["ptosGanados"]);
                    e.Igv = Convert.ToDouble(reader["IGV"]);
                    e.CodTarjeta = reader.IsDBNull(reader.GetOrdinal("codTarjeta")) ? 0 : Convert.ToInt32(reader["codTarjeta"]);
                    e.TipoVenta = reader["tipoVenta"].ToString();
                    e.Estado = Convert.ToInt32(reader["estado"]);
                    lstVenta.Add(e);
                    
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }

            return lstVenta;
        }
        
        public static List<Venta> BuscarVentaxCliente(string Cliente)
        {
            List<Venta> lstVenta = new List<Venta>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM venta v join Cliente c on (v.idCliente = c.idCliente) where c.nombre  = '" + Cliente + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Venta e = new Venta();
                    e.IdVenta = Convert.ToInt32(reader["idVenta"]);
                    e.TipoDocPago = reader["tipoDocPago"].ToString();
                    e.NumDocPago = reader["numDocPagoProducto"].ToString();
                    e.IdUsuario = Convert.ToInt32(reader["idUsuario"]);
                    e.IdCliente = Convert.ToInt32(reader["idCliente"]);
                    e.Monto = Convert.ToDouble(reader["monto"].ToString());
                    e.PtosGanados = Convert.ToInt32(reader["ptosGanados"]);
                    e.Igv = Convert.ToDouble(reader["IGV"]);
                    e.CodTarjeta = reader.IsDBNull(reader.GetOrdinal("codTarjeta")) ? 0 : Convert.ToInt32(reader["codTarjeta"]);
                    e.TipoVenta = reader["tipoVenta"].ToString();
                    e.Estado = Convert.ToInt32(reader["estado"]);
                    lstVenta.Add(e);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }

            return lstVenta;
        }

        public static List<Venta> BuscarVentaxProducto(string producto)
        {
            List<Venta> lstVenta = new List<Venta>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM venta v join DetalleVenta d on (v.idVenta = d.idVenta) join Producto p on d.idProducto = p.idProducto where p.nombre ='" + producto + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Venta e = new Venta();
                    e.IdVenta = Convert.ToInt32(reader["idVenta"]);
                    e.TipoDocPago = reader["tipoDocPago"].ToString();
                    e.NumDocPago = reader["numDocPagoProducto"].ToString();
                    e.IdUsuario = Convert.ToInt32(reader["idUsuario"]);
                    e.IdCliente = reader.IsDBNull(reader.GetOrdinal("idCliente")) ? 0 : Convert.ToInt32(reader["idCliente"]);
                    e.Monto = Convert.ToDouble(reader["monto"].ToString());
                    e.PtosGanados = Convert.ToInt32(reader["ptosGanados"]);
                    e.Igv = Convert.ToDouble(reader["IGV"]);
                    e.CodTarjeta = reader.IsDBNull(reader.GetOrdinal("codTarjeta")) ? 0 : Convert.ToInt32(reader["codTarjeta"]);
                    e.TipoVenta = reader["tipoVenta"].ToString();
                    e.Estado = Convert.ToInt32(reader["estado"]);
                    lstVenta.Add(e);

                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }

            return lstVenta;
        }
    }

}
