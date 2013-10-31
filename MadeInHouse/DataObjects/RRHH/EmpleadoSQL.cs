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

            cmd.CommandText = "insert into Empleado (DNI,sexo,nombre,apePaterno,apeMaterno,telefono,celular,email,estado,fechaReg,direccion,referencia,fechaNac,tienda,area,puesto,emailEmpresa,sueldo,cuentaBancaria,banco,codEmpleado) " +
            "VALUES (@DNI,@sexo,@nombre,@apePaterno,@apeMaterno,@telefono,@celular,@email,@estado,@fechaReg,@direccion,@referencia,@fechaNac,@tienda,@area,@puesto,@emailEmpresa,@sueldo,@cuentaBancaria,@banco,@codEmpleado)";
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
        public static List<Empleado> BuscarEmpleado(string nombre,string paterno,string dni,string area,string puesto,string tienda)
        {
            List<Empleado> lstEmpleado = new List<Empleado>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM Empleado where nombre like '" + nombre + "%' and apePaterno like '" + paterno + "%' and DNI like '" + dni + "%' and tienda like '" + tienda + "%' and area like '" + area + "%' and puesto like '" + puesto + "%'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Empleado e = new Empleado();
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

            cmd.CommandText = "update Empleado set sexo = @sexo,nombre = @nombre,apePaterno = @apePaterno,apeMaterno = @apeMaterno,telefono = @telefono,celular = @celular, " +
            "email = @email,estado = @estado,direccion = @direccion,referencia = @referencia,fechaNac = @fechaNac,tienda = @tienda,area = @area,puesto = @puesto,emailEmpresa = @emailEmpresa,sueldo = @sueldo, cuentaBancaria = @cuentaBancaria, banco = @banco where DNI = " + p.Dni;
             
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

    }
}
