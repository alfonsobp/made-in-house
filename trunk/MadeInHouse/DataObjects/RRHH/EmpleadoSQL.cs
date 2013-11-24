using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

using MadeInHouse.Models.RRHH;


namespace MadeInHouse.DataObjects.RRHH
{
    class EmpleadoSQL
    {
        /******************************************AGREGAR EMPLEADO ************************************/
        public static int agregarEmpleado(Empleado p)
        {

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            if (p.IdTienda != 0)
            {
                cmd.CommandText = "insert into Empleado (DNI,sexo,nombre,apePaterno,apeMaterno,telefono,celular,email,estado,fechaReg,direccion,referencia,fechaNac,tienda,area,puesto,emailEmpresa,sueldo,cuentaBancaria,banco,codEmpleado,idTienda) " +
                    "VALUES (@DNI,@sexo,@nombre,@apePaterno,@apeMaterno,@telefono,@celular,@email,@estado,@fechaReg,@direccion,@referencia,@fechaNac,@tienda,@area,@puesto,@emailEmpresa,@sueldo,@cuentaBancaria,@banco,@codEmpleado,@idTienda)";
            }
            else
            {//ALMACEN CENTRAL
                cmd.CommandText = "insert into Empleado (DNI,sexo,nombre,apePaterno,apeMaterno,telefono,celular,email,estado,fechaReg,direccion,referencia,fechaNac,tienda,area,puesto,emailEmpresa,sueldo,cuentaBancaria,banco,codEmpleado) " +
                    "VALUES (@DNI,@sexo,@nombre,@apePaterno,@apeMaterno,@telefono,@celular,@email,@estado,@fechaReg,@direccion,@referencia,@fechaNac,@tienda,@area,@puesto,@emailEmpresa,@sueldo,@cuentaBancaria,@banco,@codEmpleado)";
            }
             cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@codEmpleado", p.CodEmpleado);
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

            if (p.IdTienda != 0)
                cmd.Parameters.AddWithValue("@idTienda", p.IdTienda);

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
        public static List<Empleado> BuscarEmpleado(string nombre, string paterno, string dni, string area, string puesto, string tienda)
        {
            List<Empleado> lstEmpleado = new List<Empleado>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            int idTiendaNull;

            cmd.CommandText = "SELECT * FROM Empleado where nombre like '" + nombre + "%' and apePaterno like '" + paterno + "%' and DNI like '" + dni + "%' and tienda like '" + tienda + "%' and area like '" + area + "%' and puesto like '" + puesto + "%' and estado = 1";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {

                if (cmd.Transaction == null) conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Empleado e = new Empleado();
                    idTiendaNull = reader.GetOrdinal("idTienda");
                    e.CodEmpleado = reader["codEmpleado"].ToString();
                    e.Dni = reader["DNI"].ToString();
                    e.Sexo = reader["sexo"].ToString();
                    e.ApePaterno = reader["apePaterno"].ToString();
                    e.Nombre = reader["nombre"].ToString();
                    e.ApeMaterno = reader["apeMaterno"].ToString();

                    e.Telefono = reader["telefono"].ToString();
                    e.Celular = reader["celular"].ToString();
                    e.EmailEmpleado = reader["email"].ToString();
                    e.EmailEmpresa = reader["emailEmpresa"].ToString();

                    e.FechaReg = DateTime.Parse(reader["fechaReg"].ToString()) ;
                    e.FechNacimiento = DateTime.Parse(reader["fechaNac"].ToString());
                    e.Direccion = reader["direccion"].ToString();
                    e.Referecia = reader["referencia"].ToString();

                    e.Estado = Convert.ToInt32(reader["estado"].ToString());
                    e.Tienda = reader["tienda"].ToString();

                    e.IdTienda = reader.IsDBNull(idTiendaNull) ? 0 : reader.GetInt32(idTiendaNull);

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

        public static Empleado DatosBasicosEmpleado(string codEmpleado)
        {
            Empleado emp = new Empleado();

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = " SELECT idEmpleado, codEmpleado, nombre, apePaterno, apeMaterno, emailEmpresa FROM Empleado WHERE codEmpleado=@codEmpleado ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@codEmpleado", codEmpleado);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    emp.IdEmpleado = reader.IsDBNull(reader.GetOrdinal("idEmpleado")) ?  "0" : reader["idEmpleado"].ToString();
                    emp.CodEmpleado = reader.IsDBNull(reader.GetOrdinal("codEmpleado")) ? "" : reader["codEmpleado"].ToString();
                    emp.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? "" : reader["nombre"].ToString();
                    emp.ApePaterno = reader.IsDBNull(reader.GetOrdinal("apePaterno")) ?  "" : reader["apePaterno"].ToString();
                    emp.ApeMaterno = reader.IsDBNull(reader.GetOrdinal("apeMaterno")) ? "" : reader["apeMaterno"].ToString();
                    emp.EmailEmpresa = reader.IsDBNull(reader.GetOrdinal("emailEmpresa"))? "": reader["emailEmpresa"].ToString();
                }
                else
                    conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }
            return emp;
        }

