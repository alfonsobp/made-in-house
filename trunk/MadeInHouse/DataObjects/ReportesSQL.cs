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
    }
}
