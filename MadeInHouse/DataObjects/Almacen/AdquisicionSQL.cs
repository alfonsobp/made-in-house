using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MadeInHouse.DataObjects.Almacen
{
    class AdquisicionSQL
    {
        private DBConexion db;

        public AdquisicionSQL(DBConexion db = null)
        {
            this.db = (db == null) ? new DBConexion() : db;
        }

        public int insertarAdquisicion(int idTienda)
        {
            int idSolicitud = -1;
            db.cmd.CommandText = " INSERT INTO SolicitudAdquisicion(estado , fechaReg , idTienda, idUsuario) " +
                                 " output INSERTED.idSolicitudAD " +
                                 " VALUES (1, GETDATE(), @idTienda, @idUsuario) ";
            db.cmd.Parameters.AddWithValue("@idTienda", idTienda);
            db.cmd.Parameters.AddWithValue("@idUsuario", Thread.CurrentPrincipal.Identity.Name);

            if (db.cmd.Transaction == null) db.conn.Open();

            idSolicitud = (int)db.cmd.ExecuteScalar();

            if (db.cmd.Transaction == null) db.conn.Close();
            db.cmd.Parameters.Clear();

            return idSolicitud;
        }
    }
}
