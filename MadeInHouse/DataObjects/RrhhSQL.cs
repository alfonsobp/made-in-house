using MadeInHouse.Models.RRHH;
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
        /****************************************** EMPLEADO ************************************/
        public static List<Empleado> BuscarEmpleado(string codigo, string dni, string nombre, string apePaterno, string tienda, string area, string puesto)
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
                    e.Ruc = reader["RUC"].ToString();
                    e.CuentaBancaria = reader["cuentaBancaria"].ToString();
                    e.Estado = Convert.ToInt32(reader["estado"].ToString());
                    e.FechaReg = reader["fecharReg"].ToString();
                    e.IdPuesto = reader["idPuesto"].ToString();
                    e.IdCategoria = reader["idCategoria"].ToString();
                    e.IdEmpleado = reader["idEmpleado"].ToString();
                    e.SemVacacion = Convert.ToInt32(reader["semVacacion"].ToString());
                    e.RefFoto = reader["foto"].ToString();

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

            cmd.CommandText = "INSERT INTO Rol(nombre,descripcion,estado)" +
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

                    m.NombreModulo = reader["nombre"].ToString();
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


            cmd.Parameters.AddWithValue("@nombre", m.NombreModulo);
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

            cmd.Parameters.AddWithValue("@nombre", m.NombreModulo);
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
