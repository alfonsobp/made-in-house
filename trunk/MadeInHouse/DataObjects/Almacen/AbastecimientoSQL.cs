using MadeInHouse.Models.Almacen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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

        public int insertarAbastecimiento(int idTienda)
        {
            int idSolicitud = -1;
            db.cmd.CommandText = " INSERT INTO SolicitudAbastecimiento(estado , fechaReg , idTienda, idUsuario) " +
                                 " output INSERTED.idSolicitudAB " +
                                 " VALUES (1, GETDATE(), @idTienda, @idUsuario) ";
            db.cmd.Parameters.AddWithValue("@idTienda", idTienda);
            db.cmd.Parameters.AddWithValue("@idUsuario", Thread.CurrentPrincipal.Identity.Name);

            if (db.cmd.Transaction == null) db.conn.Open();
            
            idSolicitud = (int) db.cmd.ExecuteScalar();
            
            if (db.cmd.Transaction == null) db.conn.Close();
            db.cmd.Parameters.Clear();

            return idSolicitud;
        }

        public bool insertarProductosAbastecimiento(int idSolicitud, List<AbastecimientoProducto> prod)
        {
            int result = 0;
            string values = "";
            if (prod != null && prod.Count > 0)
            {
                foreach (AbastecimientoProducto item in prod)
                {
                    if (!String.IsNullOrEmpty(values)) values += " , ";
                    values += " (@idSolicitudAB, " + item.idProducto + " , " + item.pedido + " , " + item.atendido + " ) ";
                    if (!this.actualizarStock(item.idProducto, item.atendido - item.atendidoReal, item.stock, item.stockPendiente))
                        return false;
                }

                db.cmd.CommandText = " INSERT INTO ProductoxSolicitudAb (idSolicitudAB , idProducto , cantidad , cantidadAtendida) " +
                                     " VALUES " + values;
                db.cmd.Parameters.AddWithValue("@idSolicitudAB", idSolicitud);

                if (db.cmd.Transaction == null) db.conn.Open();

                result = db.cmd.ExecuteNonQuery();

                if (db.cmd.Transaction == null) db.conn.Close();
                db.cmd.Parameters.Clear();
            }

            return (result > 0) ? true : false;
        }

        public List<Abastecimiento> buscarAbastecimientos(string registroDesde, string registroHasta, int estado, int idTienda = -1)
        {
            List<Abastecimiento> lstAux = null;
            Abastecimiento abTemp;
            int posId, posState, posReg, posAtent, posTienda, posNomTienda;
            string where = "";

            if (!String.IsNullOrEmpty(registroDesde)){
                where += " AND convert (char, sa.fechaReg, 103) >= @registroDesde ";
                db.cmd.Parameters.Add(new SqlParameter("registroDesde", registroDesde));
            }

            if (!String.IsNullOrEmpty(registroHasta)){
                where += " AND convert (char, sa.fechaReg, 103) <= @registroHasta ";
                db.cmd.Parameters.Add(new SqlParameter("registroHasta", registroHasta));
            }

            if (estado >= 0)
            {
                where += " AND sa.estado = @estado ";
                db.cmd.Parameters.Add(new SqlParameter("estado", estado));
            }

            if (idTienda > 0)
            {
                where += " AND sa.idTienda = @idTienda ";
                db.cmd.Parameters.Add(new SqlParameter("idTienda", idTienda));
            }

            db.cmd.CommandText = "SELECT * FROM SolicitudAbastecimiento sa, Tienda t WHERE sa.idTienda = t.idTienda " + where + " ORDER by idSolicitudAB ASC";

            if (db.cmd.Transaction == null) db.conn.Open();
            SqlDataReader reader = db.cmd.ExecuteReader();

            while (reader.Read())
            {
                if (lstAux == null) lstAux = new List<Abastecimiento>();
                abTemp = new Abastecimiento();
                posId = reader.GetOrdinal("idSolicitudAB");
                posState = reader.GetOrdinal("estado");
                posReg = reader.GetOrdinal("fechaReg");
                posAtent = reader.GetOrdinal("fechaAtencion");
                posTienda = reader.GetOrdinal("idTienda");
                posNomTienda = reader.GetOrdinal("nombre");
                abTemp.idSolicitudAB = reader.IsDBNull(posId)? -1 : reader.GetInt32(posId);
                abTemp.estado = reader.IsDBNull(posState)? -1 : reader.GetInt32(posState);
                abTemp.txtEstado = (abTemp.estado == 1) ? "Registrada" : ((abTemp.estado == 2) ? "En revisión" : ((abTemp.estado == 3) ? "Revisada" : ((abTemp.estado == 4) ? "Consolidada" : ((abTemp.estado == 5) ? "Atendida" : "Anulada"))));
                abTemp.fechaReg = reader.IsDBNull(posReg)? null : reader.GetDateTime(posReg).ToString();
                abTemp.fechaAtencion = reader.IsDBNull(posAtent)? null : reader.GetDateTime(posAtent).ToString();
                abTemp.idTienda = reader.IsDBNull(posTienda)? -1 : reader.GetInt32(posTienda);
                abTemp.nomTienda = reader.IsDBNull(posTienda) ? null : reader.GetString(posNomTienda);
                lstAux.Add(abTemp);
            }

            db.cmd.Parameters.Clear();
            if (db.cmd.Transaction == null) db.conn.Close();
            
            return lstAux;
        }

        public List<AbastecimientoProducto> buscarProductosAbastecimiento(int idSolicitud, int idTienda = -1)
        {
            List<AbastecimientoProducto> lstAux = null;
            List<ProductoxTienda> prod;
            ProductoSQL pSQL = new ProductoSQL();
            AlmacenSQL almSQL = new AlmacenSQL();
            AbastecimientoProducto abTemp;
            int posProd, posNomProd, posCant, posAtent;
            int idAlmacen = idTienda;
            if (idTienda == 0)
                idAlmacen = almSQL.obtenerDeposito(idTienda);

            db.cmd.CommandText = "SELECT * FROM ProductoxSolicitudAb ps, Producto p WHERE ps.idProducto = p.idProducto AND ps.idSolicitudAB = @idSolicitudAB ";
            db.cmd.Parameters.Add(new SqlParameter("idSolicitudAB", idSolicitud));

            if (db.cmd.Transaction == null) db.conn.Open();
            SqlDataReader reader = db.cmd.ExecuteReader();

            while (reader.Read())
            {
                if (lstAux == null) lstAux = new List<AbastecimientoProducto>();
                abTemp = new AbastecimientoProducto();
                posProd = reader.GetOrdinal("idProducto");
                posNomProd = reader.GetOrdinal("nombre");
                posCant = reader.GetOrdinal("cantidad");
                posAtent = reader.GetOrdinal("cantidadAtendida");
                abTemp.idProducto = reader.IsDBNull(posProd) ? -1 : reader.GetInt32(posProd);
                abTemp.nombre = reader.IsDBNull(posNomProd) ? null : reader.GetString(posNomProd);
                abTemp.pedido = reader.IsDBNull(posCant) ? -1 : reader.GetInt32(posCant);
                abTemp.atendido = reader.IsDBNull(posAtent) ? -1 : reader.GetInt32(posAtent);
                abTemp.atendidoReal = reader.IsDBNull(posAtent) ? -1 : reader.GetInt32(posAtent);
                if (idTienda == 0)
                    prod = pSQL.BuscarProductoxCentral(idAlmacen, abTemp.idProducto);
                else
                    prod = pSQL.BuscarProductoxTienda(idAlmacen, abTemp.idProducto);
                abTemp.stock = prod == null? -1 : prod.ElementAt(0).StockActual;
                abTemp.stockPendiente = prod == null ? -1 : prod.ElementAt(0).StockPendiente;
                abTemp.sugerido = prod == null ? -1 : ((prod.ElementAt(0).StockMin - prod.ElementAt(0).StockActual) > 0 ? (prod.ElementAt(0).StockMin - prod.ElementAt(0).StockActual) : 0);
                lstAux.Add(abTemp);
            }

            db.cmd.Parameters.Clear();
            reader.Close();
            if (db.cmd.Transaction == null) db.conn.Close();
            
            return lstAux;
        }

        public int actualizarAbastecimiento(int idSolicitudAB, int idUsuario, int estado = -1)
        {
            int result = -1;
            string set = "";
            if (estado > -1)
            {
                set = " , estado = @estado ";
                db.cmd.Parameters.AddWithValue("@estado", estado);
            }

            db.cmd.CommandText = " UPDATE SolicitudAbastecimiento SET " +
                                 " fechaMod = GETDATE(), idUsuario = @idUsuario " +
                                 set +
                                 " WHERE idSolicitudAB =  @idSolicitudAB ";
            db.cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
            db.cmd.Parameters.AddWithValue("@idSolicitudAB", idSolicitudAB);

            if (db.cmd.Transaction == null) db.conn.Open();

            result = db.cmd.ExecuteNonQuery();

            if (db.cmd.Transaction == null) db.conn.Close();
            db.cmd.Parameters.Clear();

            return result;
        }

        public int eliminarProductosAbastecimiento(int idSolicitudAB)
        {
            int result = -1;
            db.cmd.CommandText = " DELETE FROM ProductoxSolicitudAb " +
                                 " WHERE idSolicitudAB =  @idSolicitudAB ";
            db.cmd.Parameters.AddWithValue("@idSolicitudAB", idSolicitudAB);

            if (db.cmd.Transaction == null) db.conn.Open();

            result = db.cmd.ExecuteNonQuery();

            if (db.cmd.Transaction == null) db.conn.Close();
            db.cmd.Parameters.Clear();

            return result;
        }

        public int eliminarAbastecimiento(int idSolicitudAB, int idUsuario)
        {
            int result = -1;
            db.cmd.CommandText = " UPDATE SolicitudAbastecimiento SET " +
                                 " fechaMod = GETDATE(), idUsuario = @idUsuario, estado = 0 " +
                                 " WHERE idSolicitudAB =  @idSolicitudAB ";
            db.cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
            db.cmd.Parameters.AddWithValue("@idSolicitudAB", idSolicitudAB);

            if (db.cmd.Transaction == null) db.conn.Open();

            result = db.cmd.ExecuteNonQuery();

            if (db.cmd.Transaction == null) db.conn.Close();
            db.cmd.Parameters.Clear();

            return result;
        }

        public int atenderAbastecimiento(int idSolicitudAB, int idUsuario)
        {
            int result = -1;
            db.cmd.CommandText = " UPDATE SolicitudAbastecimiento SET " +
                                 " estado = 3, idUsuario = @idUsuario, fechaAtencion = GETDATE() " +
                                 " WHERE idSolicitudAB =  @idSolicitudAB ";
            db.cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
            db.cmd.Parameters.AddWithValue("@idSolicitudAB", idSolicitudAB);

            if (db.cmd.Transaction == null) db.conn.Open();

            result = db.cmd.ExecuteNonQuery();

            if (db.cmd.Transaction == null) db.conn.Close();
            db.cmd.Parameters.Clear();

            return result;
        }

        public bool actualizarStock(int idProducto, int cantidad, int stockActual, int stockPendiente)
        {
            db.cmd.CommandText = " UPDATE Producto SET " +
                                 " stockActual = @stockActual, stockPendiente = @stockPendiente " +
                                 " WHERE idProducto = @idProducto ";
            db.cmd.Parameters.AddWithValue("@stockActual", stockActual - cantidad);
            db.cmd.Parameters.AddWithValue("@stockPendiente", stockPendiente + cantidad);
            db.cmd.Parameters.AddWithValue("@idProducto", idProducto);

            if (db.cmd.Transaction == null) db.conn.Open();

            int result = db.cmd.ExecuteNonQuery();

            if (db.cmd.Transaction == null) db.conn.Close();
            db.cmd.Parameters.Clear();

            return result > 0 ? true : false;
        }
    }
}