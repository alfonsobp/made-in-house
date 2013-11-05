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
                    av.AccModulo = new AccModuloSQL().BuscarModuloPorId(Int32.Parse(reader["idAccModulo"].ToString()));

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

        public static List<AccVentana> ListarAccVentanaPorRol(List<AccVentana> lstAccVentana, int idRol)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM RolxAccVentana WHERE idRol=@idRol ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@idRol", idRol);

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    RolxAccVentana rav = new RolxAccVentana();
                    rav.IdRol = Int32.Parse(reader["idRol"].ToString());
                    rav.AccVentana = Int32.Parse(reader["idAccVentana"].ToString());//AccVentana es el idAccVentana

                    //Mejorar esta parte
                    for (int i = 0; i < lstAccVentana.Count; i++)
                    {
                        if (lstAccVentana[i].IdAccVentana == rav.AccVentana)
                        {
                            lstAccVentana[i].Estado = 1;
                        }
                    }
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return lstAccVentana;

        }

        public static void QuitarAccesosVentana(int idRol)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = " DELETE FROM RolxAccVentana WHERE idRol= @idRol ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@idRol", idRol);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }
        }

        public static void AgregarAcceso(int idRol, int idAccVentana)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = " INSERT INTO RolxAccVentana(idRol,idAccVentana) " +
            "VALUES (@idRol,@idAccVentana)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@idRol", idRol);
            cmd.Parameters.AddWithValue("@idAccVentana", idAccVentana);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

        }

        public static void AsignarAccesosVentana(List<AccVentana> lstAccVentana, int idRol)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();

            //Trace.WriteLine("Cantidad de Checks: " + lstAccVentana.Count);
            for (int i = 0; i < lstAccVentana.Count; i++)
            {
                if (lstAccVentana[i].Estado == 1)
                {
                    Trace.WriteLine("idAccVentana: " + lstAccVentana[i].IdAccVentana + ", estado: " + lstAccVentana[i].Estado);
                    AgregarAcceso(idRol, lstAccVentana[i].IdAccVentana);
                }
            }

        }

        public static int ActualizarRol(List<AccVentana> lstAccVentana, int idRol)
        {
            Trace.WriteLine("IDROL: " + idRol);
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmd2 = new SqlCommand();
            //SqlDataReader reader;

            cmd.CommandText = "DELETE FROM RolxAccVentana WHERE idRol=@idRol";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@idRol", idRol);

            try
            {
                conn.Open();
                //cmd.ExecuteNonQuery();

                //conn.Close();

                cmd2.CommandText = " INSERT INTO RolxAccVentana(idRol, idAccVentana) VALUES (@idRol, @idAccVentana) ";
                cmd2.CommandType = CommandType.Text;
                cmd2.Connection = conn;
                for (int i = 0; i < lstAccVentana.Count; i++)
                {
                    if (lstAccVentana[i].Estado == 1)
                    {
                        cmd2.Parameters.AddWithValue("@idRol", idRol);
                        cmd2.Parameters.AddWithValue("@idAccVentana", lstAccVentana[i].IdAccVentana);
                        Trace.WriteLine("---" + idRol + " " + lstAccVentana[i].IdAccVentana);
                        cmd2.ExecuteNonQuery();
                        Trace.WriteLine("IdAccesoVentana");
                    }
                }

                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return 1;

        }
    }
}
