using MadeInHouse.Models.RRHH;
using MadeInHouse.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.DataObjects
{
    class RrhhSQL
    {


        /******************************************AGREGAR EMPLEADO ************************************/
        public static int agregarEmpleado(Empleado p)
        {

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "insert into Empleado (DNI,sexo,nombre,apePaterno,apeMaterno,telefono,celular,email,estado,fechaReg,direccion,referencia,fechaNac,tienda,area,puesto,emailEmpresa,sueldo,cuentaBancaria,banco) " +
            "VALUES (@DNI,@sexo,@nombre,@apePaterno,@apeMaterno,@telefono,@celular,@email,@estado,@fechaReg,@direccion,@referencia,@fechaNac,@tienda,@area,@puesto,@emailEmpresa,@sueldo,@cuentaBancaria,@banco)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@DNI", p.Dni);
            cmd.Parameters.AddWithValue("@sexo", p.Sexo);
            cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            cmd.Parameters.AddWithValue("@apePaterno", p.ApePaterno);
            cmd.Parameters.AddWithValue("@apeMaterno", p.ApeMaterno);

            cmd.Parameters.AddWithValue("@telefono", p.Telefono);
            cmd.Parameters.AddWithValue("@celular", p.Celular);
            cmd.Parameters.AddWithValue("@email", p.EmailEmpleado);
            cmd.Parameters.AddWithValue("@emailEmpresa", p.EmailEmpresa);

            cmd.Parameters.AddWithValue("@fechaReg", p.FechaReg);
            cmd.Parameters.AddWithValue("@fechaNac", p.FechNacimiento);            
            cmd.Parameters.AddWithValue("@direccion", p.Direccion);
            cmd.Parameters.AddWithValue("@referencia", p.Referecia);

            cmd.Parameters.AddWithValue("@estado", p.Estado);
            cmd.Parameters.AddWithValue("@tienda", p.Tienda);
            cmd.Parameters.AddWithValue("@area", p.Area);
            cmd.Parameters.AddWithValue("@puesto", p.Puesto);
            
            cmd.Parameters.AddWithValue("@sueldo", p.Sueldo);
            cmd.Parameters.AddWithValue("@cuentaBancaria", p.CuentaBancaria);
            cmd.Parameters.AddWithValue("@banco", p.Banco);
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
        /****************************************** EMPLEADO ************************************/
        public static List<Empleado> BuscarEmpleado()
        {
            List<Empleado> lstEmpleado = new List<Empleado>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM Empleado ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Empleado e = new Empleado();
                    e.Dni = reader["DNI"].ToString();
                    e.Sexo = reader["sexo"].ToString();
                    e.ApePaterno = reader["apePaterno"].ToString();
                    e.Nombre = reader["nombre"].ToString();
                    e.ApeMaterno = reader["apeMaterno"].ToString();

                    e.Telefono = reader["telefono"].ToString();
                    e.Celular = reader["celular"].ToString();
                    e.EmailEmpleado = reader["email"].ToString();
                    e.EmailEmpresa = reader["emailEmpresa"].ToString();

                    e.FechaReg = reader["fechaReg"].ToString();
                    e.FechNacimiento = reader["fechaNac"].ToString();
                    e.Direccion = reader["direccion"].ToString();
                    e.Referecia = reader["referencia"].ToString();

                    e.Estado = Convert.ToInt32(reader["estado"].ToString());
                    e.Tienda = reader["tienda"].ToString();
                    e.Area = reader["area"].ToString();
                    e.Puesto = reader["puesto"].ToString();

                    e.Sueldo = Convert.ToDecimal(reader["sueldo"].ToString());
                    e.CuentaBancaria = reader["cuentaBancaria"].ToString();
                    e.Banco = reader["banco"].ToString();

                    e.IdEmpleado = reader["idEmpleado"].ToString();
                   

                    lstEmpleado.Add(e);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }

            return lstEmpleado;
        }

        /****************************************** ROL ************************************/

        public static int buscarIdRol(Usuario u)
        {
            int idRolEnc= 0;

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
                    idRolEnc= (int)(reader["idRol"]);
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








        //BUSCAR:
        public static List<Rol> BuscarProveedor(string modulo)
        {
            List<Rol> lstRol = new List<Rol>();
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

                    r.NombRol = reader["nombre"].ToString();
                    r.Descripcion = reader["descripcion"].ToString();
                    r.Estado = Convert.ToInt32(reader["estado"].ToString());
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

        public static int insertarRolxAccModulo(int idRol, int idModulo)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "INSERT INTO RolxAccModulo(idAccModulo,idRol) " +
            "VALUES (@idAccModulo,@idRol)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@idRol", idRol);
            cmd.Parameters.AddWithValue("@idAccModulo", idModulo);

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

        //MAXIMO ID ROL

        public static int ObtenerMaximoID (string nombreTabla, string nombreID) 
        {
            int id=0;

            SqlDataReader reader;

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "SELECT MAX("+nombreID+") FROM "+nombreTabla;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                //k = cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                if(reader.Read())
                    id=reader.GetInt32(0);
                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }
            return id;
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

        /****************************************** MODULO ************************************/
        //BUSCAR:
        public static List<Modulo> BuscarModulo(string modulo)
        {
            List<Modulo> lstModulo = new List<Modulo>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM ModuloSistema ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Modulo m = new Modulo();

                    //m.NombreModulo = reader["nombre"].ToString();
                    m.Descripcion = reader["descripcion"].ToString();
                    //DESPUÉS DE AGREGAR EL CAMPO "estado" A LA TABLA ModuloSistema, DESCOMENTAR LA LÍNEA DE ABAJO
                    //m.Estado = Convert.ToInt32(reader["estado"].ToString());
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

        //INSERTAR:
        public static int insertarModulo(Modulo m)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            //cmd.CommandText = "INSERT INTO ModuloSistema(nombre,descripcion,estado)" +
            cmd.CommandText = "INSERT INTO ModuloSistema(nombre,descripcion)" +
            //"VALUES (@nombre,@descripcion,@estado)";
            "VALUES (@nombre,@descripcion)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;


            //cmd.Parameters.AddWithValue("@nombre", m.NombreModulo);
            cmd.Parameters.AddWithValue("@descripcion", m.Descripcion);
            //cmd.Parameters.AddWithValue("@estado", m.Estado);

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
        public static int actualizarModulo(Modulo m)
        {

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "UPDATE ModuloSistema" +
            "SET nombre= @nombre, descripcion= @descripcion" +
            " WHERE idModulo= @idModulo";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            //cmd.Parameters.AddWithValue("@nombre", m.NombreModulo);
            cmd.Parameters.AddWithValue("@descripcion", m.Descripcion);
            cmd.Parameters.AddWithValue("@idModulo", m.IdModulo);

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
        public static int eliminarModulo(Modulo m)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "UPDATE ModuloSistema " +
            "SET estado= @estado" +
            " WHERE idModulo= @idModulo ";


            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            m.Estado = 0;
            cmd.Parameters.AddWithValue("@estado", m.Estado);  //0: Eliminado Lógico
            cmd.Parameters.AddWithValue("@idModulo", m.IdModulo);

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
