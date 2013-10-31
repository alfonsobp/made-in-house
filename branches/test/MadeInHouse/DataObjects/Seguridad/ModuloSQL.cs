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

namespace MadeInHouse.DataObjects.Seguridad
{
    class ModuloSQL
    {
        public  BindableCollection<Modulo> ListarModulo()
        {

            BindableCollection<Modulo> lstModulo = new BindableCollection<Modulo>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM AccModulo ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Modulo m = new Modulo();
                    m.IdModulo = Int32.Parse("" + reader["idAccModulo"]);
                    m.Descripcion = reader["descripcion"].ToString();
                    m.Estado = Int32.Parse("" + reader["estado"]);

                    Trace.Write("MODULO");
                    Trace.Write("<<" + m.IdModulo + ">>");
                    Trace.Write("<<" + m.Descripcion + ">>");
                    Trace.Write("<<" + m.Estado + ">>");

                    lstModulo.Add(m);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return lstModulo;

        }

    }
    
}
