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
using MadeInHouse.Models.RRHH;

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

            cmd.CommandText = " INSERT INTO Usuario(codEmpleado,contrasenha,estado,idRol,fechaReg,fechaMod) VALUES (upper(@codEmpleado),@contrasenha,@estado,@idRol,getdate(),getdate()) ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            Trace.WriteLine("Flag1");
            Trace.WriteLine("<" + u.CodEmpleado + ">");
            Trace.WriteLine("<" + u.Contrasenha + ">");
            Trace.WriteLine("<" + u.Rol.IdRol + ">");
            Trace.WriteLine("<" + u.Estado + ">");

            cmd.Parameters.AddWithValue("@codEmpleado", u.CodEmpleado);
            cmd.Parameters.AddWithValue("@contrasenha", u.Contrasenha);
            cmd.Parameters.AddWithValue("@idRol", u.Rol.IdRol);
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

        public static int EditarUsuario(Usuario u)
        {
            DBConexion db = new DBConexion();
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;
            Trace.WriteLine("Editar Usuario 1");

            db.cmd.CommandText = "UPDATE Usuario SET contrasenha = @contrasenha, idRol = @idRol, estado = @estado, fechaMod = getdate() WHERE codEmpleado = @codEmpleado ";

            db.cmd.Parameters.AddWithValue("@codEmpleado", u.CodEmpleado);
            db.cmd.Parameters.AddWithValue("@contrasenha", u.Contrasenha);
            db.cmd.Parameters.AddWithValue("@idRol", u.Rol.IdRol);
            db.cmd.Parameters.AddWithValue("@estado", u.Estado);

            Trace.WriteLine("<" + u.IdUsuario + ">");
            Trace.WriteLine("<" + u.CodEmpleado + ">");
            Trace.WriteLine("<" + u.Contrasenha + ">");
            Trace.WriteLine("<" + u.Rol.IdRol + ">");
            Trace.WriteLine("<" + u.Estado + ">");

            Trace.WriteLine("Editar Usuario 2");
            try
            {
                db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                db.conn.Close();
                Trace.WriteLine("Editar Usuario 3");
            }

            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;
        }

        public static string buscarPass(string codEmpleado)
        {
            string passEnc = "";

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
            if (String.Compare(password, ContrasenhaDescifrada) == 0)

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
                    u.Rol = RolSQL.buscarRolPorId(Int32.Parse(reader["idRol"].ToString()));
                    u.Estado = Int32.Parse(reader["estado"].ToString());
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

        public static Usuario buscarUsuarioPorIdUsuario(int idUsuario)
        {
            Usuario u = null;

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM Usuario WHERE idUsuario=@idUsuario ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

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
                    u.Rol = RolSQL.buscarRolPorId(Int32.Parse(reader["idRol"].ToString()));
                    u.Estado = Int32.Parse(reader["estado"].ToString());
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
                    //u.IdRol = Int32.Parse(reader["idRol"].ToString());
                    Trace.WriteLine("<Rol flag1: ");
                    u.Rol = RolSQL.buscarRolPorId(Int32.Parse(reader["idRol"].ToString()));
                    u.FechaReg = DateTime.Parse(reader["fechaReg"].ToString());
                    u.FechaMod = DateTime.Parse(reader["fechaMod"].ToString());

                    u.Estado = Int32.Parse(reader["estado"].ToString());
                    Trace.WriteLine("<Flag: " + u.Rol.Nombre);
                    if (u.Estado == 1)
                    {
                        u.Estado = 0;
                        lstUsuario.Add(u);
                    }
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return lstUsuario;

        }

        public static int BuscarUsuarioPorCodigo(string codEmpleado)
        {
            int idUsuario = 0;
            int enc = 0;
            Usuario u = new Usuario();
            Empleado emp = new Empleado();

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            //cmd.CommandText = "SELECT idUsuario FROM Usuario WHERE upper(codEmpleado) like '%" +"@codEmpleado"+"%' ";
            cmd.CommandText = "SELECT * FROM Empleado WHERE upper(codEmpleado) = @codEmpleado";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@codEmpleado", codEmpleado);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    enc = 1;
                    idUsuario = (int)(reader["idEmpleado"]);
                    //MessageBox.Show("idEmpleado" + idUsuario);
                }
                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return enc;
        }

        public static int DisponibilidadUsuario(string codEmpleado)
        {
            int disp = 1;   //El usuario NO Existe; por tanto, está disponible
            Empleado emp = new Empleado();

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM Usuario WHERE upper(codEmpleado) = upper(@codEmpleado) ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@codEmpleado", codEmpleado);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    disp = 0;   //El usario ya existe
                    //idUsuario = (int)(reader["idEmpleado"]);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return disp;
        }

        public static int EliminarUsuarios(Usuario u)
        {
            DBConexion db = new DBConexion();
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;
            Trace.WriteLine("Editar Usuario 1");

            db.cmd.CommandText = "UPDATE Usuario SET estado = @estado, fechaMod = getdate() WHERE idUsuario = @idUsuario ";

            db.cmd.Parameters.AddWithValue("@idUsuario", u.IdUsuario);
            db.cmd.Parameters.AddWithValue("@estado", u.Estado);

            Trace.WriteLine("<" + u.IdUsuario + ">");
            Trace.WriteLine("<" + u.CodEmpleado + ">");
            Trace.WriteLine("<" + u.Rol.IdRol + ">");
            Trace.WriteLine("<" + u.Estado + ">");

            Trace.WriteLine("Editar Usuario 2");
            try
            {
                db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                db.conn.Close();
                Trace.WriteLine("Editar Usuario 3");
            }

            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;
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

    }
}
