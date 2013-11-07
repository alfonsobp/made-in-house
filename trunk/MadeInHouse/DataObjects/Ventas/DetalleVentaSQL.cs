using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MadeInHouse.Models.Ventas;
using MadeInHouse.Models.Almacen;
using System.Windows;

namespace MadeInHouse.DataObjects.Ventas
{
    class DetalleVentaSQL
    {
        public Producto Buscar(string idProducto)
        {
            DBConexion db = new DBConexion();
            Producto prod = new Producto();

            db.cmd.CommandText = "select * from Producto where idProducto=" + Convert.ToInt32(idProducto);

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                while (reader.Read())
                {
                    prod.IdProducto = Convert.ToInt32(reader["idProducto"].ToString());
                    prod.CodigoProd = reader["codProducto"].ToString();
                    prod.Nombre = reader["nombre"].ToString();
                    prod.Precio = Double.Parse(reader["precio"].ToString());
                }
                db.cmd.Parameters.Clear();
                db.conn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }
            return prod;
        }

        public int Actualizar(DetalleVenta dv)
        {
            throw new NotImplementedException();
        }

        public int Eliminar(object entity)
        {
            throw new NotImplementedException();
        }

        public void Agregar(Venta v, DetalleVenta dv)
        {
            DBConexion db = new DBConexion();
            db.cmd.CommandText = "INSERT INTO DetalleVenta(cantidad,descuento,idVenta,idProducto) VALUES(@cantidad,@descuento,@idVenta,@idProducto)";
            db.cmd.Parameters.AddWithValue("@cantidad", dv.Cantidad);
            db.cmd.Parameters.AddWithValue("@descuento", dv.Descuento);
            db.cmd.Parameters.AddWithValue("@idVenta", v.IdVenta);
            db.cmd.Parameters.AddWithValue("@idProducto", dv.IdProducto);

            try
            {
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.conn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }
        }
    }
}
