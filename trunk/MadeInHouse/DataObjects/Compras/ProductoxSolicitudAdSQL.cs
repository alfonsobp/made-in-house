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
    class ProductoxSolicitudAdSQL:EntitySQL
    {
        public int Agregar(object entity)
        {
            throw new NotImplementedException();
        }

        public object Buscar(params object[] filters)
        {
          int id =  Convert.ToInt32( filters[0]);


          DBConexion DB = new DBConexion();

          SqlConnection conn = DB.conn;
          SqlCommand cmd = DB.cmd;
          SqlDataReader reader;


          cmd.CommandText = "SELECT * FROM  ProductoxSolicitudAd where idSolicitudAD = "+id ;
          cmd.CommandType = CommandType.Text;
          cmd.Connection = conn;
          List<ProductoxSolicitudAd> lstProductos = new List<ProductoxSolicitudAd>();

          try
          {
              conn.Open();

              reader = cmd.ExecuteReader();


              while (reader.Read())
              {

                  ProductoxSolicitudAd p = new ProductoxSolicitudAd();

                  p.Producto = new ProductoSQL().Buscar_por_CodigoProducto(Convert.ToInt32(reader["idProducto"].ToString()));
                  p.IdSolicitudAD = Convert.ToInt32(reader["idSolicitudAD"].ToString());
                  p.Cantidad = Convert.ToInt32(reader["cantidad"].ToString());                             
                  p.CantidadAtendida =  reader.IsDBNull(reader.GetOrdinal("CantidadAtendida")) ? p.Cantidad:Convert.ToInt32(reader["cantidadAtendida"]);
                  lstProductos.Add(p);
              }

              conn.Close();

          }
          catch (Exception e)
          {
              MessageBox.Show(e.StackTrace.ToString());
          }

          return lstProductos;

        }

        public int Actualizar(object entity)
        {
            int k = 0;

            ProductoxSolicitudAd pp = entity as ProductoxSolicitudAd;
           



            DBConexion DB = new DBConexion();

            SqlConnection conn = DB.conn;
            SqlCommand cmd = DB.cmd;


            cmd.CommandText = "UPDATE ProductoxSolicitudAd set cantidadAtendida = @cantidadAtendida  " +
                                " where idSolicitudAD = @idSolicitudAD and  idProducto = @idProducto ";

            MessageBox.Show(" " + pp.IdSolicitudAD + " " + pp.Producto.IdProducto + " " + pp.CantidadAtendida);
            cmd.Parameters.AddWithValue("@idSolicitudAD", pp.IdSolicitudAD);
            cmd.Parameters.AddWithValue("@idProducto", pp.Producto.IdProducto);
            cmd.Parameters.AddWithValue("@cantidadAtendida", pp.CantidadAtendida);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;




            try
            {
                conn.Open();


                k = cmd.ExecuteNonQuery();


                cmd.ExecuteNonQuery();

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }


            return k;

        }

        public int Eliminar(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
