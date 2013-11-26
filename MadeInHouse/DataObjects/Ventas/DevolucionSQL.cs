using MadeInHouse.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.DataObjects.Ventas
{
    class DevolucionSQL
    {
        private DBConexion db;

        public DevolucionSQL(DBConexion db = null)
        {
            this.db = (db == null) ? new DBConexion() : db;
        }

        public List<DevolucionProducto> BuscarProductos(int idProducto = -1, int idVenta = -1, string docPago = null, int idDevolucion = -1)
        {
            List<DevolucionProducto> lstProducto = null;
            DevolucionProducto detTemp;
            int posDocPago, posCantidad, posNombre, posCodProducto, posPrecio, posIdProducto;
            int posDevuelto, posObservaciones, posDescuento;
            double precioTemp, descuentoTemp;
            string where = "";

            if (idProducto != -1)
            {
                where += " AND dv.idProducto = @idProducto ";
                db.cmd.Parameters.Add(new SqlParameter("idProducto", idProducto));
            }

            if (idVenta != -1)
            {
                where += " AND dv.idVenta = @idVenta ";
                db.cmd.Parameters.Add(new SqlParameter("idVenta", idVenta));
            }

            if (!String.IsNullOrEmpty(docPago))
            {
                where += " AND (v.numDocPagoProducto = UPPER(@docPago) OR v.numDocPagoServicio = UPPER(@docPago)) ";
                db.cmd.Parameters.Add(new SqlParameter("docPago", docPago));
            }

            if (idDevolucion != -1)
            {
                where += " AND dd.idDevolucion = @idDevolucion ";
                db.cmd.Parameters.Add(new SqlParameter("idDevolucion", idDevolucion));
            }

            db.cmd.CommandText = " select v.idVenta as id, dv.cantidad as cantidad, p.Nombre as nombre, " +
                                 " p.codProducto as codProducto, p.idProducto as idProducto, " +
                                 " dv.precio as precioV, dv.descuento as descuentoV, v.numDocPagoProducto as docPago, " +
                                 " sum(dd.cantidad) as devuelto, max(dd.motivo) as observaciones " +
                                 " from DetalleVenta dv join Venta v on v.idVenta = dv.idVenta " +
                                 " join Producto p on dv.idProducto = p.idProducto " +
                                 " join Usuario u on v.idUsuario = u.idUsuario " +
                                 " left join ProductoxTienda pt on pt.idProducto = p.idProducto " +
                                 " and pt.idTienda = u.idTienda " +
                                 " left join Devolucion d on d.idVenta = dv.idVenta " +
                                 " left join DetalleDevolucion dd on dd.idDevolucion = d.idDevolucion " +
                                 " and dd.idProducto = dv.idProducto " +
                                 " WHERE 1 = 1 " + where +
                                 " GROUP BY v.idVenta, dv.cantidad, p.Nombre, p.codProducto, " +
                                 " p.idProducto, dv.precio, dv.descuento, v.numDocPagoProducto ";
            try
            {
                if (db.cmd.Transaction == null) db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (lstProducto == null) lstProducto = new List<DevolucionProducto>();
                    detTemp = new DevolucionProducto();
                    posDocPago = reader.GetOrdinal("docPago");
                    posCantidad = reader.GetOrdinal("cantidad");
                    posDevuelto = reader.GetOrdinal("devuelto");
                    posNombre = reader.GetOrdinal("nombre");
                    posCodProducto = reader.GetOrdinal("codProducto");
                    posIdProducto = reader.GetOrdinal("idProducto");
                    posPrecio = reader.GetOrdinal("precioV");
                    posDescuento = reader.GetOrdinal("descuentoV");
                    posObservaciones = reader.GetOrdinal("observaciones");
                    detTemp.DocPago = reader.IsDBNull(posDocPago) ? null : reader.GetString(posDocPago);
                    detTemp.IdProducto = reader.IsDBNull(posIdProducto) ? -1 : reader.GetInt32(posIdProducto);
                    detTemp.CodProducto = reader.IsDBNull(posCodProducto) ? null : reader.GetString(posCodProducto);
                    detTemp.Producto = reader.IsDBNull(posNombre) ? null : reader.GetString(posNombre);
                    detTemp.Cantidad = reader.IsDBNull(posCantidad) ? -1 : reader.GetInt32(posCantidad);
                    detTemp.Devuelto = reader.IsDBNull(posDevuelto) ? 0 : reader.GetInt32(posDevuelto);
                    precioTemp = reader.IsDBNull(posPrecio) ? -1 : (double)reader.GetDecimal(posPrecio);
                    descuentoTemp = reader.IsDBNull(posDescuento) ? -1 : (double)reader.GetDecimal(posDescuento);
                    detTemp.Precio = (precioTemp >= 0 && descuentoTemp >= 0) ? precioTemp - descuentoTemp : -1;
                    if (idDevolucion != -1)
                        detTemp.Observaciones = reader.IsDBNull(posObservaciones) ? null : reader.GetString(posObservaciones);
                    lstProducto.Add(detTemp);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace.ToString());
            }

            db.cmd.Parameters.Clear();
            if (db.cmd.Transaction == null) db.conn.Close();

            return lstProducto;
        }

        public Venta BuscarVenta(string docPago = null)
        {
            Venta vent = null;
            int posDNI, posIdVenta;
            string where = "";

            if (!String.IsNullOrEmpty(docPago))
            {
                where += " WHERE (v.numDocPagoProducto = UPPER(@docPago) OR v.numDocPagoServicio = UPPER(@docPago)) ";
                db.cmd.Parameters.Add(new SqlParameter("docPago", docPago));


                db.cmd.CommandText = "SELECT * FROM Venta v " + where;

                if (db.cmd.Transaction == null) db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();

                if (reader.Read())
                {
                    if (vent == null) vent = new Venta();
                    posDNI = reader.GetOrdinal("dni");
                    posIdVenta = reader.GetOrdinal("idVenta");
                    vent.IdVenta = reader.IsDBNull(posIdVenta) ? -1 : reader.GetInt32(posIdVenta);
                    vent.dni = reader.IsDBNull(posDNI) ? null : reader.GetString(posDNI);
                }

                db.cmd.Parameters.Clear();
                if (db.cmd.Transaction == null) db.conn.Close();
            }
            return vent;
        }

        public int numDevoluciones()
        {
            int result = -1;

            db.cmd.CommandText = " SELECT COUNT(*) FROM Devolucion ";

            if (db.cmd.Transaction == null) db.conn.Open();
            result = (int) db.cmd.ExecuteScalar();
            if (db.cmd.Transaction == null) db.conn.Close();

            return result;
        }

        public int insertarDevolucion(Devolucion dev)
        {
            int result = -1;

            db.cmd.CommandText = " INSERT INTO Devolucion (numDocCredito,monto,fechaReg,estado,idVenta,idUsuario,archivo) " +
                " output INSERTED.idDevolucion " +
                " VALUES (@numDocCredito, @monto, GETDATE(), @estado, @idVenta, @idUsuario, @archivo) ";

            db.cmd.Parameters.AddWithValue("@numDocCredito", dev.NumDocCredito);
            db.cmd.Parameters.AddWithValue("@monto", dev.Monto);
            db.cmd.Parameters.AddWithValue("@idVenta", dev.venta.IdVenta);
            db.cmd.Parameters.AddWithValue("@idUsuario", dev.idUsuario);
            db.cmd.Parameters.AddWithValue("@archivo", dev.archivo);
            db.cmd.Parameters.AddWithValue("@estado", 1);

            if (db.cmd.Transaction == null) db.conn.Open();
            result = (int)db.cmd.ExecuteScalar();
            if (db.cmd.Transaction == null) db.conn.Close();
            db.cmd.Parameters.Clear();

            return result;
        }

        public bool insertarProductosDevolucion(int idDevolucion, List<DevolucionProducto> prod)
        {
            int result = 0;
            string values = "";
            if (prod != null && prod.Count > 0)
            {
                foreach (DevolucionProducto item in prod)
                {
                    if (!String.IsNullOrEmpty(values)) values += " , ";
                    values += " (@idDevolucion, " + item.Devuelve + " , " + (String.IsNullOrEmpty(item.Observaciones) ? "NULL" : ("'" + item.Observaciones + "'")) + " , " + item.IdProducto + " ) ";
                }

                db.cmd.CommandText = " INSERT INTO DetalleDevolucion (idDevolucion , cantidad , motivo , idProducto) " +
                                     " VALUES " + values;
                db.cmd.Parameters.AddWithValue("@idDevolucion", idDevolucion);

                if (db.cmd.Transaction == null) db.conn.Open();

                result = db.cmd.ExecuteNonQuery();

                if (db.cmd.Transaction == null) db.conn.Close();
                db.cmd.Parameters.Clear();
            }

            return (result > 0) ? true : false;
        }

        public List<Devolucion> buscarDevoluciones(string notaCredito, string docPago, int estado, string registroDesde, string registroHasta, string dni)
        {
            List<Devolucion> lstAux = null;
            Devolucion devTemp;
            int posIdDevolucion, posDNI, posDocPago, posNotaCredito, posMonto, posRegistro, posEstado;
            string where = "";

            if (!String.IsNullOrEmpty(notaCredito))
            {
                where += " AND d.numDocCredito = @notaCredito ";
                db.cmd.Parameters.Add(new SqlParameter("notaCredito", notaCredito));
            }

            if (!String.IsNullOrEmpty(docPago))
            {
                where += " AND v.numDocPagoProducto = @docPago ";
                db.cmd.Parameters.Add(new SqlParameter("docPago", docPago));
            }

            if (estado > 0)
            {
                where += " AND d.estado = @estado ";
                db.cmd.Parameters.Add(new SqlParameter("estado", estado));
            }

            if (!String.IsNullOrEmpty(registroDesde))
            {
                where += " AND convert (char, d.fechaReg, 103) >= @registroDesde ";
                db.cmd.Parameters.Add(new SqlParameter("registroDesde", registroDesde));
            }

            if (!String.IsNullOrEmpty(registroHasta))
            {
                where += " AND convert (char, d.fechaReg, 103) <= @registroHasta ";
                db.cmd.Parameters.Add(new SqlParameter("registroHasta", registroHasta));
            }

            if (!String.IsNullOrEmpty(dni))
            {
                where += " AND v.dni = @dni ";
                db.cmd.Parameters.Add(new SqlParameter("dni", dni));
            }

            db.cmd.CommandText = "SELECT * FROM Devolucion d, Venta v WHERE d.idVenta = v.idVenta " + where + " ORDER by idDevolucion ASC";

            if (db.cmd.Transaction == null) db.conn.Open();
            SqlDataReader reader = db.cmd.ExecuteReader();

            while (reader.Read())
            {
                if (lstAux == null) lstAux = new List<Devolucion>();
                devTemp = new Devolucion();
                devTemp.venta = new Venta();
                posIdDevolucion = reader.GetOrdinal("idDevolucion");
                posDNI = reader.GetOrdinal("dni");
                posDocPago = reader.GetOrdinal("numDocPagoProducto");
                posNotaCredito = reader.GetOrdinal("numDocCredito");
                posMonto = reader.GetOrdinal("monto");
                posRegistro = reader.GetOrdinal("fechaReg");
                posEstado = reader.GetOrdinal("estado");
                devTemp.IdDevolucion = reader.IsDBNull(posIdDevolucion) ? -1 : reader.GetInt32(posIdDevolucion);
                devTemp.venta.dni = reader.IsDBNull(posDNI) ? null : reader.GetString(posDNI);
                devTemp.venta.NumDocPago = reader.IsDBNull(posDocPago) ? null : reader.GetString(posDocPago);
                devTemp.NumDocCredito = reader.IsDBNull(posNotaCredito) ? null : reader.GetString(posNotaCredito);
                devTemp.Monto = reader.IsDBNull(posMonto) ? -1 : (double)reader.GetDecimal(posMonto);
                devTemp.fechaReg = reader.IsDBNull(posRegistro) ? null : reader.GetDateTime(posRegistro).ToString();
                devTemp.estado = reader.IsDBNull(posEstado) ? -1 : reader.GetInt32(posEstado);
                devTemp.nomEstado = (devTemp.estado == 1) ? "Registrada" : ((devTemp.estado == 2) ? "Ingresada" : "Anulada");
                lstAux.Add(devTemp);
            }

            db.cmd.Parameters.Clear();
            if (db.cmd.Transaction == null) db.conn.Close();

            return lstAux;
        }

        public bool cambiarEstado (int idDevolucion, int estado)
        {
            int result = 0;
            db.cmd.CommandText = " UPDATE Devolucion SET estado = @estado " +
                                    " WHERE idDevolucion = @idDevolucion ";
            db.cmd.Parameters.AddWithValue("@estado", estado);
            db.cmd.Parameters.AddWithValue("@idDevolucion", idDevolucion);

            if (db.cmd.Transaction == null) db.conn.Open();

            result = db.cmd.ExecuteNonQuery();

            if (db.cmd.Transaction == null) db.conn.Close();
            db.cmd.Parameters.Clear();

            return (result > 0) ? true : false;
        }

        public bool puedeAnular(int idDevolucion)
        {
            bool result = true;

            db.cmd.CommandText = " SELECT idDevolucion FROM Devolucion " +
                                 " WHERE idDevolucion = @idDevolucion AND estado = 3 ";
            db.cmd.Parameters.AddWithValue("@idDevolucion", idDevolucion);

            if (db.cmd.Transaction == null) db.conn.Open();

            result = db.cmd.ExecuteScalar() == null? true: false;

            if (db.cmd.Transaction == null) db.conn.Close();
            db.cmd.Parameters.Clear();

            return result;
        }
    }
}
