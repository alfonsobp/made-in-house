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
        private DBConexion db;

        public UsuarioSQL(DBConexion db = null)
        {
            this.db = (db == null) ? new DBConexion() : db;
        }

        //AGREGAR
        public static int agregarUsuario(Usuario u)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = " INSERT INTO Usuario(codEmpleado,contrasenha,estado,idRol,fechaReg,fechaMod,estadoHabilitado,numIntentos,idTienda) VALUES (upper(@codEmpleado),@contrasenha,@estado,@idRol,getdate(),getdate(),@estadoHabilitado,@numIntentos,@idTienda) ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            Trace.WriteLine("<" + u.CodEmpleado + ">");
            Trace.WriteLine("<" + u.Contrasenha + ">");
            Trace.WriteLine("<" + u.Rol.IdRol + ">");
            Trace.WriteLine("<" + u.Estado + ">");

            cmd.Parameters.AddWithValue("@codEmpleado", u.CodEmpleado);
            cmd.Parameters.AddWithValue("@contrasenha", u.Contrasenha);
            cmd.Parameters.AddWithValue("@idRol", u.Rol.IdRol);
            cmd.Parameters.AddWithValue("@estado", u.Estado);
            cmd.Parameters.AddWithValue("@estadoHabilitado", u.EstadoHabilitado);
            cmd.Parameters.AddWithValue("@idTienda", u.IdTienda);
            cmd.Parameters.AddWithValue("@numIntentos", 0); //numero de intentos 0, por ser inicial

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

            db.cmd.CommandText = "UPDATE Usuario SET contrasenha = @contrasenha, idRol = @idRol, fechaMod = getdate(), estadoHabilitado = @estadoHabilitado, idTienda = @idTienda WHERE codEmpleado = @codEmpleado ";

            db.cmd.Parameters.AddWithValue("@codEmpleado", u.CodEmpleado);
            db.cmd.Parameters.AddWithValue("@contrasenha", u.Contrasenha);
            db.cmd.Parameters.AddWithValue("@idRol", u.Rol.IdRol);
            db.cmd.Parameters.AddWithValue("@estado", u.Estado);
            db.cmd.Parameters.AddWithValue("@estadoHabilitado", u.EstadoHabilitado);
            db.cmd.Parameters.AddWithValue("@idTienda", u.IdTienda);

            try
            {
                db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                db.conn.Close();
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

            //string ContrasenhaDescifrada = cifradoAES.descifrarTextoAES(buscarPass(codEmpleado), "MadeInHouse",
            //         "MadeInHouse", "MD5", 22, "1234567891234567", 128);

            //Trace.WriteLine("<<<<Descifrada" + ContrasenhaDescifrada + ">>>>");

            string contrasenhaCifrada = cifradoAES.cifrarTextoAES(password, "MadeInHouse",
                    "MadeInHouse", "MD5", 22, "1234567891234567", 128);

            if (String.Compare(contrasenhaCifrada, buscarPass(codEmpleado)) == 0)

                return 1;
            else return 0;

        }

        public static List<Usuario> BuscarUsuarioPorCodigo(string codEmpleado)
        {
            List<Usuario> lstUsuario = new List<Usuario>();

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT U.idUsuario idUsuario, E.codEmpleado codEmpleado, E.nombre + ' ' + E.apePaterno + ' ' + E.apeMaterno nomEmpleado, U.idTienda idTienda, T.nombre tienda, U.idRol idRol, U.fechaReg fechaReg, U.fechaMod fechaMod,  U.estadoHabilitado estadoHabilitado, U.Estado estado  FROM DESarrollo.Empleado E  JOIN Desarrollo.Usuario u  ON (E.codEmpleado=u.codEmpleado )  LEFT JOIN Desarrollo.Tienda T  ON (u.idTienda=T.idTienda) ORDER BY 7 DESC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@codEmpleado", codEmpleado);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Usuario u = new Usuario();
                    u.IdUsuario = Int32.Parse(reader["idUsuario"].ToString());
                    u.CodEmpleado = reader["codEmpleado"].ToString();
                    u.Nombre = reader["nomEmpleado"].ToString();
                    u.IdTienda = Convert.ToInt32(reader["idTienda"].ToString());
                    u.NomTienda = reader["tienda"].ToString();
                    u.Rol = RolSQL.buscarRolPorId(Int32.Parse(reader["idRol"].ToString()));
                    u.FechaReg = DateTime.Parse(reader["fechaReg"].ToString());
                    u.FechaMod = DateTime.Parse(reader["fechaMod"].ToString());
                    u.EstadoHabilitado = Convert.ToInt32(reader["estadoHabilitado"].ToString());
                    u.Estado = Int32.Parse(reader["estado"].ToString());
                    //MessageBox.Show("IDTIENDA: " + Convert.ToInt32(reader["idTienda"].ToString()));

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

        public Usuario buscarUsuarioPorCodEmpleado(string codEmpleado)
        {
            Usuario u = null;
            SqlDataReader reader;
            int posIdTienda;

            db.cmd.CommandText = "SELECT * FROM Usuario WHERE codEmpleado=@codEmpleado ";
            db.cmd.Parameters.AddWithValue("@codEmpleado", codEmpleado);

            try
            {
                if(db.cmd.Transaction == null) db.conn.Open();
                reader = db.cmd.ExecuteReader();

                if (reader.Read())
                {
                    u = new Usuario();
                    posIdTienda=reader.GetOrdinal("idTienda");
                    u.IdUsuario = Int32.Parse(reader["idUsuario"].ToString());
                    u.CodEmpleado = reader["codEmpleado"].ToString();
                    u.Contrasenha = reader["contrasenha"].ToString();
                    u.Rol = RolSQL.buscarRolPorId(Int32.Parse(reader["idRol"].ToString()));
                    u.Estado = Int32.Parse(reader["estado"].ToString());
                    u.FechaReg = DateTime.Parse(reader["fechaReg"].ToString());
                    u.FechaMod = DateTime.Parse(reader["fechaMod"].ToString());
                    u.EstadoHabilitado = Convert.ToInt32(reader["estadoHabilitado"].ToString());
                    u.IdTienda = reader.IsDBNull(posIdTienda) ? 0 : reader.GetInt32(posIdTienda);
                }

                if (db.cmd.Transaction == null) db.conn.Close();
                db.cmd.Parameters.Clear();
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
            int posIdTienda;
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
                    posIdTienda = reader.GetOrdinal("idTienda");
                    u.IdUsuario = Int32.Parse(reader["idUsuario"].ToString());
                    u.CodEmpleado = reader["codEmpleado"].ToString();
                    u.Contrasenha = reader["contrasenha"].ToString();
                    u.Rol = RolSQL.buscarRolPorId(Int32.Parse(reader["idRol"].ToString()));
                    u.Estado = Int32.Parse(reader["estado"].ToString());
                    u.FechaReg = DateTime.Parse(reader["fechaReg"].ToString());
                    u.FechaMod = DateTime.Parse(reader["fechaMod"].ToString());
                    u.EstadoHabilitado = Int32.Parse(reader["estadoHabilitado"].ToString());
                    u.IdTienda = reader.IsDBNull(posIdTienda) ? 0 : reader.GetInt32(posIdTienda);
                }
                    conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return u;
        }

        public static List<Usuario> BuscarUsuarioEliminado()
        {
            List<Usuario> lstUsuarioElim = new List<Usuario>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            int posIdTienda;

            cmd.CommandText = "SELECT * FROM Usuario WHERE estado=0";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Usuario u = new Usuario();

                    posIdTienda = reader.GetOrdinal("idTienda");
                    u.IdUsuario = Int32.Parse(reader["idUsuario"].ToString());
                    u.CodEmpleado = reader["codEmpleado"].ToString();

                    u.Rol = RolSQL.buscarRolPorId(Int32.Parse(reader["idRol"].ToString()));
                    u.FechaReg = DateTime.Parse(reader["fechaReg"].ToString());
                    u.FechaMod = DateTime.Parse(reader["fechaMod"].ToString());
                    u.EstadoHabilitado = Convert.ToInt32(reader["estadoHabilitado"].ToString());
                    u.Estado = Int32.Parse(reader["estado"].ToString());
                    u.IdTienda = reader.IsDBNull(posIdTienda) ? 0 : reader.GetInt32(posIdTienda);

                    lstUsuarioElim.Add(u);
                }

                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return lstUsuarioElim;

        }

        public static List<Usuario> BuscarUsuario()
        {

            List<Usuario> lstUsuario = new List<Usuario>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            int posIdTienda;

            //cmd.CommandText = "SELECT * FROM Usuario ";
            cmd.CommandText = "SELECT U.idUsuario idUsuario, E.codEmpleado codEmpleado, E.nombre + ' ' + E.apePaterno + ' ' + E.apeMaterno nomEmpleado, U.idTienda idTienda, T.nombre tienda, U.idRol idRol, U.fechaReg fechaReg, U.fechaMod fechaMod,  U.estadoHabilitado estadoHabilitado, U.Estado estado  FROM DESarrollo.Empleado E  JOIN Desarrollo.Usuario u  ON (E.codEmpleado=u.codEmpleado )  LEFT JOIN Desarrollo.Tienda T  ON (u.idTienda=T.idTienda) ORDER BY 7 DESC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Usuario u = new Usuario();
                    posIdTienda = reader.GetOrdinal("idTienda");
                    u.IdUsuario = Int32.Parse(reader["idUsuario"].ToString());
                    u.CodEmpleado = reader["codEmpleado"].ToString();
                    u.Nombre = reader["nomEmpleado"].ToString();
                    u.IdTienda = reader.IsDBNull(posIdTienda) ? 0 : reader.GetInt32(posIdTienda);
                    u.NomTienda = reader["tienda"].ToString();
                    u.Rol = RolSQL.buscarRolPorId(Int32.Parse(reader["idRol"].ToString()));
                    u.FechaReg = DateTime.Parse(reader["fechaReg"].ToString());
                    u.FechaMod = DateTime.Parse(reader["fechaMod"].ToString());
                    u.EstadoHabilitado = Convert.ToInt32(reader["estadoHabilitado"].ToString());
                    u.Estado = Int32.Parse(reader["estado"].ToString());
                    //MessageBox.Show("IDTIENDA: " + Convert.ToInt32(reader["idTienda"].ToString()));

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

        public static int existeEmpleado(string codEmpleado)
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

        public static int VerificarCodEmpleado(string codEmpleado)
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


        public static int GetIdUsuario(string codEmpleado)
        {
            int idUsuario = 0;
            Usuario u = new Usuario();

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            
            cmd.CommandText = "SELECT * FROM Usuario WHERE upper(codEmpleado) = @codEmpleado";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@codEmpleado", codEmpleado);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    idUsuario = (int)(reader["idUsuario"]);
                }
                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return idUsuario;
        }

        public static int DisponibilidadUsuario(string codEmpleado)
        {
            int disp = 1;   //El usuario NO Existe; por tanto, está disponible
            Empleado emp = new Empleado();

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT codEmpleado FROM USUARIO WHERE upper(codEmpleado) = upper(@codEmpleado) ";
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

        public static int ActualizarEstadoUsuarios(Usuario u)
        {
            DBConexion db = new DBConexion();
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            db.cmd.CommandText = "UPDATE Usuario SET estado = @estado, estadoHabilitado = @estadoHabilitado WHERE idUsuario = @idUsuario ";

            db.cmd.Parameters.AddWithValue("@idUsuario", u.IdUsuario);
            db.cmd.Parameters.AddWithValue("@estado", u.Estado);
            db.cmd.Parameters.AddWithValue("@estadoHabilitado", u.EstadoHabilitado);

            try
            {
                db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                db.conn.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }
            return k;
        }

        public static int RecuperarUsuarios(Usuario u)
        {
            DBConexion db = new DBConexion();
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            db.cmd.CommandText = "UPDATE Usuario SET estado = @estado WHERE idUsuario = @idUsuario ";

            db.cmd.Parameters.AddWithValue("@idUsuario", u.IdUsuario);
            db.cmd.Parameters.AddWithValue("@estado", u.Estado);

            try
            {
                db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                db.conn.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }
            return k;
        }

        public static int EliminarUsuarios(Usuario u)
        {
            DBConexion db = new DBConexion();
            //SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            db.cmd.CommandText = "UPDATE Usuario SET estado = @estado, fechaMod = getdate() WHERE idUsuario = @idUsuario ";

            db.cmd.Parameters.AddWithValue("@idUsuario", u.IdUsuario);
            db.cmd.Parameters.AddWithValue("@estado", u.Estado);

            try
            {
                db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                db.conn.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;
        }
            
        public static void DeshabilitarUsuario(Usuario u)
        {

            DBConexion db = new DBConexion();
            SqlCommand cmd = new SqlCommand();

            db.cmd.CommandText = "UPDATE Usuario SET estadoHabilitado = @estadoHabilitado WHERE idUsuario = @idUsuario ";

            db.cmd.Parameters.AddWithValue("@idUsuario", u.IdUsuario);
            db.cmd.Parameters.AddWithValue("@estadoHabilitado", 0);

            try
            {
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.conn.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

        }

        public static void HabilitarUsuario(Usuario u)
        {
            DBConexion db = new DBConexion();
            SqlCommand cmd = new SqlCommand();

            db.cmd.CommandText = "UPDATE Usuario SET estadoHabilitado = @estadoHabilitado WHERE idUsuario = @idUsuario ";

            db.cmd.Parameters.AddWithValue("@idUsuario", u.IdUsuario);
            db.cmd.Parameters.AddWithValue("@estadoHabilitado", 1);

            try
            {
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.conn.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

        }

        public static int ActualizarNumIntentos(Usuario u)
        {
            DBConexion db = new DBConexion();
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            db.cmd.CommandText = "UPDATE Usuario SET numIntentos = @numIntentos, estadoHabilitado = @estadoHabilitado WHERE idUsuario = @idUsuario ";

            db.cmd.Parameters.AddWithValue("@idUsuario", u.IdUsuario);
            db.cmd.Parameters.AddWithValue("@numIntentos", u.NumIntentos);
            db.cmd.Parameters.AddWithValue("@estadoHabilitado", u.EstadoHabilitado);

            try
            {
                db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                db.conn.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }
            return k;
        }

        public static int GetNumIntentos(Usuario u)
        {
            int numIntentos = 0;

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM Usuario WHERE idUsuario = @idUsuario";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@idUsuario", u.IdUsuario);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    numIntentos = (int)(reader["numIntentos"]);
                }
                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return numIntentos;
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