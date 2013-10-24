using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.DataObjects.Almacen
{
    class TiendaSQL
    {

        private DBConexion db;

        public TiendaSQL()
        {
            db = new DBConexion();
        }


        public void AgregarTienda(Tienda p)
        {
            db.cmd.CommandText = "INSERT INTO Tienda(idTienda,nombre,direccion,ubigeo,fechaReg,estado) " +
            "VALUES (null,@nombre,@direccion,@ubigeo,@fechaReg,@estado)";

            db.cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            db.cmd.Parameters.AddWithValue("@direccion",p.Direccion);
            db.cmd.Parameters.AddWithValue("@ubigeo",p.Ubigeo.IdUbigeo);
            
            db.cmd.Parameters.AddWithValue("@fechaReg", p.FechaReg.Date);

/*          db.cmd.Parameters.AddWithValue("@tipoUso", p.TipoUso);
            db.cmd.Parameters.AddWithValue("@abreviatura", p.Abreviatura);
            db.cmd.Parameters.AddWithValue("@observaciones", p.Observaciones);
            db.cmd.Parameters.AddWithValue("@idSubLinea", p.IdSubLinea);
            db.cmd.Parameters.AddWithValue("@idLinea", p.IdLinea);
            db.cmd.Parameters.AddWithValue("@estado", p.Estado);*/

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
