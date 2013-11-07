using MadeInHouse.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace MadeInHouse.DataObjects.Ventas
{
    class ModoPagoSQL
    {
        public BindableCollection<ModoPago> BuscarModosPago()
        {
            BindableCollection<ModoPago> listaModoPagos = new BindableCollection<ModoPago>(); ;
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM ModoPago";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ModoPago mp = new ModoPago();
                    mp.IdModoPago = reader.IsDBNull(reader.GetOrdinal("idModoPago")) ? -1 : (int)reader["idModoPago"];
                    mp.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();
                    listaModoPagos.Add(mp);
                }

                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return listaModoPagos;
        }
    }
}
