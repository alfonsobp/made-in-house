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

         public List<Ubicacion> ObtenerUbicaciones(int idAlmacen=-1,int idTipoZona=-1)
        {
            List<Ubicacion> lstUbicaciones=null;
            db.cmd.CommandText = "SELECT A.* FROM Ubicacion A" +
                " WHERE A.idAlmacen=@idAlmacen and A.idTipoZona=@idTipoZona";
            db.cmd.Parameters.AddWithValue("@idAlmacen", idAlmacen);
            db.cmd.Parameters.AddWithValue("@idTipoZona", idTipoZona);
            
             try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                lstUbicaciones = new List<Ubicacion>();
                while (reader.Read())
                {
                    Ubicacion u = new Ubicacion();
                    u.IdUbicacion = int.Parse(reader["idUbicacion"].ToString());
                    u.IdAlmacen = idAlmacen;
                    u.IdTipoZona = idTipoZona;
                    u.IdProducto = reader.IsDBNull(reader.GetOrdinal("idProducto"))? -1:int.Parse(reader["idProducto"].ToString());
                    u.CordX = int.Parse(reader["cordX"].ToString());
                    u.CordY=int.Parse(reader["cordY"].ToString());
                    u.CordZ = int.Parse(reader["cordZ"].ToString());
                    u.Cantidad = reader.IsDBNull(reader.GetOrdinal("cantidad"))? -1:int.Parse(reader["cantidad"].ToString());
                    u.VolOcupado = reader.IsDBNull(reader.GetOrdinal("volOcupado")) ? -1:int.Parse(reader["volOcupado"].ToString());
                    lstUbicaciones.Add(u);

                }

                reader.Close();
                db.conn.Close();
                db.cmd.Parameters.Clear();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e) {
                Console.WriteLine(e.StackTrace.ToString());
            }


            return lstUbicaciones;
        }


    }
}
