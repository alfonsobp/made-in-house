using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MadeInHouse.DataObjects.Almacen
{
    class AlmacenSQL
    {
        private DBConexion db;

        public AlmacenSQL()
        {
            db = new DBConexion();
        }

        internal Models.Almacen.Almacen BuscarAlmacen(int idAlmacen)
        {
            

            Models.Almacen.Almacen almacen = new Models.Almacen.Almacen();

            string where = "WHERE 1=1 ";

            where = where + " AND idAlmacen=@idAlmacen";

            db.cmd.CommandText = "SELECT * FROM Almacen " + where;

            db.cmd.Parameters.AddWithValue("@idAlmacen",idAlmacen);
            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {

                    almacen.Altura = reader.IsDBNull(reader.GetOrdinal("altura")) ? -1 : int.Parse(reader["altura"].ToString());
                    almacen.CodAlmacen = reader.IsDBNull(reader.GetOrdinal("codAlmacen")) ? null : reader["codAlmacen"].ToString();
                    almacen.Estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? -1 : int.Parse(reader["estado"].ToString());

                    almacen.FechaReg = reader.IsDBNull(reader.GetOrdinal("fechaReg")) ? DateTime.MinValue : DateTime.Parse(reader["fechaReg"].ToString());
                    almacen.IdAlmacen = reader.IsDBNull(reader.GetOrdinal("idAlmacen")) ? -1 : int.Parse(reader["idAlmacen"].ToString());
                    almacen.IdTienda = reader.IsDBNull(reader.GetOrdinal("idTienda")) ? -1 : int.Parse(reader["idTienda"].ToString());
                    almacen.Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader["nombre"].ToString();
                    almacen.NroColumnas = reader.IsDBNull(reader.GetOrdinal("nroColumnas")) ? -1 : int.Parse(reader["nroColumnas"].ToString());
                    almacen.NroFilas = reader.IsDBNull(reader.GetOrdinal("nroFilas")) ? -1 : int.Parse(reader["nroFilas"].ToString());
                    almacen.Tipo = reader.IsDBNull(reader.GetOrdinal("tipo")) ? null : reader["tipo"].ToString();
                    
                }
                db.cmd.Parameters.Clear();
                db.conn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
            }

            return almacen;

        }
    }
}
