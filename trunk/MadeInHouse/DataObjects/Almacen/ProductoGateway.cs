using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;
using System.Data.SqlClient;

namespace MadeInHouse.DataObjects.Almacen
{
    class ProductoGateway
    {

        private DBConexion db;

        public ProductoGateway()
        {
            db = new DBConexion();
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
