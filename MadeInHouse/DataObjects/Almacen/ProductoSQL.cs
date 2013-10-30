using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;
using System.Data.SqlClient;
using System.Windows;
using System.Data;

namespace MadeInHouse.DataObjects.Almacen
{
    class ProductoSQL
    {

        private DBConexion db;

        public ProductoSQL()
        {
            db = new DBConexion();
        }


        public void AgregarProducto(Producto p)
        {
            db.cmd.CommandText = "INSERT INTO Producto(codProducto, nombre, descripcion, percepcion,idSubLinea,idLinea,estado,idUnidad,abreviatura) " +
            "VALUES (@codProducto,@nombre,@descripcion,@percepcion,@idSubLinea,@idLinea,@estado,@idUnidad,@abreviatura)";
            db.cmd.Parameters.AddWithValue("@codProducto", p.CodigoProd);
            db.cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            db.cmd.Parameters.AddWithValue("@abreviatura", p.Abreviatura);
            db.cmd.Parameters.AddWithValue("@descripcion", p.Descripcion==null ? "":p.Descripcion );
            db.cmd.Parameters.AddWithValue("@idUnidad", p.IdUnidad);
            db.cmd.Parameters.AddWithValue("@percepcion", p.Percepcion);
            db.cmd.Parameters.AddWithValue("@idSubLinea", p.IdSubLinea);
            db.cmd.Parameters.AddWithValue("@idLinea", p.IdLinea);
            db.cmd.Parameters.AddWithValue("@estado", 1);

            try
            {
                db.conn.Open();


                db.cmd.ExecuteNonQuery();
                db.cmd.Parameters.Clear();
                db.conn.Close();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }
        }


        public void ActualizarProducto(Producto p)
        {

            db.cmd.CommandText = "UPDATE Producto  " +
            "SET codProducto= @codProducto,nombre= @nombre,descripcion= @descripcion,idUnidad=@idUnidad, " +
            "percepcion= @percepcion,abreviatura= @abreviatura , idSubLinea=@idSubLinea, idLinea=@idLinea " +
            " WHERE idProducto= @idProducto ";


            db.cmd.Parameters.AddWithValue("@idProducto", p.IdProducto);
            db.cmd.Parameters.AddWithValue("@codProducto", p.CodigoProd);
            db.cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            db.cmd.Parameters.AddWithValue("@descripcion", p.Descripcion);
            db.cmd.Parameters.AddWithValue("@idUnidad", p.IdUnidad);
            db.cmd.Parameters.AddWithValue("@percepcion", p.Percepcion);
            db.cmd.Parameters.AddWithValue("@abreviatura", p.Abreviatura);
            db.cmd.Parameters.AddWithValue("@idSubLinea", p.IdSubLinea);
            db.cmd.Parameters.AddWithValue("@idLinea", p.IdLinea);
            

            try
            {
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.cmd.Parameters.Clear();
                db.conn.Close();
               
            }

            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

        }


        public void EliminarProducto(int id)
        {
            db.cmd.CommandText = "UPDATE Producto SET estado=0 WHERE idProducto=@idProducto";
            db.cmd.Parameters.AddWithValue("@idProducto", id);

            try
            {
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.conn.Close();


            }
            catch (SqlException e)
            {
                Console.WriteLine(e);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

        }




        public List<Producto> BuscarProducto(String codigo, int idLinea, int idSubLinea)
        {
            List<Producto> listaProductos = null;
            
            
            string where = "WHERE 1=1 ";

            if (!String.IsNullOrEmpty(codigo))
            {
                where = where + " AND codProducto = @codigo ";
                db.cmd.Parameters.AddWithValue("@codigo", codigo);
            }
            
            if (idLinea != 0)
            {
                where = where + " AND idLinea=@idLinea ";
                db.cmd.Parameters.AddWithValue("@idLinea", idLinea);
            }
            if (idSubLinea != 0)
            {
                where = where + " AND idSubLinea=@idSubLinea ";
                db.cmd.Parameters.AddWithValue("@idSubLinea", idSubLinea);
            }

            db.cmd.CommandText = "SELECT * FROM Producto " + where;

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (listaProductos == null) listaProductos = new List<Producto>();
                    Producto p = new Producto();
                    p.IdProducto = Int32.Parse(reader["idProducto"].ToString());
                    p.CodigoProd = reader.IsDBNull(reader.GetOrdinal("codProducto")) ? null : reader["codProducto"].ToString();
                    p.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();
                    p.Abreviatura = reader.IsDBNull(reader.GetOrdinal("abreviatura")) ? null : reader["abreviatura"].ToString();
                    p.Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader["descripcion"].ToString();
                    p.IdLinea = reader.IsDBNull(reader.GetOrdinal("idLinea")) ? -1 : (int)reader["idLinea"];
                    p.IdSubLinea = reader.IsDBNull(reader.GetOrdinal("idSubLinea")) ? -1 : (int)reader["idSubLinea"];
                    p.Percepcion = Int32.Parse(reader["percepcion"].ToString());
                    listaProductos.Add(p);
                    
                }
                db.cmd.Parameters.Clear();
                db.conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

            return listaProductos;
        }


        public Producto Buscar_por_CodigoProducto(string codProducto)
        {

            DBConexion DB = new DBConexion();

            SqlConnection conn = DB.conn;
            SqlCommand cmd = DB.cmd;
            SqlDataReader reader;

            Producto p = null;

            cmd.CommandText = "SELECT * from Producto where codProducto = @codProducto and estado = 1 ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@codProducto", codProducto);


            try
            {
                conn.Open();





                reader = cmd.ExecuteReader();


                if (reader.Read())
                {

                    p = new Producto();
                    p.IdProducto = Convert.ToInt32(reader["idProducto"].ToString());
                    p.Nombre = reader["nombre"].ToString();
                    p.CodigoProd = reader["codProducto"].ToString();
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }


            return p;

        }

        public Producto Buscar_por_CodigoProducto(int IdProducto)
        {

            DBConexion DB = new DBConexion();

            SqlConnection conn = DB.conn;
            SqlCommand cmd = DB.cmd;
            SqlDataReader reader;

            Producto p = null;

            cmd.CommandText = "SELECT * from Producto where idProducto = @idProducto and estado = 1 ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@idProducto", IdProducto);


            try
            {
                conn.Open();





                reader = cmd.ExecuteReader();


                if (reader.Read())
                {

                    p = new Producto();
                    p.IdProducto = Convert.ToInt32(reader["idProducto"].ToString());
                    p.Nombre = reader["nombre"].ToString();
                    p.CodigoProd = reader["codProducto"].ToString();
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }


            return p;

        }
    }
}
