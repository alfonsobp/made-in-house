using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;
using System.Data.SqlClient;
using System.Data;

namespace MadeInHouse.DataObjects.Almacen
{
    class SubLineaProductoSQL
    {
        private DBConexion db = null;

        public SubLineaProductoSQL()
        {
            db = new DBConexion();
        }

        public void AgregarSubLineaProducto(SubLineaProducto slp,int idLinea)
        {
            
            db.cmd.CommandText = "INSERT INTO SubLineaProducto (nombre,idLinea,abreviatura) values (@nombre,@idLinea,@abreviatura)";
            db.cmd.Parameters.AddWithValue("@nombre", slp.Nombre);
            db.cmd.Parameters.AddWithValue("@idLinea", idLinea);
            db.cmd.Parameters.AddWithValue("@Abreviatura", slp.Abreviatura);

            try
            {   
                
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.conn.Close();
                db.cmd.Parameters.Clear();

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
