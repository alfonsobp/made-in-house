using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Model;
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
    class ServicioxProductoSQL:EntitySQL
    {
        public int Agregar(object entity)
        {
            throw new NotImplementedException();
        }

        public object Buscar(params object[] filters)
        {
            string where = "";
            int codServicio;

            if (filters.Length != 0)
            {

                codServicio = Convert.ToInt32(filters[0]);
                where += "idServicio = " + codServicio;

            }


            DBConexion DB = new DBConexion();

            SqlConnection conn = DB.conn;
            SqlCommand cmd = DB.cmd;
            SqlDataReader reader;


            cmd.CommandText = "SELECT * FROM ServicioxProducto WHERE " + where;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            List<ServicioxProducto> lstServicios = new List<ServicioxProducto>();

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    ServicioxProducto p = new ServicioxProducto();
                    p.IdServicio = Convert.ToInt32(reader["idServicio"].ToString());
                    //p.Producto = new ProductoSQL().Buscar_por_CodigoProducto(Convert.ToInt32(reader["idProducto"].ToString()));
                    p.Precio = Convert.ToDouble(reader["precio"].ToString());


                    lstServicios.Add(p);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return lstServicios;
        }

        public int Actualizar(object entity)
        {
            throw new NotImplementedException();
        }

        public int Eliminar(object entity)
        {
            throw new NotImplementedException();
        }

        public int InsertarValidado(ServicioxProducto sp)
        {

            //Producto p = new ProductoSQL().Buscar_por_CodigoProducto(sp.Producto.CodigoProd);
            MadeInHouse.Model.Producto p = null;
            //MessageBox.Show("IdServicio = " + sp.IdServicio + " IdProducto = " + p.IdProducto + " Precio = " + sp.Precio);
            int k = 0;
            

            if (p != null)
            {

                DBConexion DB = new DBConexion();

                SqlConnection conn = DB.conn;
                SqlCommand cmd = DB.cmd;


                cmd.CommandText = "IF NOT EXISTS(SELECT 1 from ServicioxProducto where idServicio = @idServicio and idProducto = @idProducto) " +
                                   "Insert into ServicioxProducto(idServicio, idProducto, precio) " +
                                   "VALUES (@idServicio,@idProducto,@precio) " +
                                    " else " +
                                    "UPDATE ServicioxProducto set precio = @precio " +
                                    "where idServicio = @idServicio and idProducto = @idProducto ";

                cmd.Parameters.AddWithValue("@idServicio", sp.IdServicio);
                cmd.Parameters.AddWithValue("@idProducto", p.IdProducto);
                cmd.Parameters.AddWithValue("@precio", sp.Precio);
                

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;




                try
                {
                    conn.Open();


                    k = cmd.ExecuteNonQuery();


                    conn.Close();

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.StackTrace.ToString());
                }

            }
            return k;

        }

    }
}
