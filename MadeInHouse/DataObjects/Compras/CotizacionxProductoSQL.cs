using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Compras;
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
    class CotizacionxProductoSQL:EntitySQL
    {
        public int Agregar(object entity)
        {
            throw new NotImplementedException();
        }

        public object Buscar(params object[] filters)
        {
            string where = "";
            int codCotizacion;

            if (filters.Length != 0)
            {

                codCotizacion = Convert.ToInt32(filters[0]);
                where += "idCotizacion = " + codCotizacion;

            }


            DBConexion DB = new DBConexion();

            SqlConnection conn = DB.conn;
            SqlCommand cmd = DB.cmd;
            SqlDataReader reader;


            cmd.CommandText = "SELECT * FROM CotizacionxProducto WHERE " + where;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            List<CotizacionxProducto> lstCotizaciones = new List<CotizacionxProducto>();

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    CotizacionxProducto c = new CotizacionxProducto();
                    c.IdCotizacion = Convert.ToInt32(reader["idCotizacion"].ToString());
                    c.Producto = new ProductoSQL().Buscar_por_CodigoProducto(Convert.ToInt32(reader["idProducto"].ToString()));
                    c.Cantidad = Convert.ToInt32(reader["cantidad"].ToString());
                    c.Precio = Convert.ToDouble(reader["precio"].ToString());

                    lstCotizaciones.Add(c);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return lstCotizaciones;
        }

        public int Actualizar(object entity)
        {
            throw new NotImplementedException();
        }

        public int Eliminar(object entity)
        {
            throw new NotImplementedException();
        }

        public int InsertarValidado(CotizacionxProducto cp)
        {
            Producto p = new ProductoSQL().Buscar_por_CodigoProducto(cp.Producto.CodigoProd);
            int k = 0;
            //MessageBox.Show("nombre = " + p.Nombre);
            ////cp.Producto.IdProducto = p.IdProducto;
            cp.Producto = p;

            if (p != null)
            {

                DBConexion DB = new DBConexion();

                SqlConnection conn = DB.conn;
                SqlCommand cmd = DB.cmd;

 //               MessageBox.Show("IDcot = " + cp.IdCotizacion + " IDprod = " + cp.Producto.IdProducto + " precio = " + cp.Precio + " cantidad = " + cp.Cantidad);

                cmd.CommandText = "IF NOT EXISTS(SELECT 1 from CotizacionxProducto where idCotizacion = @idCotizacion and idProducto = @idProducto) " +
                                   "Insert into CotizacionxProducto(idCotizacion,idProducto,cantidad,precio) " +
                                   "VALUES (@idCotizacion,@idProducto,@cantidad,@precio) " +
                                    " else " +
                                    "UPDATE CotizacionxProducto set cantidad = @cantidad, precio = @precio " +
                                    "where idCotizacion = @idCotizacion and idProducto = @idProducto ";

                cmd.Parameters.AddWithValue("@idCotizacion", cp.IdCotizacion);
                cmd.Parameters.AddWithValue("@idProducto", cp.Producto.IdProducto);
                cmd.Parameters.AddWithValue("@cantidad", cp.Cantidad);
                cmd.Parameters.AddWithValue("@precio", cp.Precio);


                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;


                try
                {
                    conn.Open();


                    k = cmd.ExecuteNonQuery();


                    conn.Close();

                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.StackTrace.ToString());
                }

            }
            return k;
        }
    }
}
