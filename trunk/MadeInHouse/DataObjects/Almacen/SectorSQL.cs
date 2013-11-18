using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.DataObjects.Almacen
{
    class SectorSQL
    {
        private DBConexion db;
        private bool tipo = true;

        public SectorSQL(DBConexion db = null)
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


        public List<Sector> ObtenerSectores (int idAlmacen = -1, int idTipoZona = -1)
        {
            List<Sector> lstSectores = null;
            db.cmd.CommandText = "SELECT A.* FROM Sector S" +
                                 " WHERE A.idAlmacen=@idAlmacen and A.idTipoZona=@idTipoZona";
            db.cmd.Parameters.AddWithValue("@idAlmacen", idAlmacen);
            db.cmd.Parameters.AddWithValue("@idTipoZona", idTipoZona);

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                lstSectores = new List<Sector>();
                while (reader.Read())
                {
                    Sector s = new Sector();
                    s.IdSector= int.Parse(reader["idSector"].ToString());
                    s.IdAlmacen = idAlmacen;
                    s.IdTipoZona = idTipoZona;
                    s.IdProducto = reader.IsDBNull(reader.GetOrdinal("idProducto")) ? 0 : int.Parse(reader["idProducto"].ToString());
                    s.Cantidad = reader.IsDBNull(reader.GetOrdinal("cantidad")) ? 0 : int.Parse(reader["cantidad"].ToString());
                    s.VolOcupado = reader.IsDBNull(reader.GetOrdinal("volOcupado")) ? 0: int.Parse(reader["volOcupado"].ToString());
                    s.NroColor = reader.IsDBNull(reader.GetOrdinal("nroColor")) ? 0 : int.Parse(reader["nroColor"].ToString());
                    lstSectores.Add(s);

                }

                reader.Close();
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


            return lstSectores;
        }




    }
}
