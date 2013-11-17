using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.Models.Compras;

namespace MadeInHouse.DataObjects.Reportes
{
    class OrdenDeCompra
    {
        int idOrden;
        string codOrdenCompra;
        string proveedor;
        string fechaReg;
        string fechaSin;
        string medioPago;
        string estado;

        public int IdOrden { get { return idOrden; } set { idOrden = value; } }
        public string CodOrdenCompra { get { return codOrdenCompra; } set { codOrdenCompra = value; } }
        public string Proveedor { get { return proveedor; } set { proveedor = value; } }
        public string FechaReg { get { return fechaReg; } set { fechaReg = value; } }
        public string FechaSin { get { return fechaSin; } set { fechaSin = value; } }
        public string MedioPago { get { return medioPago; } set { medioPago = value; } }
        public string Estado { get { return estado; } set { estado = value; } }
    }

    class ReporteOrdenCompraSQL
    {
        public static List<Proveedor> BuscarProveedor()
        {
            List<Proveedor> lstProveedor = new List<Proveedor>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM Proveedor order by 1";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Proveedor e = new Proveedor();
                    e.IdProveedor = Convert.ToInt32(reader["idProveedor"]);
                    e.CodProveedor =  reader.IsDBNull(reader.GetOrdinal("codProveedor")) ? "sin codigo" : reader["codProveedor"].ToString();
                    e.RazonSocial = reader.IsDBNull(reader.GetOrdinal("razonSocial")) ? "sin razon" : reader["razonsocial"].ToString();
                    lstProveedor.Add(e);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }

            return lstProveedor;
        }

        public static List<OrdenDeCompra> BuscarOrdenesCompra()
        {
            List<OrdenDeCompra> lstOrdenCompra = new List<OrdenDeCompra>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM OrdenCompra order by 1";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    OrdenDeCompra e = new OrdenDeCompra();
                    e.IdOrden = Convert.ToInt32(reader["idOrden"]);
                    e.CodOrdenCompra = reader.IsDBNull(reader.GetOrdinal("codOrdenCompra")) ? "sin codigo" : reader["codOrdenCompra"].ToString();
                    e.Proveedor = reader["idProveedor"].ToString();
                    e.FechaReg = reader["fechaReg"].ToString();
                    e.FechaSin = reader["fechaSinAtencion"].ToString();
                    e.MedioPago = reader["medioPago"].ToString();
                    e.Estado = reader["estado"].ToString();
                    lstOrdenCompra.Add(e);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }

            return lstOrdenCompra;
        }

    }
}
