using MadeInHouse.Models.Almacen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.DataObjects.Almacen
{
    class AbastecimientoSQL
    {
        private DBConexion db;

        public AbastecimientoSQL(DBConexion db = null)
        {
            this.db = (db == null) ? new DBConexion() : db;
        }

        public int insertarAbastecimiento(int idDeposito)
        {
            int idSolicitud = -1;
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = db.conn;
            //cmd.Transaction = db.trans;
            db.cmd.CommandText = " INSERT INTO SolicitudAbastecimiento(estado,fechaReg,fechaAtencion,idOrden,idDeposito) " +
                                 " output INSERTED.idSolicitudAB " +
                                 " VALUES (1, GETDATE(), NULL, NULL, @idDeposito) ";
            db.cmd.Parameters.AddWithValue("@idDeposito", idDeposito);

            try
            {
                idSolicitud = (int) db.cmd.ExecuteScalar();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                Console.WriteLine(e.StackTrace.ToString());
            }

            return idSolicitud;
        }

        public bool insertarProductosAbastecimiento(int idSolicitud, List<AbastecimientoProducto> prod)
        {
            int result = 0;
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = db.conn;
            //cmd.Transaction = db.trans;
            string values = "";
            if (prod != null && prod.Count > 0)
            {
                foreach (AbastecimientoProducto item in prod)
                {
                    if (!String.IsNullOrEmpty(values)) values += " , ";
                    values += " (@idSolicitudAB, " + item.idProducto + " , " + item.pedido + " , " + item.atendido + " ) ";
                }

                db.cmd.CommandText = " INSERT INTO ProductoxSolicitudAb (idSolicitudAB , idProducto , cantidad , cantidadAtendida) " +
                                     " VALUES " + values;
                db.cmd.Parameters.AddWithValue("@idSolicitudAB", idSolicitud);

                result = db.cmd.ExecuteNonQuery();
            }

            return (result > 0) ? true : false;
        }
    }
}