        public static Empleado BuscarEmpleadoPorCodigo(string codEmpleado)
        {
            Empleado e = new Empleado();

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            int idTiendaNull;

            cmd.CommandText = "SELECT * FROM Empleado WHERE codEmpleado=@codEmpleado ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@codEmpleado", codEmpleado);

            try
            {
                if (cmd.Transaction == null) conn.Open();
                reader = cmd.ExecuteReader();

                idTiendaNull = reader.GetOrdinal("idTienda");
                reader = cmd.ExecuteReader();
                e.Dni = reader.IsDBNull(reader.GetOrdinal("DNI"))  ? "" : reader["DNI"].ToString();
                e.Sexo = reader.IsDBNull(reader.GetOrdinal("sexo")) ? "" : reader["sexo"].ToString();
                e.ApePaterno = reader.IsDBNull(reader.GetOrdinal("apePaterno")) ? "" : reader["apePaterno"].ToString();
                e.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? "" : reader["nombre"].ToString();
                e.ApeMaterno = reader.IsDBNull(reader.GetOrdinal("apeMaterno")) ? "" : reader["apeMaterno"].ToString();

                e.Telefono = reader.IsDBNull(reader.GetOrdinal("telefono")) ? "" :  reader["telefono"].ToString();
                e.Celular = reader.IsDBNull(reader.GetOrdinal("celular")) ? "" : reader["celular"].ToString();
                e.EmailEmpleado = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader["email"].ToString();
                e.EmailEmpresa = reader.IsDBNull(reader.GetOrdinal("emailEmpresa")) ? "" : reader["emailEmpresa"].ToString();

                e.FechaReg = reader.IsDBNull(reader.GetOrdinal("fechaReg")) ? DateTime.MinValue : DateTime.Parse( reader["fechaReg"].ToString());
                e.FechNacimiento = reader.IsDBNull(reader.GetOrdinal("fechaNac")) ? DateTime.MinValue : DateTime.Parse( reader["fechaNac"].ToString());
                e.Direccion = reader.IsDBNull(reader.GetOrdinal("direccion")) ? "" : reader["direccion"].ToString();
                e.Referecia = reader.IsDBNull(reader.GetOrdinal("referencia")) ? "" : reader["referencia"].ToString();

                e.Estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? 0 : Convert.ToInt32(reader["estado"].ToString());
                e.Tienda = reader.IsDBNull(reader.GetOrdinal("tienda")) ? "" :  reader["tienda"].ToString();
                e.IdTienda = reader.IsDBNull(reader.GetOrdinal("idTienda")) ? 0 : reader.GetInt32(idTiendaNull);

                e.Area = reader.IsDBNull(reader.GetOrdinal("area")) ? "" : reader["area"].ToString();
                e.Puesto = reader.IsDBNull(reader.GetOrdinal("puesto")) ? "" :reader["puesto"].ToString();

                e.Sueldo =reader.IsDBNull(reader.GetOrdinal("sueldo")) ?   0 : Convert.ToDecimal(reader["sueldo"].ToString());
                e.CuentaBancaria = reader.IsDBNull(reader.GetOrdinal("cuentaBancaria")) ? "" : reader["cuentaBancaria"].ToString();
                e.Banco = reader.IsDBNull(reader.GetOrdinal("banco")) ? "" : reader["banco"].ToString();

                e.IdEmpleado = reader.IsDBNull(reader.GetOrdinal("idEmpleado")) ? "" : reader["idEmpleado"].ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }

            return e;
        }

        ////////////////////////////////ELIMINAR UN EMPLEADO

