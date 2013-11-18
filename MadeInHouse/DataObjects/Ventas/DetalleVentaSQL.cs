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

        public Producto Buscar(string idProducto)
        {
            Producto prod = new Producto();

            db.cmd.CommandText = "select * from ProductoxTienda where idProducto=" + Convert.ToInt32(idProducto);

            try
            {
                if (tipo) db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                while (reader.Read())
                {
                    prod.IdProducto = Convert.ToInt32(reader["idProducto"].ToString());
                    prod.CodigoProd = reader["codProducto"].ToString();
                    prod.Nombre = reader["nombre"].ToString();
                    prod.Precio = Double.Parse(reader["precio"].ToString());
                }
                db.cmd.Parameters.Clear();
                if (tipo) db.conn.Close();
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
            db.cmd.CommandText = "INSERT INTO DetalleVenta(cantidad,descuento,idVenta,idProducto) VALUES(@cantidad,@descuento,@idVenta,@idProducto)";
            db.cmd.Parameters.AddWithValue("@cantidad", dv.Cantidad);
            db.cmd.Parameters.AddWithValue("@descuento", dv.Descuento);
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
                MessageBox.Show(e.StackTrace.ToString());
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
                MessageBox.Show(e.StackTrace.ToString());
            }
        }

        public List<DetalleVenta> BuscarTodos()
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();

            db.cmd.CommandText = "select * from DetalleVenta ";

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
                db.cmd.Parameters.Clear();
                if (tipo) db.conn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }
            return lista;
        
        }
    }
}
