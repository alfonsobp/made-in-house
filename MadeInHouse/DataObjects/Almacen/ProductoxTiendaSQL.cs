using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.DataObjects.Almacen
{
    class ProductoxTiendaSQL
    {
        private DBConexion db;
        private bool tipo = true;

        public ProductoxTiendaSQL(DBConexion db = null)
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


        public void ActualizarStockSalida(List<Models.Almacen.ProductoCant> list, int idTienda)
        {

            db.cmd.CommandText = "UPDATE ProductoxTienda SET  stockActual=stockActual-@cantidad WHERE idproducto=@idProducto AND idTienda=@idTienda;";
            try
            {
                db.conn.Open();
                for (int i = 0; i < list.Count; i++)
                {
                    db.cmd.Parameters.AddWithValue("@cantidad",list.ElementAt(i).CanAtender);
                    db.cmd.Parameters.AddWithValue("@idTienda", idTienda);
                    db.cmd.Parameters.AddWithValue("@idProducto", list.ElementAt(i).IdProducto);
                    db.cmd.ExecuteNonQuery();
                    db.cmd.Parameters.Clear();

                }
                db.conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

        }


        public void ActualizarStockEntrada(List<Models.Almacen.ProductoCant> list, int idTienda)
        {

            db.cmd.CommandText = "UPDATE ProductoxTienda SET  stockActual=stockActual+@cantidad WHERE idproducto=@idProducto AND idTienda=@idTienda;";
            try
            {
                db.conn.Open();
                for (int i = 0; i < list.Count; i++)
                {
                    db.cmd.Parameters.AddWithValue("@cantidad", list.ElementAt(i).CanAtender);
                    db.cmd.Parameters.AddWithValue("@idTienda", idTienda);
                    db.cmd.Parameters.AddWithValue("@idProducto", list.ElementAt(i).IdProducto);
                    db.cmd.ExecuteNonQuery();
                    db.cmd.Parameters.Clear();

                }
                db.conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

        }

    }
}
