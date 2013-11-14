using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Data;
using MadeInHouse.Models.Ventas;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.DataObjects.Ventas
{
    class DetalleProductoVentaSQL
    {

        private DBConexion db;
        private bool tipo = true;

        public DetalleProductoVentaSQL(DBConexion db = null)
        {
            if (db == null)
            {
                this.db = new DBConexion();
            }
            else
            {
                this.db = db;
                tipo = false;
            }
        }

        public List<DetalleProductoVenta> BuscarProducto(String codigo = null, int idLinea = -1, int idSubLinea = -1, int idTienda = -1)
        {
            List<DetalleProductoVenta> listaProductos = null;

            string where = "WHERE 1=1 ";
            string from = "SELECT p.* , L.nombre linea, S.nombre sublinea pt.precioVenta precioVenta FROM Producto p" +
                        " JOIN LineaProducto L " +
                        " ON (P.idLinea=L.idLinea) " +
                        " JOIN SubLineaProducto S " +
                        " ON (P.idSubLinea=S.idSubLinea) " +
                        " JOIN ProductoxTienda pt " + 
                        " ON (P.idProducto=PT.idProducto) ";

            if (!String.IsNullOrEmpty(codigo))
            {
                where = where + " AND codProducto = @codigo ";
                db.cmd.Parameters.AddWithValue("@codigo", codigo);
            }

            if (idLinea > 0)
            {
                where = where + " AND p.idLinea=@idLinea ";
                db.cmd.Parameters.AddWithValue("@idLinea", idLinea);
            }
            if (idSubLinea > 0)
            {
                where = where + " AND p.idSubLinea=@idSubLinea ";
                db.cmd.Parameters.AddWithValue("@idSubLinea", idSubLinea);
            }
            if (idTienda > 0)
            {
                from = "SELECT p.*, pt.precioVenta , L.nombre linea, S.nombre sublinea" +
                        " FROM Producto p " +
                        " JOIN LineaProducto L " +
                        " ON (P.idLinea=L.idLinea) " +
                        " JOIN SubLineaProducto S " +
                        " ON (P.idSubLinea=S.idSubLinea) " +
                        " JOIN ProductoxTienda pt ON ( p.idProducto = pt.idProducto) ";
                where += " AND  pt.idTienda = @idTienda AND vigente=1 ";
                db.cmd.Parameters.AddWithValue("@idTienda", idTienda);
            }


            db.cmd.CommandText = from + where;
            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (listaProductos == null) listaProductos = new List<DetalleProductoVenta>();
                    DetalleProductoVenta p = new DetalleProductoVenta();
                    p.IdProducto = Int32.Parse(reader["idProducto"].ToString());
                    p.CodigoProd = reader.IsDBNull(reader.GetOrdinal("codProducto")) ? null : reader["codProducto"].ToString();
                    p.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();

                    LineaProducto lp = new LineaProducto();
                    lp.IdLinea = reader.IsDBNull(reader.GetOrdinal("idLinea")) ? -1 : (int)reader["idLinea"];
                    lp.Nombre = reader.IsDBNull(reader.GetOrdinal("linea")) ? null : reader["linea"].ToString();
                    p.Linea = lp;

                    SubLineaProducto slp = new SubLineaProducto();
                    slp.IdSubLinea = reader.IsDBNull(reader.GetOrdinal("idSubLinea")) ? -1 : (int)reader["idSubLinea"];
                    slp.Nombre = reader.IsDBNull(reader.GetOrdinal("sublinea")) ? null : reader["sublinea"].ToString();
                    p.Sublinea = slp;

                    if (idTienda > 0)
                    {
                        p.PrecioVenta = reader.IsDBNull(reader.GetOrdinal("precioVenta")) ? -1 : float.Parse(reader["precioVenta"].ToString());
                    }

                    listaProductos.Add(p);
                }
                db.cmd.Parameters.Clear();
                reader.Close();
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

        public int ActualizarProducto(int idProducto, int idTienda, float precioVenta)
        {
            int k = 0;

            db.cmd.CommandText = "UPDATE ProductoxTienda  " +
           "SET precioVenta= @precioVenta " +
           " WHERE idProducto= @idProducto AND idTienda=@idTienda ";


            //db.cmd.Parameters.AddWithValue("@idProducto", dpv.IdProducto);
            //db.cmd.Parameters.AddWithValue("@idTienda", dpv.Tienda.IdTienda);
            //db.cmd.Parameters.AddWithValue("@precioVenta", dpv.PrecioVenta);

            db.cmd.Parameters.AddWithValue("@idProducto", idProducto);
            db.cmd.Parameters.AddWithValue("@idTienda", idTienda);
            db.cmd.Parameters.AddWithValue("@precioVenta", precioVenta);

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
            return k;
        }

    }
}
