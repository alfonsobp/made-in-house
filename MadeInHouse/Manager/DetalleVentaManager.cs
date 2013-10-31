﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MadeInHouse.Model;
using System.Windows;

namespace MadeInHouse.Manager
{
    class DetalleVentaManager : EntityManager
    {

        public int Agregar(object entity)
        {
            throw new NotImplementedException();
        }

        public object Buscar(params object[] filters)
        {
            Producto prod = new Producto();
            DBConexion db = new DBConexion();

            db.cmd.CommandText = "select * from Producto where idProducto=" + Convert.ToInt32(filters[0]);

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                while (reader.Read())
                {
                    prod.IdProducto = Convert.ToInt32(reader["idProducto"].ToString());
                    prod.CodProducto = reader["codProducto"].ToString();
                    prod.Nombre = reader["nombre"].ToString();
                    prod.Precio = 1;
                    //prod.Precio = Convert.ToDouble(reader["precio"].ToString());
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

        public int Actualizar(object entity)
        {
            throw new NotImplementedException();
        }

        public int Eliminar(object entity)
        {
            throw new NotImplementedException();
        }

        public void Agregar(Model.Ventas.Venta v, Model.Ventas.DetalleVenta dv)
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
