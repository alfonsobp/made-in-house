using MadeInHouse.Models.Compras;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects.Almacen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.DataObjects.Compras
{
    class ServicioSQL:EntitySQL
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

            db.cmd.CommandText = "INSERT INTO Servicio(idProveedor,nombre,descripcion)" +
                                 "VALUES (@idProveedor,@nombre,@descripcion)";

            //db.cmd.Parameters.AddWithValue("@codServicio", s.CodServicio);
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

            String where = "";
            string codProducto = "";

            if (filters.Length > 0 && filters.Length <= 3)
            {

                string proveedor = Convert.ToString(filters[0]);
                string nombre = Convert.ToString(filters[1]);
                codProducto = Convert.ToString(filters[2]);


                if (proveedor != "")
                {
                    int idProveedor = getIDfromProv(proveedor);
                    where += " and idProveedor = '" + idProveedor.ToString() + "' ";
                }

                if (nombre != "")
                {
                    where += " and nombre LIKE  '%" + nombre + "%' ";
                }


            }


            db.cmd.CommandText = "SELECT * FROM Servicio WHERE  estado = 1   " + where;
            db.cmd.CommandType = CommandType.Text;
            db.cmd.Connection = db.conn;



            try
            {
                db.conn.Open();

                reader = db.cmd.ExecuteReader();


                while (reader.Read())
                {

                    Servicio s = new Servicio();

                    s.IdServicio = Convert.ToInt32(reader["idServicio"].ToString());
                    s.IdProveedor = Convert.ToInt32(reader["idProveedor"].ToString());
                    s.CodServicio = reader["codServicio"].ToString();
                    s.Nombre = reader["nombre"].ToString();
                    s.Descripcion = reader["descripcion"].ToString();

                    if (codProducto != "")
                    {
                        ServicioxProductoSQL spSQL = new ServicioxProductoSQL();
                        Producto p = new ProductoSQL().Buscar_por_CodigoProducto(codProducto);
                        
                        if (p != null)
                        {
                            //MessageBox.Show("idServ = " + s.IdServicio + " IdProd = " + p.IdProducto);
                            if (spSQL.productoPertenece(s.IdServicio, p.IdProducto) == true)
                                lstServicio.Add(s);
                        }
                    }

                    else
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
