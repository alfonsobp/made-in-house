

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;
using System.Data;
using System.Data.SqlClient;


namespace MadeInHouse.DataObjects.Almacen
{
    class AlmacenSQL
    {
        private DBConexion db;

        public AlmacenSQL()
        {
            db = new DBConexion();
        }


        public void AgregarZonas(int idTipoZona,int idAlmacen) 
        {
            db.cmd.CommandType = CommandType.Text;
            db.cmd.CommandText = "INSERT INTO ZonaxAlmacen (idTipoZona,idAlmacen) VALUES (@idTipoZona,@idAlmacen)" ;
            db.cmd.Parameters.AddWithValue("@idTipoZona", idTipoZona);
            db.cmd.Parameters.AddWithValue("@idAlmacen", idAlmacen);

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


        public int Agregar(Almacenes alm)
        {
            db.cmd.CommandText = "sp_AgregarAlmacen";
            db.cmd.CommandType = CommandType.StoredProcedure;
            db.cmd.Parameters.AddWithValue("@nombre", alm.Nombre);
            db.cmd.Parameters.AddWithValue("@codAlmacen",alm.CodAlmacen);
            db.cmd.Parameters.AddWithValue("@direccion", alm.Direccion);
            db.cmd.Parameters.AddWithValue("@tipo", alm.Tipo);
            db.cmd.Parameters.AddWithValue("@idTienda", alm.IdTienda);
            db.cmd.Parameters.AddWithValue("@telefono", alm.Telefono);

            db.cmd.Parameters.Add("@idAlmacen", SqlDbType.Int).Direction = ParameterDirection.Output;

            int idalmacen = -1;

            try
            {
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
                idalmacen = Convert.ToInt32(db.cmd.Parameters["@idAlmacen"].Value);
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

            return idalmacen;

        }

        internal Models.Almacen.Almacenes BuscarAlmacen(int idAlmacen)
        {


            Models.Almacen.Almacenes almacen = new Models.Almacen.Almacenes();

            string where = "WHERE 1=1 ";

            where = where + " AND idAlmacen=@idAlmacen";

            db.cmd.CommandText = "SELECT * FROM Almacen " + where;

            db.cmd.Parameters.AddWithValue("@idAlmacen", idAlmacen);
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
                    almacen.Tipo = reader.IsDBNull(reader.GetOrdinal("tipo")) ? -1: int.Parse(reader["tipo"].ToString());

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