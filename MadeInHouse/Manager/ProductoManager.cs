using MadeInHouse.Model;
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
    class ProductoManager:EntityManager
    {

        public int Agregar(object entity)
        {
            throw new NotImplementedException();
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

        public Producto  Buscar_por_CodigoProducto(string codProducto) {

            DBConexion DB = new DBConexion();

            SqlConnection conn = DB.conn;
            SqlCommand cmd = DB.cmd;
             SqlDataReader reader;

             Producto p = null;

            cmd.CommandText = "SELECT * from Producto where codProducto = @codProducto and estado = 1 ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@codProducto",codProducto);
            

            try
            {
                conn.Open();

     

            
           
                     reader = cmd.ExecuteReader();


                if (reader.Read())
                {

                    p = new Producto();
                    p.IdProducto = Convert.ToInt32(reader["idProducto"].ToString());
                    p.Nombre = reader["nombre"].ToString();
                    p.CodProducto = reader["codProducto"].ToString();
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }


            return p;
        
        }

        public Producto Buscar_por_CodigoProducto(int IdProducto)
        {

            DBConexion DB = new DBConexion();

            SqlConnection conn = DB.conn;
            SqlCommand cmd = DB.cmd;
            SqlDataReader reader;

            Producto p = null;

            cmd.CommandText = "SELECT * from Producto where idProducto = @idProducto and estado = 1 ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@idProducto", IdProducto);


            try
            {
                conn.Open();





                reader = cmd.ExecuteReader();


                if (reader.Read())
                {

                    p = new Producto();
                    p.IdProducto = Convert.ToInt32(reader["idProducto"].ToString());
                    p.Nombre = reader["nombre"].ToString();
                    p.CodProducto = reader["codProducto"].ToString();
                }

                conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }


            return p;

        }
    }
}
