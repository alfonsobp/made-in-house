using MadeInHouse.Models.Almacen;
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

        public int insertarProductosAdquisicion(int idSolicitud, List<AbastecimientoProducto> prod)
        {
            int result = 0;
            string values = "";
            if (prod != null && prod.Count > 0)
            {
                foreach (AbastecimientoProducto item in prod)
                {
                    if (!String.IsNullOrEmpty(values)) values += " , ";
                    values += " (@idSolicitudAD, " + item.idProducto + " , " + (item.pedido - item.atendido) + " , 0 ) ";
                }

                db.cmd.CommandText = " INSERT INTO ProductoxSolicitudAd (idSolicitudAD , idProducto , cantidad , cantidadAtendida) " +
                                     " VALUES " + values;
                db.cmd.Parameters.AddWithValue("@idSolicitudAD", idSolicitud);

                if (db.cmd.Transaction == null) db.conn.Open();

                result = db.cmd.ExecuteNonQuery();

                if (db.cmd.Transaction == null) db.conn.Close();
                db.cmd.Parameters.Clear();
            }

            return result;
        }
    }
}
