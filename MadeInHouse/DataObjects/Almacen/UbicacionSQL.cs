using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;
using System.Data.SqlClient;

namespace MadeInHouse.DataObjects.Almacen
{
    class UbicacionSQL
    {
        private DBConexion db;

        public UbicacionSQL()
        {
            db = new DBConexion();
        }

        public void Agregar(Ubicacion u)
        {
            db.cmd.CommandText = "INSERT INTO Ubicacion (idTipoZona,idAlmacen,cordX,cordY,cordZ) VALUES (@idTipoZona,@idAlmacen,@cordX,@cordY,@cordZ)";
            db.cmd.Parameters.AddWithValue("@idTipoZona", u.IdTipoZona);
            db.cmd.Parameters.AddWithValue("@idAlmacen", u.IdAlmacen);
            db.cmd.Parameters.AddWithValue("@cordX", u.CordX);
            db.cmd.Parameters.AddWithValue("@cordY", u.CordY);
            db.cmd.Parameters.AddWithValue("@cordZ", u.CordZ);

            try
            {
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.cmd.Parameters.Clear();
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
