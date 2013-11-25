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
        private DBConexion db;
        private bool tipo = true;

        public DetalleVentaSQL(DBConexion db = null)
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

        public Producto Buscar(string codProducto, int idTienda)
        {
            Producto prod = new Producto();
            db.cmd.CommandText = "select * from Producto where codProducto=@codProducto";
            db.cmd.Parameters.AddWithValue("@codProducto", codProducto);
            try
            {
                if (tipo) db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("El producto ingreasado no existe en el sistema", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                while (reader.Read())
                {
                    prod.IdProducto = Convert.ToInt32(reader["idProducto"].ToString());
                    prod.CodigoProd = reader["codProducto"].ToString();
                    prod.Nombre = reader["nombre"].ToString();
                    prod.UnidadMedida = new DataObjects.Almacen.UnidadMedidaSQL().BuscarUnidadById(Convert.ToInt32(reader["idUnidad"].ToString()));
                }
                db.cmd.Parameters.Clear();
                if (tipo) db.conn.Close();
                reader.Close();
                db.cmd.Parameters.Clear();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }

            db.cmd.CommandText = "select * from ProductoxTienda where idProducto=" + prod.IdProducto + " AND idTienda=" + idTienda;
            try
            {
                if (tipo) db.conn.Open();
                SqlDataReader rs = db.cmd.ExecuteReader();
                if (!rs.HasRows)
                {
                    MessageBox.Show("El producto ingresado no esta en la tienda", "AVISO", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                while (rs.Read())
                {
                    prod.Precio = Double.Parse(rs["precioVenta"].ToString());
                }
                db.cmd.Parameters.Clear();
                if (tipo) db.conn.Close();
                db.cmd.Parameters.Clear();
                rs.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
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
            db.cmd.CommandText = "INSERT INTO DetalleVenta(cantidad,descuento,precio,idVenta,idProducto) VALUES(@cantidad,@descuento,@precio,@idVenta,@idProducto)";
            db.cmd.Parameters.AddWithValue("@cantidad", dv.Cantidad);
            db.cmd.Parameters.AddWithValue("@descuento", dv.Descuento);
            db.cmd.Parameters.AddWithValue("@precio", dv.Precio);
            db.cmd.Parameters.AddWithValue("@idVenta", v.IdVenta);
            db.cmd.Parameters.AddWithValue("@idProducto", dv.IdProducto);

            try
            {
                if (tipo) db.conn.Open();
                db.cmd.ExecuteNonQuery();
                if (tipo) db.conn.Close();
                db.cmd.Parameters.Clear();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void AgregarServicios(Venta v, DetalleVentaServicio item)
        {
            db.cmd.CommandText = "INSERT INTO DetalleVentaServicio(idServicio,idVenta,estado,idProducto) VALUES(@idServicio,@idVenta,@estado,@idProducto)";
            db.cmd.Parameters.AddWithValue("@idServicio", item.IdServicio);
            db.cmd.Parameters.AddWithValue("@idVenta", v.IdVenta);
            db.cmd.Parameters.AddWithValue("@estado", v.Estado);
            db.cmd.Parameters.AddWithValue("@idProducto", item.IdProducto);

            try
            {
                if (tipo) db.conn.Open();
                db.cmd.ExecuteNonQuery();
                if (tipo) db.conn.Close();
                db.cmd.Parameters.Clear();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public List<DetalleVenta> BuscarTodos(int idVenta = 0)
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();

            if (idVenta == 0)
            {
                db.cmd.CommandText = "select * from DetalleVenta ";
            }
            else
            {
                db.cmd.CommandText = "select * from DetalleVenta where idVenta=@idVenta";
                db.cmd.Parameters.AddWithValue("@idVenta", idVenta);
            }

            try
            {
                if (tipo) db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                while (reader.Read())
                {
                    DetalleVenta d = new DetalleVenta();

                    d.IdProducto = Convert.ToInt32(reader["idProducto"].ToString());
                    d.IdDetalleV = Convert.ToInt32(reader["idVenta"].ToString());
                    d.Descuento = Convert.ToDouble(reader["descuento"].ToString());
                    d.Cantidad = Convert.ToInt32(reader["cantidad"].ToString());

                    lista.Add(d);
                }
                reader.Close();
                db.cmd.Parameters.Clear();
                if (tipo) db.conn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
            return lista;
        
        }
    }
}
