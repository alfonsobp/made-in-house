using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;
using System.Data;

namespace MadeInHouse.DataObjects.Almacen
{
    class TiendaSQL
    {

        private DBConexion db;

        public TiendaSQL()
        {
            db = new DBConexion();
        }


        public int AgregarTienda(Tienda p)
        {
            db.cmd.CommandText = "sp_AgregarTienda";
            db.cmd.CommandType = CommandType.StoredProcedure;
            db.cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            db.cmd.Parameters.AddWithValue("@direccion", p.Direccion);
            db.cmd.Parameters.AddWithValue("@admin", p.Administrador);
            db.cmd.Parameters.AddWithValue("@telefono", p.Telefono);
            db.cmd.Parameters.AddWithValue("@idUbigeo", p.IdUbigeo);
            db.cmd.Parameters.Add("@idTienda", SqlDbType.Int).Direction = ParameterDirection.Output;

            int idtienda=-1;

            try
            {
                db.conn.Open();


                db.cmd.ExecuteNonQuery();
                idtienda = Convert.ToInt32(db.cmd.Parameters["@idTienda"].Value);
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

            return idtienda;

        }

    }
}
