using MadeInHouse.DataObjects.Almacen;
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
    class OrdenCompraxProductoSQL:EntitySQL
    {
        public int Agregar(object entity)
        {
            DBConexion db = new DBConexion();
            int k = 0;
            ProductoxOrdenCompra c = entity as ProductoxOrdenCompra;

            db.cmd.CommandText = "INSERT INTO OrdenCompraxProducto(idOrden,idProducto,cantidad,cantAtendida,PU)" +
                                 "VALUES (@idOrden,@idProducto,@cantidad,@cantAtendida,@PU)";

            db.cmd.Parameters.AddWithValue("@idOrden", c.IdOrden);
            db.cmd.Parameters.AddWithValue("@idProducto", c.Producto.IdProducto);
            db.cmd.Parameters.AddWithValue("@cantidad", c.Cantidad);
            db.cmd.Parameters.AddWithValue("@cantAtendida", 0);
            db.cmd.Parameters.AddWithValue("@PU", c.PrecioUnitario);
           

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

            int id = Convert.ToInt32(filters[0]);


            DBConexion DB = new DBConexion();

            SqlConnection conn = DB.conn;
            SqlCommand cmd = DB.cmd;
            SqlDataReader reader;


            cmd.CommandText = "SELECT * FROM  OrdenCompraxProducto where idOrden = " + id;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            List<ProductoxOrdenCompra> lst = new List<ProductoxOrdenCompra>();

             try
            {
                conn.Open();

                reader = cmd.ExecuteReader();


                while (reader.Read())
                {


                    ProductoxOrdenCompra p = new ProductoxOrdenCompra();
                    p.Producto = new ProductoSQL().Buscar_por_CodigoProducto(Convert.ToInt32(reader["idProducto"].ToString()));
                    p.Cantidad = reader["cantidad"].ToString();
                    p.IdOrden = id;
                    p.PrecioUnitario = Convert.ToDouble(reader["PU"].ToString());
                    p.Monto = p.PrecioUnitario * (Convert.ToInt32(p.Cantidad));
                    p.CantAtendida = Convert.ToInt32(reader["cantAtendida"].ToString());
                    lst.Add(p);

                    //MessageBox.Show("Detalle por producto: \nProducto = " + p.Producto.Nombre + "\ncant = " + p.Cantidad + 
                                    //"\nPU = " + p.PrecioUnitario);
                }

                conn.Close();

            }
            catch (Exception e)
            { 
                MessageBox.Show(e.StackTrace.ToString());
            }

            return lst;

        }

        public int Actualizar(object entity)
        {
            throw new NotImplementedException();
        }

        public int Eliminar(object entity)
        {
            OrdenCompra o = entity as OrdenCompra;

            int id = o.IdOrden;

                
            DBConexion db = new DBConexion();
            int k = 0;
            ProductoxOrdenCompra c = entity as ProductoxOrdenCompra;

            db.cmd.CommandText = "DELETE FROM  OrdenCompraxProducto" +
                                 "WHERE  idOrden = @idOrden";

            db.cmd.Parameters.AddWithValue("@idOrden", id);
         
           

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
    }
}
