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

using MadeInHouse.Models.RRHH;

namespace MadeInHouse.DataObjects.Seguridad
{
    class RolSQL
    {

        public BindableCollection<Rol> ListarRol()
        {

            BindableCollection<Rol> lstRol = new BindableCollection<Rol>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM Rol ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Rol r = new Rol();
                    r.IdRol = Int32.Parse("" + reader["idRol"]);
                    r.NombRol = reader["nombre"].ToString();
                    r.Descripcion = reader["descripcion"].ToString();
                    r.Estado = Int32.Parse("" + reader["estado"]);

                    lstRol.Add(r);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return lstRol;

        }

        public static List<ModuloVentana> BuscarModuloVentana(string idAccModulo, string idAccVentana)
        {

            List<ModuloVentana> lstModuloVentana = new List<ModuloVentana>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM AccModulo";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ModuloVentana mv = new ModuloVentana();
                    mv.IdAccModulo = (int)reader["idAccModulo"];
                    mv.IdAccVentana = (int)reader["idAccVentana"];

                    lstModuloVentana.Add(mv);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return lstModuloVentana;

        }

        public static int buscarIdRol(Usuario u)
        {
            int idRolEnc = 0;

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT idRol FROM Usuario WHERE idUsuario=@idUsuario ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@idUsuario", u.IdUsuario);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                    idRolEnc = (int)(reader["idRol"]);
                else
                    MessageBox.Show("Usuario no válido, revisar datos");

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return idRolEnc;
        }

        //INSERTAR:
        public static int insertarRol(Rol r)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "INSERT INTO Rol(nombre,descripcion,estado) " +
            "VALUES (@nombre,@descripcion,@estado)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@nombre", r.NombRol);
            cmd.Parameters.AddWithValue("@descripcion", r.Descripcion);
            cmd.Parameters.AddWithValue("@estado", r.Estado);

            try
            {
                conn.Open();
                k = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }
            return k;
        }

        //ACTUALIZAR:
        public static int actualizarRol(Rol r)
        {

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "UPDATE Rol " +
            "SET nombre= @nombre, descripcion= @descripcion" +
            " WHERE idRol= @idRol ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@nombre", r.NombRol);
            cmd.Parameters.AddWithValue("@descripcion", r.Descripcion);
            cmd.Parameters.AddWithValue("@idRol", r.IdRol);

            try
            {
                conn.Open();
                k = cmd.ExecuteNonQuery();
                conn.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;
        }

        //ELIMINAR:
        public static int eliminarRol(Rol r)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "UPDATE Rol " +
            "SET estado= @estado" +
            " WHERE idRol= @idRol ";


            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@estado", 0);  //0: Eliminado Lógico
            cmd.Parameters.AddWithValue("@idRol", r.IdRol);

            try
            {
                conn.Open();
                k = cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;

        }


    }
}