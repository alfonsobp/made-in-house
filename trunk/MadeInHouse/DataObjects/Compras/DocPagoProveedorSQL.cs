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
    class DocPagoProveedorSQL: EntitySQL
    {
        public int Agregar(object entity)
        {
            DBConexion db = new DBConexion();
            int k = 0;
            DocPagoProveedor d = entity as DocPagoProveedor;

            MessageBox.Show("Datos Documento: \nidProveedor = " + d.Proveedor.IdProveedor + "\nidOrden = " + d.OrdenCompra.IdOrden + "\nfecha Rec = " +
                            d.FechaRecepcion + "\nTotal Bruto = " + d.TotalBruto + "\ndescuentos = " + d.Descuentos + "\nIGV = " + d.Igv + "\ncant Prod = " +
                            d.CantProductos + "\nmonto Tot = " + d.MontoTotal + "\nobservaciones = " + d.Observaciones + "\nsaldo = " + d.SaldoPagado + 
                            "\nFecha Ven = " + d.FechaVencimiento);

            db.cmd.CommandText = "INSERT INTO DocPagoProveedor(idProveedor,idOrden,fechaRecepcion,fechaVencimiento,totalBruto,descuentos,IGV,estado,cantProductos,montoTotal,observaciones,fechaPago,saldoPagado) " +
                                 "VALUES (@idProveedor,@idOrden,@fechaRecepcion,@fechaVencimiento,@totalBruto,@descuentos,@IGV,@estado,@cantProductos,@montoTotal,@observaciones,@fechaPago,@saldoPagado)";

            db.cmd.Parameters.AddWithValue("@idProveedor", d.Proveedor.IdProveedor);
            db.cmd.Parameters.AddWithValue("@idOrden", d.OrdenCompra.IdOrden);
            db.cmd.Parameters.AddWithValue("@fechaRecepcion", d.FechaRecepcion);
            db.cmd.Parameters.AddWithValue("@fechaVencimiento", d.FechaVencimiento);
            db.cmd.Parameters.AddWithValue("@totalBruto", d.TotalBruto);
            db.cmd.Parameters.AddWithValue("@descuentos", d.Descuentos);
            db.cmd.Parameters.AddWithValue("@IGV", d.Igv);
            db.cmd.Parameters.AddWithValue("@estado", 1);
            db.cmd.Parameters.AddWithValue("@cantProductos", d.CantProductos);
            db.cmd.Parameters.AddWithValue("@montoTotal", d.MontoTotal);
            db.cmd.Parameters.AddWithValue("@observaciones", d.Observaciones);
            db.cmd.Parameters.AddWithValue("@fechaPago", DateTime.Now);
            db.cmd.Parameters.AddWithValue("@saldoPagado", d.SaldoPagado);

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
            List<DocPagoProveedor> lstDocs = new List<DocPagoProveedor>();
            DBConexion db = new DBConexion();
            SqlDataReader reader;

            String where = "";

            if (filters.Length >= 1 && filters.Length <= 3)
            {

                string codDoc = Convert.ToString(filters[0]);
                string codProveedor = Convert.ToString(filters[1]);
                string estado = Convert.ToString(filters[2]);

                if (!String.IsNullOrEmpty(codDoc))
                {
                    //MessageBox.Show("En el buscador..codDoc = " + codDoc);
                    int idDoc = getIDfromCOD(codDoc);

                    //MessageBox.Show("ID docPago = " + idDoc);
                    where += " and idDocPago = '" + idDoc.ToString() + "' ";
                }

                if (!String.IsNullOrEmpty(codProveedor))
                {
                    int idProveedor = getIDfromCOD(codProveedor);

                    //MessageBox.Show("ID prov = " + idProveedor);
                    where += " and idProveedor = '" + idProveedor.ToString() + "' ";
                }

                if (!String.IsNullOrEmpty(estado))
                {
                    int est = 1;

                    if (estado.Equals("COMPLETO"))
                        est = 2;
                    if (estado.Equals("PENDIENTE"))
                        est = 1;
                    if (estado.Equals("CANCELADO"))
                        est = 0;

                    where += " and estado = '" + est + "' ";
                }

            }

            // MessageBox.Show("SELECT * FROM Proveedor WHERE  estado = 1 " + where);


            //MessageBox.Show("SELECT * FROM DocPagoProveedor  WHERE   estado >= 0   " + where);
            db.cmd.CommandText = "SELECT * FROM DocPagoProveedor  WHERE   estado >= 0   " + where;
            db.cmd.CommandType = CommandType.Text;
            db.cmd.Connection = db.conn;



            try
            {
                db.conn.Open();

                reader = db.cmd.ExecuteReader();


                while (reader.Read())
                {

                    DocPagoProveedor d = new DocPagoProveedor();
                    d.Proveedor = new Proveedor();
                    d.OrdenCompra = new OrdenCompra();

                    d.IdDocPago = Convert.ToInt32(reader["idDocPago"].ToString());
                    d.CodDoc = "DP-" + (1000000 + d.IdDocPago).ToString();

                    int idProv = Convert.ToInt32(reader["idProveedor"].ToString());
                    d.Proveedor = getPROVfromID(idProv); 

                    int idOrd = Convert.ToInt32(reader["idOrden"].ToString());
                    
                    d.OrdenCompra = getORDfromID(idOrd);

                    d.FechaRecepcion = Convert.ToDateTime(reader["fechaRecepcion"].ToString());
                    d.FechaVencimiento = Convert.ToDateTime(reader["fechaVencimiento"].ToString());
                    d.TotalBruto = Convert.ToDouble(reader["totalBruto"].ToString());
                    d.Descuentos = Convert.ToDouble(reader["descuentos"].ToString());
                    d.Igv = Convert.ToDouble(reader["IGV"].ToString());
                    d.CantProductos = Convert.ToInt32(reader["cantProductos"].ToString());
                    d.MontoTotal = Convert.ToDouble(reader["montoTotal"].ToString());
                    d.Observaciones = reader["observaciones"].ToString();
                    d.FechaPago = Convert.ToDateTime(reader["fechaPago"].ToString());
                    d.SaldoPagado = Convert.ToDouble(reader["saldoPagado"].ToString());
                    d.Estado = Convert.ToInt32(reader["estado"].ToString());

                    lstDocs.Add(d);
                }

                db.conn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }


            return lstDocs;
        }

        public int Actualizar(object entity)
        {
            DBConexion db = new DBConexion();
            DocPagoProveedor d = entity as DocPagoProveedor;
            int k = 0;

            db.cmd.CommandText = "UPDATE DocPagoProveedor " +
                                 "SET saldoPagado=@saldoPagado " +
                                 "WHERE idDocPago= @idDocPago ";

            db.cmd.Parameters.AddWithValue("@idDocPago", d.IdDocPago);
            db.cmd.Parameters.AddWithValue("@saldoPagado", d.SaldoPagado);

            try
            {
                db.conn.Open();
                k = db.cmd.ExecuteNonQuery();
                MessageBox.Show("Actualizacion de saldo completa \nDoc Pago = " + d.CodDoc + "\nNuevo Saldo = " + d.SaldoPagado);
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

        public int getIDfromCOD(string cod)
        {
            MessageBox.Show("Inside getIDfromCOD: " + cod);
            int finalVal = 0, factor = 1;
            int last = cod.Length - 1;


            for (int i = 0; i < 7; i++)
            {
                int val = Convert.ToInt32(cod[last].ToString());
                finalVal = factor * val + finalVal;
                factor = factor * 10; last = last - 1;
            }

            return (finalVal - 1000000);
        }

        public Proveedor getPROVfromID(int idProv)
        {
            ProveedorSQL eM = new ProveedorSQL();
            List<Proveedor> lstP = eM.Buscar() as List<Proveedor>;

            for (int i = 0; i < lstP.Count; i++)
                if (lstP[i].IdProveedor == idProv)
                    return lstP[i];

            return null;
        }

        public OrdenCompra getORDfromID(int idOrd)
        {
            OrdenCompraSQL eM = new OrdenCompraSQL();
            List<OrdenCompra> lstO = eM.Buscar(null, null, 4, null, null) as List<OrdenCompra>;

            for (int i = 0; i < lstO.Count; i++)
                if (lstO[i].IdOrden == idOrd)
                {
                    return lstO[i];
                }

            return null;
        }
    }
}
