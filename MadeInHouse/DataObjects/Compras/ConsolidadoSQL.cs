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
    class ConsolidadoSQL:EntitySQL
    {

        public List<int> getSolicitudes() {

           

            DBConexion DB = new DBConexion();

            SqlConnection conn = DB.conn;
            SqlCommand cmd = DB.cmd;
            SqlDataReader reader;


            cmd.CommandText = "select idSolicitudAD from SolicitudAdquisicion where estado = 2 ";

            

            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            List<int> lst = new List<int>();

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    int p = Convert.ToInt32(reader["idSolicitudAD"]);
                    lst.Add(p);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return lst;

        
        }  

        public int Agregar(object entity)
        {
            throw new NotImplementedException();
        }


        public object Buscar(params object[] filters)
        {

          

            DBConexion DB = new DBConexion();

            SqlConnection conn = DB.conn;
            SqlCommand cmd = DB.cmd;
            SqlDataReader reader;


            cmd.CommandText = "select ps.idProducto as idProducto, sum(ps.cantidadAtendida) as suma from " +
                              " ProductoxSolicitudAd ps , SolicitudAdquisicion p where " +
                              "ps.idSolicitudAD = p.idSolicitudAD and p.estado = 2 " +
                            
                              " group by ps.idProducto; ";

           

            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            List<Consolidado> lst = new List<Consolidado>();

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    Consolidado p = new Consolidado();
                    p.Cantidad = Convert.ToInt32(reader["suma"].ToString());
                    p.Producto = new ProductoSQL().Buscar_por_CodigoProducto(Convert.ToInt32(reader["idProducto"].ToString()));
                    
                    lst.Add(p);
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
            throw new NotImplementedException();
        }

       public  List<PrecioProductoProveedor> BuscarProveedores(int idProducto) {


            
            DBConexion DB = new DBConexion();

            SqlConnection conn = DB.conn;
            SqlCommand cmd = DB.cmd;
            SqlDataReader reader;


            cmd.CommandText = "select  pp.idProveedor as idProveedor , pp.precio  as precio "+
                              "from ProveedorxProducto pp ,Proveedor p "+
                              "where pp.idProveedor = p.idProveedor  and p.estado = 1 and pp.idProducto=@idProducto" ;

            cmd.Parameters.AddWithValue("@idProducto", idProducto);

            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            List<PrecioProductoProveedor> lst = new List<PrecioProductoProveedor>();

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    PrecioProductoProveedor p = new PrecioProductoProveedor();
                    p.Costo = Convert.ToDouble(reader["precio"].ToString());
                    p.Prov =   new ProveedorSQL().BuscarPorCodigo(Convert.ToInt32(reader["idProveedor"]));
                    
                    lst.Add(p);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return lst;

        
        }

    }
}
