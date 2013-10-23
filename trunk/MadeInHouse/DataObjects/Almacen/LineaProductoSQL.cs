using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;
using System.Data.SqlClient;

namespace MadeInHouse.DataObjects.Almacen
{
    class LineaProductoSQL
    {

        private DBConexion db = null;
        public LineaProductoSQL()
        {
            db = new DBConexion();
        }

        public void AgregarLineaProducto(LineaProducto lp)
        {

            db.cmd.CommandText = "INSERT INTO LineaProducto (nombre,abreviatura) values (@nombre,@abreviatura)";
            db.cmd.Parameters.AddWithValue("@nombre", lp.Nombre);
            db.cmd.Parameters.AddWithValue("@abreviatura", lp.Abreviatura);

            try
            {
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
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


        public void ActualizarLineaProducto(LineaProducto lp)
        {
            db.cmd.CommandText = "UPDATE LineaProducto SET nombre= @nombre , abreviatura=@abreviatura WHERE idLinea=@idLinea";
            db.cmd.Parameters.AddWithValue("@idLinea", lp.IdLinea);
            db.cmd.Parameters.AddWithValue("@nombre", lp.Nombre);
            db.cmd.Parameters.AddWithValue("@abreviatura", lp.Abreviatura);
            try
            {
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
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
