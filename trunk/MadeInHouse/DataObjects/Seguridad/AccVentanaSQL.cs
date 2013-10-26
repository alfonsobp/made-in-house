using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using Caliburn.Micro;
using MadeInHouse.Models.Seguridad;
using MadeInHouse.DataObjects.Seguridad;

namespace MadeInHouse.DataObjects.Seguridad
{
    class AccVentanaSQL
    {
        public static List<AccVentana> ListarAccVentana()
        {

            List<AccVentana> lstAccVentana = new List<AccVentana>();

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM AccVentana ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    AccVentana av = new AccVentana();
                    av.IdAccVentana = Int32.Parse("" + reader["idAccVentana"]);
                    av.Nombre = reader["nombre"].ToString();
                    av.Estado = Int32.Parse("" + reader["estado"]);
                    av.AccModulo= new AccModuloSQL().BuscarModuloPorId(Int32.Parse(reader["idAccModulo"].ToString()));
 
                    lstAccVentana.Add(av);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return lstAccVentana;

        }

    }
}
