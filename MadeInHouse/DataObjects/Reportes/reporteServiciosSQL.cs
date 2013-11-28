using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MadeInHouse.Models.Compras;
using MadeInHouse.Models.Ventas;

namespace MadeInHouse.DataObjects.Reportes
{
    public class Servi
    {
        string doc;
        string tienda;
        string cliente;
        string servicio;
        string fecha;
        public string Doc { get { return doc; } set { doc = value; } }
        public string Tienda { get { return tienda; } set { tienda = value; } }
        public string Cliente { get { return cliente; } set { cliente = value; } }
        public string Servicio { get { return servicio; } set { servicio = value; } }
        public string Fecha { get { return fecha; } set { fecha = value; } }
    }

    class reporteServiciosSQL
    {
        public static List<Servicio> BuscarServicio()
        {
            List<Servicio> lstServicio = new List<Servicio>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM Servicio order by 1";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Servicio e = new Servicio();
                    e.Nombre = reader["nombre"].ToString();
                    e.IdProveedor = Convert.ToInt32(reader["idProveedor"]);
                    e.IdServicio = Convert.ToInt32(reader["idServicio"]);
                    e.CodServicio = reader["codServicio"].ToString();
                    lstServicio.Add(e);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }

            return lstServicio;
        }

        public static List<Servi> BuscarServi(int tienda, int cliente, int servicio)
        {
            List<Servi> lstServicio = new List<Servi>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "select v.numdocpagoservicio, t.nombre tienda, c.nombre cliente, s.nombre servicio, v.fechareg fecha from venta v join DetalleVentaServicio d on (v.idVenta = d.idVenta)  join servicio s on (d.idServicio = s.idServicio) join usuario u on ( v.idUsuario = u.idusuario) join tienda t on (u.idTienda = t.idtienda) , cliente c where t.idtienda =" + tienda + "and c.idcliente = " + cliente + "and s.idservicio= " + servicio;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Servi e = new Servi();

                    e.Doc = reader["numdocpagoservicio"].ToString();                    
                    e.Servicio = reader["Servicio"].ToString();
                    e.Tienda = reader["tienda"].ToString();
                    e.Cliente = reader["cliente"].ToString();
                    e.Fecha = reader["fecha"].ToString();
                    lstServicio.Add(e);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }

            return lstServicio;
        }

        public static List<Servi> BuscarServiTodos(int tienda, int cliente)
        {
            List<Servi> lstServicio = new List<Servi>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "select v.idventa, t.nombre tienda, c.nombre cliente, s.nombre servicio, v.fechareg fecha from venta v join DetalleVentaServicio d on (v.idVenta = d.idVenta)  join servicio s on (d.idServicio = s.idServicio) join usuario u on ( v.idUsuario = u.idusuario) join tienda t on (u.idTienda = t.idtienda) , cliente c where t.idtienda =" + tienda + "and c.idcliente = " + cliente;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Servi e = new Servi();

                    e.IdVenta = Convert.ToInt32(reader["idventa"]);
                    e.Servicio = reader["Servicio"].ToString();
                    e.Tienda = reader["tienda"].ToString();
                    e.Cliente = reader["cliente"].ToString();
                    e.Fecha = reader["fecha"].ToString();
                    lstServicio.Add(e);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }

            return lstServicio;
        }


    }
}
