using MadeInHouse.Models.Compras;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects.Almacen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.DataObjects.Compras
{
    class CotizacionSQL:EntitySQL
    {
        public int Agregar(object entity)
        {
            DBConexion db = new DBConexion();
            int k = 0;
            Cotizacion c = entity as Cotizacion;

            db.cmd.CommandText = "INSERT INTO Cotizacion(fechaInicio,fechaFin,estado,idProveedor,fechaResp,observacion)" +
                                 "VALUES (@fechaInicio,@fechaFin,@estado,@idProveedor,@fechaResp,@observacion)";

            db.cmd.Parameters.AddWithValue("@fechaInicio", DateTime.Now);
            db.cmd.Parameters.AddWithValue("@fechaFin", DateTime.Now);
            db.cmd.Parameters.AddWithValue("@estado", 1);
            db.cmd.Parameters.AddWithValue("@idProveedor", c.Proveedor.IdProveedor);
            db.cmd.Parameters.AddWithValue("@fechaResp", DateTime.Now);
            db.cmd.Parameters.AddWithValue("@observacion", c.Observacion);

            try
            {
                db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                db.conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;
        }

        public object Buscar(params object[] filters)
        {
            throw new NotImplementedException();
        }

        public int Actualizar(object entity)
        {
            DBConexion db = new DBConexion();
            Cotizacion c = entity as Cotizacion;
            int k = 0;

            db.cmd.CommandText = "UPDATE Cotizacion " +
                                 "SET estado= @estado,fechaInicio= @fechaInicio,fechaFin= @fechaFin,fechaResp= @fechaResp " +
                                 "WHERE idCotizacion= @idCotizacion ";

            db.cmd.Parameters.AddWithValue("@idCotizacion", c.IdCotizacion);
            db.cmd.Parameters.AddWithValue("@estado", 2);
            db.cmd.Parameters.AddWithValue("@fechaInicio", c.FechaInicio);
            db.cmd.Parameters.AddWithValue("@fechaFin", c.FechaFin);
            db.cmd.Parameters.AddWithValue("@fechaResp", c.FechaRespuesta);

            try
            {
                db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                db.conn.Close();

            }

            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;
        }

        public int Eliminar(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
