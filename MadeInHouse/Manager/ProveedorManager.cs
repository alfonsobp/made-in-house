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
            DBConexion db = new DBConexion();
            int k = 0;
            Proveedor p = entity as Proveedor;

            db.cmd.CommandText = "INSERT INTO Proveedor(razonSocial,contacto,direccion,fax,telefono ,telefonoContacto,email,RUC)" +
            "VALUES (@razonSocial,@contacto,@direccion,@fax,@telefono ,@telefonoContacto,@email,@ruc)";
            db.cmd.CommandType = CommandType.Text;
            db.cmd.Connection = db.conn;

            //db.cmd.Parameters.AddWithValue("@codProveedor", p.CodProveedor);
            db.cmd.Parameters.AddWithValue("@razonSocial", p.RazonSocial);
            db.cmd.Parameters.AddWithValue("@contacto", p.Contacto);
            db.cmd.Parameters.AddWithValue("@direccion", p.Direccion);
            db.cmd.Parameters.AddWithValue("@fax", p.Fax);
            db.cmd.Parameters.AddWithValue("@telefono", p.Telefono);
            db.cmd.Parameters.AddWithValue("@telefonoContacto", p.TelefonoContacto);
            db.cmd.Parameters.AddWithValue("@email", p.Email);
            db.cmd.Parameters.AddWithValue("@ruc", p.Ruc);

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

        public object Buscar(params object[] filters)
        {
            
            List<Proveedor> lstProveedor = new List<Proveedor>();
            DBConexion db = new DBConexion();
            SqlDataReader reader;

            String where="";


            if (filters.Length > 1 && filters.Length <= 5)
            {

                string codigo = Convert.ToString(filters[0]);
                string ruc = Convert.ToString(filters[1]);
                string razonSocial = Convert.ToString(filters[2]);
                DateTime fechaIni = Convert.ToDateTime(filters[3]);
                DateTime fechaFin = Convert.ToDateTime(filters[4]);

                if (codigo != "")
                {
                    where += " and codProveedor = '" + codigo + "' ";
                }

                if (ruc != "")
                {
                    where += " and ruc = '" + ruc + "' ";
                }

                if (razonSocial != "")
                {
                    where += " and razonSocial LIKE  '%" + razonSocial + "%' ";
                }

                if (fechaIni != null)
                {


                    where += " and CONVERT(DATE,'" + fechaIni.ToString("yyyy-MM-dd") + "')   <=  CONVERT(DATE,fechaReg,103) ";

                }

                if (fechaFin != null)
                {

                    where += " and CONVERT(DATE,'" + fechaFin.ToString("yyyy-MM-dd") + "')   >=  CONVERT(DATE,fechaReg,103) ";
                }

            }

                // MessageBox.Show("SELECT * FROM Proveedor WHERE  estado = 1 " + where);

                db.cmd.CommandText = "SELECT * FROM Proveedor WHERE  estado = 1   " + where;
                db.cmd.CommandType = CommandType.Text;
                db.cmd.Connection = db.conn;



            try
            {
                db.conn.Open();

                reader = db.cmd.ExecuteReader();


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

                db.conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }


            return lstProveedor;

        }

        public int Actualizar(object entity)
        {
            DBConexion DB = new DBConexion();

            SqlConnection conn = DB.conn;
            SqlCommand cmd = DB.cmd;
            int k = 0;
            Proveedor p = entity as Proveedor;
            cmd.CommandText = "UPDATE Proveedor  " +
            "SET razonSocial= @razonSocial,contacto= @contacto,direccion= @direccion,fax= @fax,telefono= @telefono ,telefonoContacto= @telefonoContacto,email= @email ,ruc = @ruc " +
            " WHERE idProveedor = @IdProveedor ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            
            cmd.Parameters.AddWithValue("@IdProveedor", p.IdProveedor);
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
            DBConexion DB = new DBConexion();

            SqlConnection conn = DB.conn;
            SqlCommand cmd = DB.cmd;
            int k = 0;
            Proveedor p = entity as Proveedor;
            cmd.CommandText = "UPDATE Proveedor  " +
            "SET estado = @estado" +
            " WHERE idProveedor = @IdProveedor ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

         
            cmd.Parameters.AddWithValue("@estado", 0);
            cmd.Parameters.AddWithValue("@IdProveedor", p.IdProveedor);
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
