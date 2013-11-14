using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MadeInHouse.DataObjects.Ventas;
using MadeInHouse.Models.Ventas;

namespace MadeInHouse.DataObjects.Ventas
{
    class VentaSQL
    {

        public int Agregar(Venta v)
        {
            DBConexion db = new DBConexion();
            int k = 0;

            db.cmd.CommandText = "INSERT INTO Venta(monto,descuento,IGV,ptosGanados,estado,idCliente,tipoVenta,fechaReg,tipoDocPago,idUsuario,codTarjeta)" +
                                  " VALUES (@monto,@descuento,@IGV,@ptsGanados,@estado,@idCliente,@tipoVenta,@fechaReg,@tipoDocPago,@idUsuario,@codTarjeta)";
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
                db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                db.conn.Close();

                //sacar la ultima insersion
                DBConexion db2 = new DBConexion();

                db2.cmd.CommandText = "SELECT MAX (idVenta) as id FROM Venta";

                db2.conn.Open();
                SqlDataReader rs = db2.cmd.ExecuteReader();
                rs.Read();
                v.IdVenta = Convert.ToInt32(rs["id"].ToString());

                //guardar el detalle de la venta
                DetalleVentaSQL dvm = new DetalleVentaSQL();
                foreach (DetalleVenta dv in v.LstDetalle)
                {
                    dvm.Agregar(v,dv);
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

                db2.conn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;
        }

        public int AgregarVentaObra(Venta v)
        {
            DBConexion db = new DBConexion();
            int k = 0;

            db.cmd.CommandText = "INSERT INTO Venta(monto,descuento,IGV,ptosGanados,estado,idCliente,tipoVenta,fechaReg,fechaDespacho,tipoDocPago,idUsuario,codTarjeta)" +
                                  " VALUES (@monto,@descuento,@IGV,@ptsGanados,@estado,@idCliente,@tipoVenta,@fechaReg,@fechaDespacho,@tipoDocPago,@idUsuario,@codTarjeta)";
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
                db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                db.conn.Close();

                //sacar la ultima insersion
                DBConexion db2 = new DBConexion();

                db2.cmd.CommandText = "SELECT MAX (idVenta) as id FROM Venta";

                db2.conn.Open();
                SqlDataReader rs = db2.cmd.ExecuteReader();
                rs.Read();
                v.IdVenta = Convert.ToInt32(rs["id"].ToString());

                //generar orden de despacho
                InsertaOrdenDeDespacho(v);

                //guardar el detalle de la venta
                DetalleVentaSQL dvm = new DetalleVentaSQL();
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

                db2.conn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;
        }

        private int InsertaOrdenDeDespacho(Venta v)
        {
            DBConexion db = new DBConexion();
            int k = 0;

            db.cmd.CommandText = "INSERT INTO OrdenDespacho(idVenta,fechaDespacho,estado)" +
                                  " VALUES (@idVenta,@fechaDespacho,@estado)";
            db.cmd.Parameters.AddWithValue("@idVenta", v.IdVenta);
            db.cmd.Parameters.AddWithValue("@fechaDespacho", v.FechaDespacho);
            db.cmd.Parameters.AddWithValue("@estado", 1);

            try
            {
                db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                db.conn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }

            return k;
        }

        public void AgregarPagoVenta(Venta v, VentaPago vp)
        {
            DBConexion db = new DBConexion();
            db.cmd.CommandText = "INSERT INTO Pago(monto,idVenta,idModoPago) VALUES(@monto,@idVenta,@idModoPago)";
            db.cmd.Parameters.AddWithValue("@monto", vp.Monto);
            db.cmd.Parameters.AddWithValue("@idVenta", v.IdVenta);
            db.cmd.Parameters.AddWithValue("@idModoPago", vp.IdModoPago);
            try
            {
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.conn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

        }

        public object Buscar(params object[] filters)
        {
            DBConexion db = new DBConexion();
            List<Venta> lista = new List<Venta>();
            lista = null;

            string where = "WHERE 1=1 ";

            if (!String.IsNullOrEmpty((string)filters[0]))
            {
                where = where + " AND numDocPago = @numDocPago ";
                db.cmd.Parameters.AddWithValue("@numDocPago", (string)filters[1]);
            }

            /*if (!String.IsNullOrEmpty((string)filters[1]))
            {
                where = where + " AND dni=@dniRuc ";
                db.cmd.Parameters.AddWithValue("@dniRuc", "");
            }*/
            if (!String.IsNullOrEmpty((string)filters[2]))
            {
                where = where + " AND monto>=@montoMin ";
                db.cmd.Parameters.AddWithValue("@montoMin", (string)filters[3]);
            }
            if (!String.IsNullOrEmpty((string)filters[3]))
            {
                where = where + " AND monto<=@montoMax ";
                db.cmd.Parameters.AddWithValue("@montoMax", (string)filters[4]);
            }


            db.cmd.CommandText = "select * from Venta" + where;
            
            try
            {
                db.conn.Open();
                SqlDataReader reader = db.cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (lista == null) lista = new List<Venta>();
                    Venta v = new Venta();
                    v.IdVenta = Convert.ToInt32(reader["idVenta"].ToString());
                    v.CodTarjeta = Convert.ToInt32(reader["codTarjeta"].ToString());
                    v.Descuento = Convert.ToDouble(reader["descuento"].ToString());
                    v.Estado = Convert.ToInt32(reader["estado"].ToString());
                    v.FechaMod = Convert.ToDateTime(reader["fechaMod"].ToString());
                    v.FechaReg = Convert.ToDateTime(reader["fechaReg"].ToString());
                    v.IdUsuario = Convert.ToInt32(reader["idUsuario"].ToString());
                    v.Igv = Convert.ToInt32(reader["igv"].ToString());
                    v.NumDocPago = reader["numDocPago"].ToString();
                    v.PtosGanados = Convert.ToInt32(reader["ptosGanados"].ToString());
                    v.Monto = (double)reader["monto"];
                    v.TipoDocPago = reader["tipoDocPago"].ToString();
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

        public int AgregarSinCliente(Venta v)
        {
            DBConexion db = new DBConexion();
            int k = 0;

            db.cmd.CommandText = "INSERT INTO Venta(monto,descuento,IGV,ptosGanados,estado,tipoVenta,fechaReg,tipoDocPago,idUsuario)" +
                                  " VALUES (@monto,@descuento,@IGV,@ptsGanados,@estado,@tipoVenta,@fechaReg,@tipoDocPago,@idUsuario)";
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
                db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                db.conn.Close();

                //sacar la ultima insersion
                DBConexion db2 = new DBConexion();

                db2.cmd.CommandText = "SELECT MAX (idVenta) as id FROM Venta";

                db2.conn.Open();
                SqlDataReader rs = db2.cmd.ExecuteReader();
                rs.Read();
                v.IdVenta = Convert.ToInt32(rs["id"].ToString());

                //guardar el detalle de la venta
                DetalleVentaSQL dvm = new DetalleVentaSQL();
                foreach (DetalleVenta dv in v.LstDetalle)
                {
                    dvm.Agregar(v, dv);
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

                db2.conn.Close();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }

            return k;
        }

        private void actualizaNumDocServicios(Venta v)
        {
            DBConexion db = new DBConexion();

            db.cmd.CommandText = "UPDATE Venta SET numDocPagoServicio=@numDocPagoServicio WHERE idVenta=@idVenta";
            db.cmd.Parameters.AddWithValue("@numDocPagoServicio", v.NumDocPagoServicio);
            db.cmd.Parameters.AddWithValue("@idVenta", v.IdVenta);

            try
            {
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.cmd.Parameters.Clear();
                db.conn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void actualizaNumDocProductos(Venta v)
        {
            DBConexion db = new DBConexion();

            db.cmd.CommandText = "UPDATE Venta SET numDocPagoProducto=@numDocPagoProducto WHERE idVenta=@idVenta";
            db.cmd.Parameters.AddWithValue("@numDocPagoProducto", v.NumDocPago);
            db.cmd.Parameters.AddWithValue("@idVenta", v.IdVenta);

            try
            {
                db.conn.Open();
                db.cmd.ExecuteNonQuery();
                db.cmd.Parameters.Clear();
                db.conn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private string SacaNumDocPago(Venta v,int tipo)
        {
            string numDoc;
            int num = numero(v,tipo);
            if (v.TipoDocPago.Equals("Boleta"))
                numDoc = "BOL00" + num;
            else
                numDoc = "FAC00" + num;
            return numDoc;
        }

        private int numero(Venta v,int tipo)
        {
            DBConexion db2 = new DBConexion();
            int k = 0;
            db2.cmd.CommandText = "SELECT count(*) as C FROM Venta WHERE tipoDocPago=@tipoDocPago";
            db2.cmd.Parameters.AddWithValue("@tipoDocPago", v.TipoDocPago);
            try
            {
                db2.conn.Open();
                SqlDataReader rs = db2.cmd.ExecuteReader();
                rs.Read();
                k = Convert.ToInt32(rs["C"].ToString());
                db2.conn.Close();
                
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }

            DBConexion db = new DBConexion();
            int k2 = 0;
            db.cmd.CommandText = "SELECT count(*) as C FROM Venta WHERE tipoDocPago=@tipoDocPago AND numDocPagoServicio is not null";
            db.cmd.Parameters.AddWithValue("@tipoDocPago", v.TipoDocPago);
            try
            {
                db.conn.Open();
                SqlDataReader rs = db.cmd.ExecuteReader();
                rs.Read();
                k2 = Convert.ToInt32(rs["C"].ToString());
                db.conn.Close();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }

            if (tipo == 2) return k + 1 + k2;
            else return k + k2;
        }

    }
}