        public static int EliminarEmpleado(string dni)
        {

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "update Empleado set estado = 0 where DNI = " + dni;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

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

        public static int editarEmpleado(Empleado p)
        {

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;


            if (p.IdTienda != 0)
            {
                cmd.CommandText = "update Empleado set sexo = @sexo,nombre = @nombre,apePaterno = @apePaterno,apeMaterno = @apeMaterno,telefono = @telefono,celular = @celular, " +
            "email = @email,estado = @estado,direccion = @direccion,referencia = @referencia,fechaNac = @fechaNac,tienda = @tienda,idTienda=@idTienda,area = @area,puesto = @puesto,emailEmpresa = @emailEmpresa,sueldo = @sueldo, cuentaBancaria = @cuentaBancaria, banco = @banco where DNI = " + p.Dni;
            }
            else
            {
                cmd.CommandText = "update Empleado set sexo = @sexo,nombre = @nombre,apePaterno = @apePaterno,apeMaterno = @apeMaterno,telefono = @telefono,celular = @celular, " +
            "email = @email,estado = @estado,direccion = @direccion,referencia = @referencia,fechaNac = @fechaNac,tienda = @tienda, area = @area,puesto = @puesto,emailEmpresa = @emailEmpresa,sueldo = @sueldo, cuentaBancaria = @cuentaBancaria, banco = @banco where DNI = " + p.Dni;

            }
            
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;


            cmd.Parameters.AddWithValue("@sexo", p.Sexo);
            cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            cmd.Parameters.AddWithValue("@apePaterno", p.ApePaterno);
            cmd.Parameters.AddWithValue("@apeMaterno", p.ApeMaterno);

            cmd.Parameters.AddWithValue("@telefono", p.Telefono);
            cmd.Parameters.AddWithValue("@celular", p.Celular);
            cmd.Parameters.AddWithValue("@email", p.EmailEmpleado);
            cmd.Parameters.AddWithValue("@emailEmpresa", p.EmailEmpresa);


            cmd.Parameters.AddWithValue("@fechaNac", p.FechNacimiento);
            cmd.Parameters.AddWithValue("@direccion", p.Direccion);
            cmd.Parameters.AddWithValue("@referencia", p.Referecia);

            cmd.Parameters.AddWithValue("@estado", p.Estado);
            cmd.Parameters.AddWithValue("@tienda", p.Tienda);

            if (p.IdTienda != 0)
            {
                cmd.Parameters.AddWithValue("@idTienda", p.IdTienda);
            }

            cmd.Parameters.AddWithValue("@area", p.Area);
            cmd.Parameters.AddWithValue("@puesto", p.Puesto);

            cmd.Parameters.AddWithValue("@sueldo", p.Sueldo);
            cmd.Parameters.AddWithValue("@cuentaBancaria", p.CuentaBancaria);
            cmd.Parameters.AddWithValue("@banco", p.Banco);
            try
            {
                if (cmd.Transaction == null) conn.Open();

                k = cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;
        }

        public static void ActualizarTiendaEnUsuario(string codEmpleado, int idTienda)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();

            int k = 0;

            if (idTienda != 0)
            {
                cmd.CommandText = "UPDATE USUARIO set idTienda=@idTienda where codEmpleado=@codEmpleado ";
            }
            else
            {
                cmd.CommandText = "UPDATE USUARIO set idTienda=null where codEmpleado=@codEmpleado ";
            }

            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            if (idTienda != 0)
            {
                cmd.Parameters.AddWithValue("@idTienda", idTienda);
            }
            
            cmd.Parameters.AddWithValue("@codEmpleado", codEmpleado);

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
        }


        public static List<string> Tiendas()
        {
            List<string> tiendas = new List<string>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "select nombre from tienda";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string tienda;
                    tienda = reader["nombre"].ToString();
                    tiendas.Add(tienda);
                }
                conn.Close();
            }
            catch
            {
                MessageBox.Show("safs");
            }
            return tiendas;
        }

        public static int GetIdTienda(string nombTienda){
            int idTienda = 0;

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = " SELECT idTienda FROM Tienda WHERE upper(nombre)=upper(@nombre)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@nombre", nombTienda);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                    idTienda = Convert.ToInt32(reader["idTienda"].ToString());
                else
                    conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }

            return idTienda;
        }

        public static int GetIdTiendaPorCodEmpleado(string codEmpleado)
        {
            int idTienda = 0;

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = " SELECT idTienda FROM Empleado WHERE upper(codEmpleado)=upper(@codEmpleado)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@codEmpleado", codEmpleado);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                    idTienda = Convert.ToInt32(reader["idTienda"].ToString());
                else
                    conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }

            return idTienda;
        }


        public static List<Empleado> BuscarEmpleadoId(int id)
        {
            List<Empleado> lstEmpleado = new List<Empleado>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "select * from Empleado where codEmpleado = (select codEmpleado from Usuario where idUsuario = " + id + ")";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Empleado e = new Empleado();
                    
                    e.ApePaterno = reader["apePaterno"].ToString();
                    e.Nombre = reader["nombre"].ToString();
                    e.ApeMaterno = reader["apeMaterno"].ToString();

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
        ////////////////////////ELIMINAR USUARIO//////////////////////////////
        public static int eliminarUsuario(string p)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;

            cmd.CommandText = "UPDATE USUARIO set estado = 0 where codEmpleado = '" + p +"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

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
