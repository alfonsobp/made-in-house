using MadeInHouse.Models.Compras;
using System;
using System.Collections.Generic;
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

            db.cmd.CommandText = "INSERT INTO OrdenCompraxProducto(idOrden,idProducto,cantidad)" +
                                 "VALUES (@idOrden,@idProducto,@cantidad)";

            db.cmd.Parameters.AddWithValue("@idOrden", c.IdOrden);
            db.cmd.Parameters.AddWithValue("@idProducto", c.Producto.IdProducto);
            db.cmd.Parameters.AddWithValue("@cantidad", c.Cantidad);
           

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
            throw new NotImplementedException();
        }

        public int Actualizar(object entity)
        {
            throw new NotImplementedException();
        }

        public int Eliminar(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
