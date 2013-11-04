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
        private bool tipo=true;

        public TiendaSQL(DBConexion db=null)
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


        public int AgregarTienda(Tienda p)
        {
            //db.cmd.CommandText = "sp_AgregarTienda";
            //db.cmd.CommandType = CommandType.StoredProcedure;
            db.cmd.CommandText = "INSERT INTO Tienda (nombre,direccion,administrador,telefono,idUbigeo) " +
                                "output INSERTED.idTienda "+
                                " VALUES (@nombre,@direccion,@admin,@telefono,@idUbigeo)";
            db.cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            db.cmd.Parameters.AddWithValue("@direccion", p.Direccion);
            db.cmd.Parameters.AddWithValue("@admin", p.Administrador);
            db.cmd.Parameters.AddWithValue("@telefono", p.Telefono);
            db.cmd.Parameters.AddWithValue("@idUbigeo", p.IdUbigeo);
            //db.cmd.Parameters.Add("@idTienda", SqlDbType.Int).Direction = ParameterDirection.Output;

            int idtienda=-1;

            try
            {
                if (tipo) db.conn.Open();


                idtienda= (int) db.cmd.ExecuteScalar();
               // idtienda = Convert.ToInt32(db.cmd.Parameters["@idTienda"].Value);
                db.cmd.Parameters.Clear();
                if (tipo) db.conn.Close();

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

            return idtienda;

        }

    }
}
