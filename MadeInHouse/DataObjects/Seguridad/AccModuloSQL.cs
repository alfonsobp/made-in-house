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
    class AccModuloSQL
    {
        public BindableCollection<AccModulo> ListarAccModulo()
        {

            BindableCollection<AccModulo> lstModulo = new BindableCollection<AccModulo>();
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
                    AccModulo m = new AccModulo();
                    m.IdAccModulo = Int32.Parse("" + reader["idAccModulo"]);
                    m.Nombre = reader["nombre"].ToString();
                    m.Estado = Int32.Parse("" + reader["estado"]);

                    Trace.Write("MODULO");
                    Trace.Write("<<" + m.IdAccModulo + ">>");
                    Trace.Write("<<" + m.Nombre + ">>");
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

        public AccModulo BuscarModuloPorId(int idAccModulo)
        {

            AccModulo am = null;

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM AccModulo WHERE idAccModulo=@idAccModulo";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@idAccModulo", idAccModulo);

            Trace.WriteLine("<Flag: ");

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    am = new AccModulo();
                    am.IdAccModulo = Int32.Parse(reader["idAccModulo"].ToString());
                    am.Nombre = reader["nombre"].ToString();
                    am.Estado = Int32.Parse(reader["estado"].ToString());
                }
                else
                {
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return am;

        }


    }
}
