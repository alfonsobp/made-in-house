using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MadeInHouse.DataObjects.Ventas;
using MadeInHouse.Models.Ventas;
using MadeInHouse.Models.Seguridad;

namespace MadeInHouse.DataObjects.Ventas
{
    class VentaSQL
    {
        private DBConexion db;
        private bool tipo = true;

        public VentaSQL(DBConexion db = null)
        {

            if (db == null)
            {
                this.db = new DBConexion();
            }
            else
            {
                this.db = db;
                tipo = false;
            }

        }

        public int Agregar(Venta v)
        {
            int k = 0;

            db.cmd.CommandText = "INSERT INTO Venta(monto,descuento,IGV,ptosGanados,estado,idCliente,tipoVenta,fechaReg,tipoDocPago,idUsuario,codTarjeta)" +
                                  "OUTPUT INSERTED.idVenta VALUES (@monto,@descuento,@IGV,@ptsGanados,@estado,@idCliente,@tipoVenta,@fechaReg,@tipoDocPago,@idUsuario,@codTarjeta)";
            //db.cmd.Parameters.AddWithValue("@numDocPago", v.NumDocPago);
            db.cmd.Parameters.AddWithValue("@tipoDocPago", v.TipoDocPago);
            db.cmd.Parameters.AddWithValue("@monto", v.Monto);
            db.cmd.Parameters.AddWithValue("@descuento", v.Descuento);
            db.cmd.Parameters.AddWithValue("@IGV", v.Igv);
            db.cmd.Parameters.AddWithValue("@ptsGanados", v.PtosGanados);
            db.cmd.Parameters.AddWithValue("@fechaReg", v.FechaReg);
            db.cmd.Parameters.AddWithValue("@estado", v.Estado);
            db.cmd.Parameters.AddWithValue("@idUsuario", v.IdUsuario);
            db.cmd.Parameters.AddWithValue("@idCliente", v.IdCliente);
            db.cmd.Parameters.AddWithValue("@tipoVenta", v.TipoVenta);
            db.cmd.Parameters.AddWithValue("@codTarjeta", v.CodTarjeta);
            

            try
            {
                if (tipo) db.conn.Open();
                k = (int)db.cmd.ExecuteScalar();
                v.IdVenta = k;
                if (tipo) db.conn.Close();
                db.cmd.Parameters.Clear();

                //guardar el detalle de la venta
                DetalleVentaSQL dvm = new DetalleVentaSQL(db);
                foreach (DetalleVenta dv in v.LstDetalle)
                {
                    dvm.Agregar(v,dv);
                    descontarDeSector(v,dv);
                }

                foreach (VentaPago vp in v.LstPagos)
                {
                    AgregarPagoVenta(v, vp);
                }
                //agregar numDocPagoProducto
                v.NumDocPago = SacaNumDocPago(v,1);
                actualizaNumDocProductos(v);

                foreach (VentaPago vp in v.LstPagos)
                {
                    AgregarPagoVenta(v, vp);
                }

                if (v.LstDetalleServicio.Count() > 0)
                {
                    foreach (DetalleVentaServicio item in v.LstDetalleServicio)
                    {
                        dvm.AgregarServicios(v, item);
                    }
                    //agregar numDocPagoServicio
                    v.NumDocPagoServicio = SacaNumDocPago(v,2);
                    actualizaNumDocServicios(v);
                }

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;
        }

        public int AgregarVentaObra(Venta v)
        {
            int k = 0;

            db.cmd.CommandText = "INSERT INTO Venta(monto,descuento,IGV,ptosGanados,estado,idCliente,tipoVenta,fechaReg,fechaDespacho,tipoDocPago,idUsuario,codTarjeta)" +
                                  "OUTPUT INSERTED.idVenta VALUES (@monto,@descuento,@IGV,@ptsGanados,@estado,@idCliente,@tipoVenta,@fechaReg,@fechaDespacho,@tipoDocPago,@idUsuario,@codTarjeta)";
            //db.cmd.Parameters.AddWithValue("@numDocPago", v.NumDocPago);
            db.cmd.Parameters.AddWithValue("@tipoDocPago", v.TipoDocPago);
            db.cmd.Parameters.AddWithValue("@monto", v.Monto);
            db.cmd.Parameters.AddWithValue("@descuento", v.Descuento);
            db.cmd.Parameters.AddWithValue("@IGV", v.Igv);
            db.cmd.Parameters.AddWithValue("@ptsGanados", v.PtosGanados);
            db.cmd.Parameters.AddWithValue("@fechaReg", v.FechaReg);
            db.cmd.Parameters.AddWithValue("@estado", v.Estado);
            db.cmd.Parameters.AddWithValue("@idUsuario", v.IdUsuario);
            db.cmd.Parameters.AddWithValue("@idCliente", v.IdCliente);
            db.cmd.Parameters.AddWithValue("@tipoVenta", v.TipoVenta);
            db.cmd.Parameters.AddWithValue("@codTarjeta", v.CodTarjeta);
            db.cmd.Parameters.AddWithValue("@fechaDespacho", v.FechaDespacho);


            try
            {
                if (tipo) db.conn.Open();
                k = (int)db.cmd.ExecuteScalar();
                v.IdVenta = k;
                if (tipo) db.conn.Close();
                db.cmd.Parameters.Clear();

                //generar orden de despacho
                InsertaOrdenDeDespacho(v);

                //guardar el detalle de la venta
                DetalleVentaSQL dvm = new DetalleVentaSQL(db);
                foreach (DetalleVenta dv in v.LstDetalle)
                {
                    dvm.Agregar(v, dv);
                }

                foreach (VentaPago vp in v.LstPagos)
                {
                    AgregarPagoVenta(v, vp);
                }
                //agregar numDocPagoProducto
                v.NumDocPago = SacaNumDocPago(v,1);
                actualizaNumDocProductos(v);

                foreach (VentaPago vp in v.LstPagos)
                {
                    AgregarPagoVenta(v, vp);
                }

                if (v.LstDetalleServicio.Count > 0)
                {
                    foreach (DetalleVentaServicio item in v.LstDetalleServicio)
                    {
                        dvm.AgregarServicios(v, item);
                    }
                    //agregar numDocPagoServicio
                    v.NumDocPagoServicio = SacaNumDocPago(v,2);
                    actualizaNumDocServicios(v);
                }
                
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;
        }

        public int AgregarSinCliente(Venta v)
        {
            DBConexion db = new DBConexion();
            int k = 0;

            db.cmd.CommandText = "INSERT INTO Venta(monto,descuento,IGV,ptosGanados,estado,tipoVenta,fechaReg,tipoDocPago,idUsuario)" +
                                  "OUTPUT INSERTED.idVenta VALUES (@monto,@descuento,@IGV,@ptsGanados,@estado,@tipoVenta,@fechaReg,@tipoDocPago,@idUsuario)";
            //db.cmd.Parameters.AddWithValue("@numDocPago", v.NumDocPago);
            db.cmd.Parameters.AddWithValue("@tipoDocPago", v.TipoDocPago);
            db.cmd.Parameters.AddWithValue("@monto", v.Monto);
            db.cmd.Parameters.AddWithValue("@descuento", v.Descuento);
            db.cmd.Parameters.AddWithValue("@IGV", v.Igv);
            db.cmd.Parameters.AddWithValue("@ptsGanados", v.PtosGanados);
            db.cmd.Parameters.AddWithValue("@fechaReg", v.FechaReg);
            db.cmd.Parameters.AddWithValue("@estado", v.Estado);
            db.cmd.Parameters.AddWithValue("@idUsuario", v.IdUsuario);
            db.cmd.Parameters.AddWithValue("@tipoVenta", v.TipoVenta);

            try
            {
                if (tipo) db.conn.Open();
                k = (int)db.cmd.ExecuteScalar();
                v.IdVenta = k;
                if (tipo) db.conn.Close();

                //guardar el detalle de la venta
                DetalleVentaSQL dvm = new DetalleVentaSQL(db);
                foreach (DetalleVenta dv in v.LstDetalle)
                {
                    dvm.Agregar(v, dv);
                    descontarDeSector(v, dv);
                }
                //agregar numDocPagoProducto
                v.NumDocPago = SacaNumDocPago(v, 1);
                actualizaNumDocProductos(v);

                foreach (VentaPago vp in v.LstPagos)
                {
                    AgregarPagoVenta(v, vp);
                }

                if (v.LstDetalleServicio.Count > 0)
                {
                    foreach (DetalleVentaServicio item in v.LstDetalleServicio)
                    {
                        dvm.AgregarServicios(v, item);
                    }
                    //agregar numDocPagoServicio
                    v.NumDocPagoServicio = SacaNumDocPago(v, 2);
                    actualizaNumDocServicios(v);
                }


            }
            catch (SqlException e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;
        }

        private int InsertaOrdenDeDespacho(Venta v)
        {
            int k = 0;

            db.cmd.CommandText = "INSERT INTO OrdenDespacho(idVenta,fechaDespacho,estado)" +
                                  " VALUES (@idVenta,@fechaDespacho,@estado)";
            db.cmd.Parameters.AddWithValue("@idVenta", v.IdVenta);
            db.cmd.Parameters.AddWithValue("@fechaDespacho", v.FechaDespacho);
            db.cmd.Parameters.AddWithValue("@estado", 1);

            try
            {
                if (tipo) db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                if (tipo) db.conn.Close();
                db.cmd.Parameters.Clear();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }

            return k;
        }

        public void AgregarPagoVenta(Venta v, VentaPago vp)
        {
            db.cmd.CommandText = "INSERT INTO Pago(monto,idVenta,idModoPago) VALUES(@monto,@idVenta,@idModoPago)";
            db.cmd.Parameters.AddWithValue("@monto", vp.Monto);
            db.cmd.Parameters.AddWithValue("@idVenta", v.IdVenta);
            db.cmd.Parameters.AddWithValue("@idModoPago", vp.IdModoPago);
            try
            {
                if (tipo) db.conn.Open();
                db.cmd.ExecuteNonQuery();
                if (tipo) db.conn.Close();
                db.cmd.Parameters.Clear();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace.ToString());
            }

        }

        public object Buscar(params object[] filters)
        {
            DBConexion db = new DBConexion();
            List<Venta> lista = new List<Venta>();
            lista = null;

            // LstVentas = new VentaSQL().Buscar(TxtDocPago,  MontoMin, MontoMax,client,fechaInicio,fechaFin) as List<Venta>;
            String txtDocPago = filters[0] as String;
            String MontoMin = filters[1] as String;
            String MontoMax = filters[2] as String;
            Cliente client = filters[3] as Cliente;
            DateTime fechaIni = Convert.ToDateTime(filters[4]);
            DateTime fechaFin = Convert.ToDateTime(filters[5]);
            int estado = Convert.ToInt32(filters[6]);
            string sql = "";

            sql = "SELECT v.idVenta,v.codTarjeta,v.descuento,v.estado,v.fechaReg,v.idCliente   " +
                  " , c.tipoCliente as tipoC , c.razonSocial as rsC , c.nombre as nombreC      " +
                  " ,v.numDocPagoProducto,v.tipoDocPago,v.monto,v.descuento,v.IGV,v.ptosGanados,v.numDocPagoServicio " +
                    "FROM  Venta v LEFT JOIN  Cliente c ON c.IdCliente = v.IdCliente  ";



            string where = " WHERE 1=1 ";

            if (client != null)
            {

                where += "and  v.IdCliente = " + client.Id;

            }

            if (estado >= 0)
            {

                where += " and v.estado = " + estado;

            }

            if (!String.IsNullOrEmpty(txtDocPago))
            {
                where += " and ( v.numDocPagoProducto like '" + txtDocPago + "' ";
                where += " or v.numDocPagoServicio like '" + txtDocPago + "' ) ";
            }


            if (!String.IsNullOrEmpty(MontoMin))
            {

                where += " and  v.monto >= " + MontoMin;
            }

            if (!String.IsNullOrEmpty(MontoMax))
            {

                where += " and  v.monto <= " + MontoMax;
            }

            if (fechaIni != null)
            {
                where += " and CONVERT(DATE,'" + fechaIni.ToString("yyyy-MM-dd") + "')   <=  CONVERT(DATE,v.fechaReg,103) ";
            }


            if (fechaFin != null)
            {
                where += " and CONVERT(DATE,'" + fechaFin.ToString("yyyy-MM-dd") + "')   >=  CONVERT(DATE,v.fechaReg,103) ";
            }




            db.cmd.CommandText = sql + where;

            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (lista == null) lista = new List<Venta>();
                    Venta v = new Venta();
                    v.IdVenta = Convert.ToInt32(reader["idVenta"]);
                    v.NumDocPago = reader.IsDBNull(reader.GetOrdinal("numDocPagoProducto")) ? "" : reader["numDocPagoProducto"].ToString();
                    v.NumDocPagoServicio = reader.IsDBNull(reader.GetOrdinal("numDocPagoServicio")) ? "" : reader["numDocPagoServicio"].ToString(); ;
                    v.IdCliente = reader.IsDBNull(reader.GetOrdinal("idCliente")) ? 0 : Convert.ToInt32(reader["idCliente"]);
                    v.Monto = Convert.ToDouble(reader["monto"]);
                    v.Estado = Convert.ToInt32(reader["estado"]);

                    if (v.IdCliente != 0)
                    {
                        v.NombreCliente = (Convert.ToInt32(reader["tipoC"]) == 0) ? Convert.ToString(reader["nombreC"]) : Convert.ToString(reader["rsC"]);

                    }
                    else
                    {
                        v.NombreCliente = "NR";
                    }

                    v.FechaRegS = Convert.ToDateTime(reader["fechaReg"]).ToString();

                    v.EstadoS = (v.Estado == 0) ? "ANULADA" : "REALIZADA";
                    // t.tipoCliente as tipoC , t.razonSocial as rsC , t.nombre as nombreC  

                    lista.Add(v);

                }
                db.cmd.Parameters.Clear();
                db.conn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return lista;
        }

        public int Actualizar(object entity)
        {
            throw new NotImplementedException();
        }

        public int Eliminar(object entity)
        {
            throw new NotImplementedException();
        }

        private void descontarDeSector(Venta v, DetalleVenta dv)
        {
            Usuario u = new Usuario();
            db.cmd.CommandText = "SELECT * FROM Usuario WHERE idUsuario=@idUsuario";
            db.cmd.Parameters.AddWithValue("@idUsuario", v.IdUsuario);
            SqlDataReader rs = db.cmd.ExecuteReader();
            rs.Read();
            int idTienda = Convert.ToInt32(rs["idTienda"].ToString());

            db.cmd.CommandText = "SELECT * FROM Almacen WHERE idTienda=@idTienda AND tipo=@tipo";
            db.cmd.Parameters.AddWithValue("@idTienda", idTienda);
            db.cmd.Parameters.AddWithValue("@tipo", 2);
            SqlDataReader rs2 = db.cmd.ExecuteReader();
            rs2.Read();
            int idAlmacen = Convert.ToInt32(rs2["idAlmacen"].ToString());

            db.cmd.CommandText = "UPDATE Sector SET cantidad=cantidad-@cantidad WHERE idAlmacen=@idAlmacen AND idProducto=@idProducto; UPDATE ProductoxTienda SET stockActual=stockActual-@cantidad WHERE idTienda=@idTienda AND idProducto=@idProducto";
            db.cmd.Parameters.AddWithValue("@cantidad", dv.Cantidad);
            db.cmd.Parameters.AddWithValue("@idAlmacen", idAlmacen);
            db.cmd.Parameters.AddWithValue("@idProducto", dv.IdProducto);
            db.cmd.Parameters.AddWithValue("@idTienda", idTienda);
            try
            {
                if (tipo) db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.cmd.Parameters.Clear();
                if (tipo) db.conn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }


        }

        private void actualizaNumDocServicios(Venta v)
        {
            if(v.TipoDocPago.Equals("Boleta"))
                db.cmd.CommandText = "UPDATE Venta SET numDocPagoServicio='BOL' + right('0000000000' + cast( @numDocPagoServicio as varchar(10)), 10) WHERE idVenta=@idVenta";
            else
                db.cmd.CommandText = "UPDATE Venta SET numDocPagoServicio='FAC' + right('0000000000' + cast( @numDocPagoServicio as varchar(10)), 10) WHERE idVenta=@idVenta";
            
            db.cmd.Parameters.AddWithValue("@numDocPagoServicio", v.NumDocPagoServicio);
            db.cmd.Parameters.AddWithValue("@idVenta", v.IdVenta);

            try
            {
                if (tipo) db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.cmd.Parameters.Clear();
                if (tipo) db.conn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void actualizaNumDocProductos(Venta v)
        {
            if(v.TipoDocPago.Equals("Boleta"))
                db.cmd.CommandText = "UPDATE Venta SET numDocPagoProducto='BOL' + right('0000000000' + cast( @numDocPagoProducto as varchar(10)), 10) WHERE idVenta=@idVenta";
            else
                db.cmd.CommandText = "UPDATE Venta SET numDocPagoProducto='FAC' + right('0000000000' + cast( @numDocPagoProducto as varchar(10)), 10) WHERE idVenta=@idVenta";

            db.cmd.Parameters.AddWithValue("@numDocPagoProducto", v.NumDocPago);
            db.cmd.Parameters.AddWithValue("@idVenta", v.IdVenta);

            try
            {

                if (tipo) db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.cmd.Parameters.Clear();
                if (tipo) db.conn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private string SacaNumDocPago(Venta v,int tipo)
        {
            return numero(v, tipo).ToString();
        }

        private int numero(Venta v,int tipo2)
        {
            int k = 0;
            db.cmd.CommandText = "SELECT count(*) as C FROM Venta WHERE tipoDocPago=@tipoDocPago";
            db.cmd.Parameters.AddWithValue("@tipoDocPago", v.TipoDocPago);
            try
            {
                if (tipo) db.conn.Open();
                SqlDataReader rs = db.cmd.ExecuteReader();
                rs.Read();
                k = Convert.ToInt32(rs["C"].ToString());
                if (tipo) db.conn.Close();
                db.cmd.Parameters.Clear();
                rs.Close();
                
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }

            int k2 = 0;
            db.cmd.CommandText = "SELECT count(*) as C FROM Venta WHERE tipoDocPago=@tipoDocPago AND numDocPagoServicio is not null";
            db.cmd.Parameters.AddWithValue("@tipoDocPago", v.TipoDocPago);
            try
            {
                if (tipo) db.conn.Open();
                SqlDataReader rs2 = db.cmd.ExecuteReader();
                rs2.Read();
                k2 = Convert.ToInt32(rs2["C"].ToString());
                if (tipo) db.conn.Close();
                db.cmd.Parameters.Clear();
                rs2.Close();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }

            if (tipo2 == 2) return k + 1 + k2;
            else return k + k2;
        }


        public Venta buscarVentaPorId(int idVenta)
        {
                        Venta v = null;

            db.cmd.CommandText = "SELECT * FROM Venta WHERE idVenta=@idVenta ";
            db.cmd.Parameters.AddWithValue("@idVenta", idVenta);

            try
            {
                db.conn.Open();
                SqlDataReader reader;
                reader = db.cmd.ExecuteReader();

                if (reader.Read())
                {
                    v = new Venta();

                    v.IdVenta = Int32.Parse(reader["idVenta"].ToString());
                    v.TipoDocPago = reader["tipoDocPago"].ToString();
                    v.Monto = Double.Parse(reader["monto"].ToString());
                    v.Descuento = Double.Parse(reader["descuento"].ToString());
                    v.Igv = Double.Parse(reader["IGV"].ToString());
                    v.PtosGanados = Int32.Parse(reader["ptosGanados"].ToString());
                    v.FechaReg = DateTime.Parse(reader["fechaReg"].ToString());
                    v.Estado = Int32.Parse(reader["estado"].ToString());
                    v.IdUsuario = Int32.Parse(reader["idUsuario"].ToString());
                    v.IdCliente = Int32.Parse(reader["idCliente"].ToString());
                    v.TipoVenta = reader["tipoDocPago"].ToString();
                    v.CodTarjeta = Int32.Parse(reader["codTarjeta"].ToString());

                }
                else
                    db.conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return v;
        }
    }
}
