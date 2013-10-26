using MadeInHouse.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.Manager
{
    class ProveedorManager : EntityManager
    {

        public int Agregar(object entity)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;
            Proveedor p = entity as Proveedor;
            cmd.CommandText = "INSERT INTO Proveedor(codProveedor,razonSocial,contacto,direccion,fax,telefono ,telefonoContacto,email,RUC)" +
            "VALUES (@codProveedor,@razonSocial,@contacto,@direccion,@fax,@telefono ,@telefonoContacto,@email,@ruc)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@codProveedor", p.CodProveedor);
            cmd.Parameters.AddWithValue("@razonSocial", p.RazonSocial);
            cmd.Parameters.AddWithValue("@contacto", p.Contacto);
            cmd.Parameters.AddWithValue("@direccion", p.Direccion);
            cmd.Parameters.AddWithValue("@fax", p.Fax);
            cmd.Parameters.AddWithValue("@telefono", p.Telefono);
            cmd.Parameters.AddWithValue("@telefonoContacto", p.TelefonoContacto);
            cmd.Parameters.AddWithValue("@email", p.Email);
            cmd.Parameters.AddWithValue("@ruc", p.Ruc);

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

        public object Buscar(params object[] filters)
        {
            List<Proveedor> lstProveedor = new List<Proveedor>();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM Proveedor ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    Proveedor p = new Proveedor();
                    p.IdProveedor = Convert.ToInt32(reader["idProveedor"].ToString() );
                    p.CodProveedor = reader["codProveedor"].ToString();
                    p.RazonSocial = reader["razonSocial"].ToString();
                    p.Contacto = reader["contacto"].ToString();
                    p.Direccion = reader["direccion"].ToString();
                    p.Fax = reader["fax"].ToString();
                    p.Telefono = reader["telefono"].ToString();
                    p.TelefonoContacto = reader["telefonoContacto"].ToString();
                    p.Email = reader["email"].ToString();
                    p.Ruc = reader["Ruc"].ToString();
                    lstProveedor.Add(p);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }


            return lstProveedor;

        }

        public int Actualizar(object entity)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.inf245g4ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int k = 0;
            Proveedor p = entity as Proveedor;
            cmd.CommandText = "UPDATE Proveedor  " +
            "SET razonSocial= @razonSocial,contacto= @contacto,direccion= @direccion,fax= @fax,telefono= @telefono ,telefonoContacto= @telefonoContacto,email= @email ,ruc = @ruc " +
            " WHERE codProveedor= @codProveedor ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            
            cmd.Parameters.AddWithValue("@codProveedor", p.CodProveedor);
            cmd.Parameters.AddWithValue("@razonSocial", p.RazonSocial);
            cmd.Parameters.AddWithValue("@contacto", p.Contacto);
            cmd.Parameters.AddWithValue("@direccion", p.Direccion);
            cmd.Parameters.AddWithValue("@fax", p.Fax);
            cmd.Parameters.AddWithValue("@telefono", p.Telefono);
            cmd.Parameters.AddWithValue("@telefonoContacto", p.TelefonoContacto);
            cmd.Parameters.AddWithValue("@email", p.Email);
            cmd.Parameters.AddWithValue("@ruc", p.Ruc);

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

        public int Eliminar(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
