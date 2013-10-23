using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;
using System.Data.SqlClient;

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
            db.cmd.CommandText = "INSERT INTO Producto(codProducto, nombre, descripcion, unidadMedida, percepcion,tipoUso,abreviatura,observaciones,idSubLinea,idLinea,estado)" +
            "VALUES (@codProveedor,@razonSocial,@contacto,@direccion,@fax,@telefono ,@telefContacto,@email,@ruc)";
            db.cmd.Parameters.AddWithValue("@codProducto", p.CodigoProd);
            db.cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            db.cmd.Parameters.AddWithValue("@descripcion", p.Descripcion);
            db.cmd.Parameters.AddWithValue("@unidadMedida", p.UnidadMedida);
            db.cmd.Parameters.AddWithValue("@percepcion", p.Percepcion);
            db.cmd.Parameters.AddWithValue("@tipoUso", p.TipoUso);
            db.cmd.Parameters.AddWithValue("@abreviatura", p.Abreviatura);
            db.cmd.Parameters.AddWithValue("@observaciones", p.Observaciones);
            db.cmd.Parameters.AddWithValue("@idSubLinea", p.IdSubLinea);
            db.cmd.Parameters.AddWithValue("@idLinea", p.IdLinea);
            db.cmd.Parameters.AddWithValue("@estado", p.Estado);

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


        public void ActualizarProducto(Producto p)
        {

            db.cmd.CommandText = "UPDATE Producto  " +
            "SET codProducto= @codProducto,nombre= @nombre,descripcion= @descripcion,unidadMedida=@unidadMedida, "+
            "percepcion= @percepcion ,tipoUso= @tipoUso,abreviatura= @abreviatura ,observaciones = @observaciones, idSubLinea=@idSubLinea, idLinea=@idLinea, estado=@estado "+
            " WHERE idProducto= @idProducto ";
            

            db.cmd.Parameters.AddWithValue("@codProducto", p.CodigoProd);
            db.cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            db.cmd.Parameters.AddWithValue("@descripcion", p.Descripcion);
            db.cmd.Parameters.AddWithValue("@unidadMedida", p.UnidadMedida);
            db.cmd.Parameters.AddWithValue("@percepcion", p.Percepcion);
            db.cmd.Parameters.AddWithValue("@tipoUso", p.TipoUso);
            db.cmd.Parameters.AddWithValue("@abreviatura", p.Abreviatura);
            db.cmd.Parameters.AddWithValue("@observaciones", p.Observaciones);
            db.cmd.Parameters.AddWithValue("@idSubLinea", p.IdSubLinea);
            db.cmd.Parameters.AddWithValue("@idSubLinea", p.IdLinea);
            //db.cmd.Parameters.AddWithValue("@estado", p.Estado);

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




        public List<Producto> BuscarProducto(String codigo, String tipoUso, int idLinea, int idSubLinea)
        {
            List<Producto> listaProductos = null;
            
            
            string where = "WHERE 1=1 ";

            if (!String.IsNullOrEmpty(codigo))
            {
                where = where + " AND codProducto = @codigo ";
                db.cmd.Parameters.Add(new SqlParameter("codProducto", codigo));
            }
            if (!String.IsNullOrEmpty(tipoUso))
            {
                where = where + " AND tipoUso=@tipoUso ";
                db.cmd.Parameters.Add(new SqlParameter("tipoUso", tipoUso));
            }
            if (idLinea != -1)
            {
                where = where + " AND idLinea=@idLinea ";
                db.cmd.Parameters.Add(new SqlParameter("idLinea", idLinea));
            }
            if (idSubLinea != -1)
            {
                where = where + " AND idSubLinea=@idSubLinea ";
                db.cmd.Parameters.Add(new SqlParameter("idSubLinea", idSubLinea));
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
                    p.CodigoProd = reader.IsDBNull(reader.GetOrdinal("codProducto")) ? null : reader["codProducto"].ToString();
                    p.TipoUso = reader.IsDBNull(reader.GetOrdinal("tipoUso")) ? null : reader["tipoUso"].ToString();
                    p.IdLinea = reader.IsDBNull(reader.GetOrdinal("idLinea")) ? -1 : (int)reader["idLinea"];
                    p.IdSubLinea = reader.IsDBNull(reader.GetOrdinal("idSubLinea")) ? -1 : (int)reader["idSubLinea"];
                    
                }

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
    }
}
