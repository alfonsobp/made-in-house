using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using MadeInHouse.ViewModels.Seguridad;
using MadeInHouse.Models.Seguridad;

namespace MadeInHouse.DataObjects.Seguridad
{
    class UsuarioSQL
    {

        //AGREGAR
        public static int agregarUsuario(Usuario u)
        {

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "INSERT INTO Usuario(codEmpleado,contrasenha,estado,idRol,fechaReg,fechaMod) VALUES (@codEmpleado,@contrasenha,@estado,@idRol,getdate(),getdate())";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            Trace.WriteLine("Flag1");
            Trace.WriteLine("<" + u.CodEmpleado + ">");
            Trace.WriteLine("<" + u.Contrasenha + ">");
            Trace.WriteLine("<" + u.IdRol + ">");
            Trace.WriteLine("<" + u.Estado + ">");

            cmd.Parameters.AddWithValue("@codEmpleado", u.CodEmpleado);
            cmd.Parameters.AddWithValue("@contrasenha", u.Contrasenha);
            cmd.Parameters.AddWithValue("@idRol", u.IdRol);
            cmd.Parameters.AddWithValue("@estado", u.Estado);
            

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
        
        public static string buscarPass(string codEmpleado)
        {
            string passEnc="";

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT contrasenha FROM Usuario WHERE codEmpleado=@codEmpleado ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@codEmpleado", codEmpleado);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                    passEnc = (reader["contrasenha"]).ToString();
                else
                    //MessageBox.Show("No se encontro contrasenha");

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return passEnc;
        }

        public static int autenticarUsuario(string codEmpleado, string password)
        {
            CifrarAES cifradoAES = new CifrarAES();

            string ContrasenhaDescifrada = cifradoAES.descifrarTextoAES(buscarPass(codEmpleado), "MadeInHouse",
                     "MadeInHouse", "MD5", 22, "1234567891234567", 128);

            Trace.WriteLine("<<<<Descifrada" + ContrasenhaDescifrada + ">>>>");
            if (String.Compare(password, ContrasenhaDescifrada)==0)

                return 1;
            else return 0;

        }

        public static Usuario buscarUsuarioPorCodEmpleado(string codEmpleado)
        {
            Usuario u = null;

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM Usuario WHERE codEmpleado=@codEmpleado ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@codEmpleado", codEmpleado);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    u = new Usuario();

                    u.IdUsuario = Int32.Parse(reader["idUsuario"].ToString());
                    u.CodEmpleado = reader["codEmpleado"].ToString();
                    u.Contrasenha = reader["contrasenha"].ToString();
                    u.IdRol = Int32.Parse(reader["idRol"].ToString());
                    u.Estado = Int32.Parse(reader["idUsuario"].ToString());
                    u.FechaReg = DateTime.Parse(reader["fechaReg"].ToString());
                    u.FechaMod = DateTime.Parse(reader["fechaMod"].ToString());
                }
                else
                    conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return u;
        }

        public static int editarUsuario(Usuario u)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "UPDATE Usuario " +
                              "SET contrasenha= @contrasenha,idRol= @idRol, estado = @estado, fechaMod = @fechaMod " +
                              "WHERE idUsuario= @idUsuario";

            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@idUsuario", u.IdUsuario);
            cmd.Parameters.AddWithValue("@contrasenha", u.Contrasenha);
            cmd.Parameters.AddWithValue("@idRol", u.IdRol);
            cmd.Parameters.AddWithValue("@estado", u.Estado);
            cmd.Parameters.AddWithValue("@fechaMod", u.FechaMod);

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

        public static List<Usuario> BuscarUsuario(string codEmpleado, int idRol, DateTime fechaRegIni, DateTime fechaRegFin)
        {

            List<Usuario> lstUsuario = new List<Usuario>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM Usuario ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                Trace.WriteLine("<Rol flag0: ");
                while (reader.Read())
                {
                    Usuario u = new Usuario();
                    u.IdUsuario = Int32.Parse(reader["idUsuario"].ToString());
                    u.CodEmpleado = reader["codEmpleado"].ToString();
                    u.IdRol = Int32.Parse(reader["idRol"].ToString());
                    Trace.WriteLine("<Rol flag1: ");
                    u.Rol = RolSQL.buscarRolPorId(Int32.Parse(reader["idRol"].ToString()));
                    u.FechaReg = DateTime.Parse(reader["fechaReg"].ToString());
                    u.FechaMod = DateTime.Parse(reader["fechaMod"].ToString());

                    Trace.WriteLine("<Flag: " + u.Rol.Nombre);

                    lstUsuario.Add(u);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return lstUsuario;

        }

       

        //public static int editarServicio(Servicio s)
        //{
        //    SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
        //    SqlCommand cmd = new SqlCommand();
        //    int k = 0;

        //    cmd.CommandText = "UPDATE Servicio " +
        //                      "SET nombre= @nombre,descripcion= @descripcion " +
        //                      "WHERE codServicio= @codServicio ";

        //    cmd.CommandType = CommandType.Text;
        //    cmd.Connection = conn;

        //    cmd.Parameters.AddWithValue("@codServicio", s.Codigo);
        //    cmd.Parameters.AddWithValue("@nombre", s.Nombre);
        //    cmd.Parameters.AddWithValue("@descripcion", s.Descripcion);

        //    try
        //    {
        //        conn.Open();
        //        k = cmd.ExecuteNonQuery();
        //        conn.Close();

        //    }

        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.StackTrace.ToString());
        //    }

        //    return k;
        //}

        //public static int eliminarServicio(Servicio s)
        //{
        //    SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
        //    SqlCommand cmd = new SqlCommand();
        //    int k = 0;

        //    cmd.CommandText = "DELETE FROM Servicio WHERE codServicio = @codServicio";
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Connection = conn;

        //    cmd.Parameters.AddWithValue("@codServicio", s.Codigo);

        //    try
        //    {
        //        conn.Open();
        //        k = cmd.ExecuteNonQuery();
        //        conn.Close();

        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.StackTrace.ToString());
        //    }

        //    return k;
        //}

    }
}
