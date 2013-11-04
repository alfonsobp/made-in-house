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
        private bool tipo=true;

        public UbicacionSQL(DBConexion db=null)
        {

            if (db == null)
            {
                this.db = new DBConexion();
            }
            else {
                this.db=db;
                tipo=false;
            }
            
        }
        public int Agregar(Ubicacion u)
        {
            db.cmd.CommandText = "INSERT INTO Ubicacion (idTipoZona,idAlmacen,cordX,cordY,cordZ) VALUES (@idTipoZona,@idAlmacen,@cordX,@cordY,@cordZ)";
            db.cmd.Parameters.AddWithValue("@idTipoZona", u.IdTipoZona);
            db.cmd.Parameters.AddWithValue("@idAlmacen", u.IdAlmacen);
            db.cmd.Parameters.AddWithValue("@cordX", u.CordX);
            db.cmd.Parameters.AddWithValue("@cordY", u.CordY);
            db.cmd.Parameters.AddWithValue("@cordZ", u.CordZ);

            try
            {
                if (tipo) db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.cmd.Parameters.Clear();
                if(tipo) db.conn.Close();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return -1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
                return -1;
            }

            return 1;
        }



    }
}
