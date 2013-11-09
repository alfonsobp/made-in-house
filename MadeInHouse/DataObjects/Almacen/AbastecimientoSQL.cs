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

        public int insertarAbastecimiento(int idTienda)
        {
            int idSolicitud = -1;
            db.cmd.CommandText = " INSERT INTO SolicitudAbastecimiento(estado , fechaReg , fechaAtencion , idOrden , idTienda) " +
                                 " output INSERTED.idSolicitudAB " +
                                 " VALUES (1, GETDATE(), NULL, NULL, @idTienda) ";
            db.cmd.Parameters.AddWithValue("@idTienda", idTienda);

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

        public List<Abastecimiento> buscarAbastecimientos(string registroDesde, string registroHasta)
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
                abTemp.txtEstado = (abTemp.estado == 1) ? "Registrada" : ((abTemp.estado == 2) ? "En revisión" : ((abTemp.estado == 3) ? "Revisada" : ((abTemp.estado == 4) ? "Atendida" : "Anulada")));
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

        public List<AbastecimientoProducto> buscarProductosAbastecimiento(int idSolicitud, int idTienda)
        {
            List<AbastecimientoProducto> lstAux = null;
            List<ProductoxAlmacen> prod;
            ProductoSQL pSQL = new ProductoSQL();
            AlmacenSQL almSQL = new AlmacenSQL();
            AbastecimientoProducto abTemp;
            int posProd, posNomProd, posCant, posAtent;
            int idAlmacen = almSQL.obtenerDeposito(idTienda);

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
                prod = pSQL.BuscarProductoxAlmacen(idAlmacen, abTemp.idProducto);
                abTemp.stock = prod == null? -1 : prod.ElementAt(0).StockActual;
                abTemp.sugerido = prod == null ? -1 : (prod.ElementAt(0).StockMin - prod.ElementAt(0).StockActual);
                lstAux.Add(abTemp);
            }

            db.cmd.Parameters.Clear();
            if (db.cmd.Transaction == null) db.conn.Close();
            
            return lstAux;
        }

        public int actualizarAbastecimiento(int idSolicitudAB, int idUsuario)
        {
            int result = -1;
            db.cmd.CommandText = " UPDATE SolicitudAbastecimiento SET " +
                                 " fechaMod = GETDATE(), idUsuario = @idUsuario " +
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
    }
}