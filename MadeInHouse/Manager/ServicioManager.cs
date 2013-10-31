using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Model;
using System.Data.SqlClient;


namespace MadeInHouse.Manager
{
    class ServicioManager : EntityManager
    {
        public static string getCODfromProv(int proveedor)
        {
            DBConexion db = new DBConexion();
            SqlDataReader reader;
            string codProveedor = null;

            db.cmd.CommandText = "SELECT codProveedor FROM Proveedor WHERE idProveedor=@idProveedor ";
            db.cmd.Parameters.AddWithValue("@idProveedor", proveedor);

            try
            {
                db.conn.Open();
                reader = db.cmd.ExecuteReader();

                if (reader.Read())
                    codProveedor = reader["codProveedor"].ToString();
                else
                    MessageBox.Show("Proveedor no válido, revisar datos");

                db.conn.Close();

            }

            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return codProveedor;
        }


        public static int getIDfromProv(string proveedor)
        {
            DBConexion db = new DBConexion();
            SqlDataReader reader;
            int idProveedor = 0;

            db.cmd.CommandText = "SELECT idProveedor FROM Proveedor WHERE codProveedor=@codProveedor ";
            db.cmd.Parameters.AddWithValue("@codProveedor", proveedor);

            try
            {
                db.conn.Open();
                reader = db.cmd.ExecuteReader();

                if (reader.Read())
                    idProveedor = (int)(reader["idProveedor"]);
                else
                    MessageBox.Show("Proveedor no válido, revisar datos");

                db.conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return idProveedor;
        }

        public int Agregar(object entity)
        {
            DBConexion db = new DBConexion();
            int k = 0;
            Servicio s = entity as Servicio;

            db.cmd.CommandText = "INSERT INTO Servicio(codServicio,idProveedor,nombre,descripcion)" +
                                 "VALUES (@codServicio,@idProveedor,@nombre,@descripcion)";

            db.cmd.Parameters.AddWithValue("@codServicio", s.CodServicio);
            db.cmd.Parameters.AddWithValue("@idProveedor", s.IdProveedor);
            db.cmd.Parameters.AddWithValue("@nombre", s.Nombre);
            db.cmd.Parameters.AddWithValue("@descripcion", s.Descripcion);

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
            List<Servicio> lstServicio = new List<Servicio>();
            DBConexion db = new DBConexion();
            SqlDataReader reader;

            db.cmd.CommandText = "SELECT * FROM Servicio ";

            try
            {
                db.conn.Open();
                reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {
                    Servicio s = new Servicio();
                    s.CodServicio = reader["codServicio"].ToString();
                    s.Nombre = reader["nombre"].ToString();
                    s.IdProveedor = (int)(reader["idProveedor"]);
                    s.Descripcion = reader["descripcion"].ToString();

                    lstServicio.Add(s);
                }

                db.conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return lstServicio;
        }

        public int Actualizar(object entity)
        {
            DBConexion db = new DBConexion();
            Servicio s = entity as Servicio;
            int k = 0;

            db.cmd.CommandText = "UPDATE Servicio " +
                                 "SET nombre= @nombre,descripcion= @descripcion " +
                                 "WHERE codServicio= @codServicio ";

            db.cmd.Parameters.AddWithValue("@codServicio", s.CodServicio);
            db.cmd.Parameters.AddWithValue("@nombre", s.Nombre);
            db.cmd.Parameters.AddWithValue("@descripcion", s.Descripcion);

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

        public int Eliminar(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
