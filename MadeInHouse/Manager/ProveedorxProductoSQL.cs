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

namespace MadeInHouse.Manager
{
    class ProveedorxProductoSQL:EntityManager
    {

        public int Agregar(object entity)
        {
            throw new NotImplementedException();
        }

        public object Buscar(params object[] filters)
        {
            string where = "";
            int codProveedor;

            if (filters.Length != 0)
            {

                 codProveedor = Convert.ToInt32(filters[0]);
                where += "and  idProveedor = " + codProveedor;
            
            }

           
            DBConexion DB = new DBConexion();

            SqlConnection conn = DB.conn;
            SqlCommand cmd = DB.cmd;
            SqlDataReader reader;


            cmd.CommandText = "SELECT * FROM ProveedorxProducto WHERE  estado = 1   " + where + " ORDER BY fechaReg DESC ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            List<ProveedorxProducto> lstProductos = new List<ProveedorxProducto>();

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    ProveedorxProducto p = new ProveedorxProducto();
                    p.Estado = Convert.ToInt32(reader["estado"].ToString());
                    p.Producto = new ProductoSQL().Buscar_por_CodigoProducto(Convert.ToInt32(reader["idProducto"].ToString() ));
                    p.Precio = Convert.ToDouble(reader["precio"].ToString());
                    p.IdProveedor = Convert.ToInt32(reader["idProveedor"].ToString() );
                    p.FechaAct = Convert.ToDateTime(reader["fechaAct"].ToString());
                    p.FechaReg = Convert.ToDateTime(reader["fechaReg"].ToString());
                    p.CodComercial = reader["codComercial"].ToString();
                    p.Descripcion = reader["descripcion"].ToString();
                    

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

            throw new NotImplementedException();
        }

        public int Eliminar(object entity)
        {
            ProveedorxProducto pp = entity as ProveedorxProducto;
            int k = 0;

          

                DBConexion DB = new DBConexion();

                SqlConnection conn = DB.conn;
                SqlCommand cmd = DB.cmd;
              

                cmd.CommandText = "UPDATE ProveedorxProducto set estado = 0 " +
                                    " where idProveedor = @idProveedor and idProducto = @idProducto ";

                cmd.Parameters.AddWithValue("@idProveedor", pp.IdProveedor);
                cmd.Parameters.AddWithValue("@idProducto", pp.Producto.IdProducto);

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

        public int Insertar(ProveedorxProducto pp) {

            Producto p = new ProductoSQL().Buscar_por_CodigoProducto(pp.Producto.CodigoProd);
            int k = 0 ;

            if (p != null)
            {

                DBConexion DB = new DBConexion();

                SqlConnection conn = DB.conn;
                SqlCommand cmd = DB.cmd;
                SqlDataReader reader;

               
                cmd.CommandText =  "IF NOT EXISTS(SELECT 1 from ProveedorxProducto where idProveedor = @idProveedor and idProducto = @idProducto  )"+
                                   "Insert into ProveedorxProducto(idProducto,idProveedor,codComercial,precio, estado,descripcion,fechaReg,fechaAct) "+
                                   "VALUES (@idProducto,@idProveedor,@codComercial,@precio,@estado,@descripcion,GETDATE(),GETDATE() )"+
                                    " else " +
                                    "UPDATE ProveedorxProducto set fechaAct = GETDATE() , precio = @precio , descripcion = @descripcion ,codComercial  = @codComercial "+
                                    " , estado = @estado where idProveedor = @idProveedor and idProducto = @idProducto ";

                cmd.Parameters.AddWithValue("@idProveedor", pp.IdProveedor);
                cmd.Parameters.AddWithValue("@idProducto", p.IdProducto);
                cmd.Parameters.AddWithValue("@codComercial", pp.CodComercial);
                cmd.Parameters.AddWithValue("@precio", pp.Precio);
                cmd.Parameters.AddWithValue("@estado", 1);
                cmd.Parameters.AddWithValue("@fechaReg", DateTime.Now);
                cmd.Parameters.AddWithValue("@fechaAct", DateTime.Now);
                cmd.Parameters.AddWithValue("@descripcion", pp.Descripcion);

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                


                try
                {
                    conn.Open();


                   k = cmd.ExecuteNonQuery();


                    reader = cmd.ExecuteReader();


                    if (reader.Read())
                    {

                        p = new Producto();
                        p.IdProducto = Convert.ToInt32(reader["idProducto"].ToString());
                        p.Nombre = reader["nombre"].ToString();
                        p.CodigoProd = reader["codProducto"].ToString();
                    }

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
