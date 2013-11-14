using MadeInHouse.Models;
using MadeInHouse.Models.Compras;
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
        public static List<OrdenCompra> GenerarReporOrdenCompra(string codOrCom, Proveedor prov, int estado, DateTime fechaIni, DateTime fechaFin)
        {

            List<OrdenCompra> lstServ = new List<OrdenCompra>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            String where = "";

            if (!String.IsNullOrEmpty(codOrCom))
            {
                where += " AND ";
                where = where + " o.codOrdenCompra = @tiendaNom ";
                cmd.Parameters.Add(new SqlParameter("tiendaNom", codOrCom));
            }

            if (prov!=null)
            {
                if (!String.IsNullOrEmpty(prov.CodProveedor))
                {
                    where += " AND ";
                    where = where + "  o.idProveedor=@nombreCli  ";
                    cmd.Parameters.Add(new SqlParameter("nombreCli", prov.IdProveedor));
                }
            }

            if (estado!=-1)
            {
                where += " AND ";
                where = where + "  o.estado=@estadoo  ";
                cmd.Parameters.Add(new SqlParameter("estadoo", estado));
            }


            cmd.CommandText = "Select o.codOrdenCompra as codOC, o.estado as estado, o.observaciones as observaciones, o.fechaReg as fecha, p.razonSocial as razonSocial from OrdenCompra o, Proveedor p where o.idProveedor=p.idProveedor " + where;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    OrdenCompra p = new OrdenCompra();
                    p.Estado = Int32.Parse(reader["estado"].ToString());
                    p.CodOrdenCompra = reader["codOC"].ToString();
                    p.FechaReg = reader["fecha"].ToString();
                    p.Observaciones = reader["observaciones"].ToString();
                    Proveedor pro = new Proveedor();
                    pro.RazonSocial = reader["razonSocial"].ToString();
                    p.Proveedor = pro;
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

        public static List<Serviciorepor> GenerarReporServicios(string tienda, string cliente, DateTime fechaIni, DateTime fechaFin, ref Double montoTotal )
        {

            List<Serviciorepor> lstServ = new List<Serviciorepor>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            String where = "";
            montoTotal = 0;

            if (!String.IsNullOrEmpty(tienda))
            {
                where += " AND ";
                where = where + " t.nombre = @tiendaNom ";
                cmd.Parameters.Add(new SqlParameter("tiendaNom", tienda));
            }

            if (!String.IsNullOrWhiteSpace(cliente))
            {
                where += " AND ";
                where = where + " ( (c.nombre + ' ' + c.apePaterno + ' ' + c.apeMaterno) like @nombreCli OR (c.apePaterno + ' ' + c.apeMaterno + ', ' + c.nombre) like @nombreCli OR  c.razonSocial like @nombreCli  ) ";
                cmd.Parameters.Add(new SqlParameter("nombreCli", '%' + cliente + '%'));   
            }


            cmd.CommandText = "Select v.fechaReg as fecha, (c.nombre + ' ' + c.apePaterno + ' ' +c.apeMaterno) as cliente, c.razonSocial as razonSocial, t.nombre as tienda, s.nombre as servicio, v.monto as precio from Venta v, Cliente c, Tienda t, Usuario u, Servicio s, DetalleVentaServicio d where c.idCLiente=v.idCLiente and u.idTienda=t.idTienda and v.idUsuario=u.idUsuario and d.idServicio=s.idServicio and v.idVenta=d.idVenta " + where;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    Serviciorepor p = new Serviciorepor();
                    p.Tienda = reader["tienda"].ToString();
                    p.Nombre = reader["servicio"].ToString();
                    p.Precio = Double.Parse(reader["precio"].ToString());
                    if (!String.IsNullOrEmpty(reader["razonSocial"].ToString()))
                    {
                        p.Cliente = reader["razonSocial"].ToString();
                    }
                    else
                    {
                        p.Cliente = reader["cliente"].ToString();
                    }
                    p.Fecha = Convert.ToDateTime(reader["fecha"].ToString());
                    montoTotal += p.Precio;
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
                where = where + " t.idAlmacen = @idAlmacen ";
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

            cmd.CommandText = " SELECT c.idTienda, c.nombre as nTienda, t.nombre as nAlmacen, a.stockActual as stock, p.nombre as nombrepro FROM Almacen t, Tienda c, ProductoxTienda a, Producto p where p.idProducto=a.idProducto and c.idTienda=t.idTienda and a.idTienda=t.idTienda " + where;
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
