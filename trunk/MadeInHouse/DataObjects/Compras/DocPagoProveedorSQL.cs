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

            MessageBox.Show("Datos cotizacion: \nidProveedor = " + d.Proveedor.IdProveedor + "\nidOrden = " + d.OrdenCompra.IdOrden + "\nfecha Rec = " +
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
            int est = 2;
            string codDoc = "", codProveedor = "";

            if (filters.Length > 1 && filters.Length <= 5)
            {

                codDoc = Convert.ToString(filters[0]);
                codProveedor = Convert.ToString(filters[1]);
                string estado = Convert.ToString(filters[2]);
                DateTime fechaIni = Convert.ToDateTime(filters[3]);
                DateTime fechaFin = Convert.ToDateTime(filters[4]);

                if (codDoc != "")
                {
                    int idDoc = getIDfromCOD(codDoc);

                    MessageBox.Show("ID doc = " + idDoc);
                    where += " and idDoc = '" + idDoc.ToString() + "' ";
                }

                if (codProveedor != "")
                {
                    int idProveedor = getIDfromCOD(codProveedor);

                    MessageBox.Show("ID prov = " + idProveedor);
                    where += " and idProveedor = '" + idProveedor.ToString() + "' ";
                }

                if (estado != "")
                {
                    if (estado.Equals("COMPLETO"))
                        est = 2;
                    if (estado.Equals("PENDIENTE"))
                        est = 1;
                    if (estado.Equals("CANCELADO"))
                        est = 0;

                    where += " and estado = '" + est + "' ";
                }

                if ((fechaIni != null) && (filters[3] !=  null))
                {

                    where += " and CONVERT(DATE,'" + fechaIni.ToString("yyyy-MM-dd") + "')   <=  CONVERT(DATE,fechaFin,103) ";

                }

                if ((fechaFin != null) && (filters[4] != null))
                {

                    where += " and CONVERT(DATE,'" + fechaFin.ToString("yyyy-MM-dd") + "')   >=  CONVERT(DATE,fechaFin,103) ";
                }

            }

            // MessageBox.Show("SELECT * FROM Proveedor WHERE  estado = 1 " + where);


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
                    MessageBox.Show("idOrd = " + idOrd);
                    
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
            throw new NotImplementedException();
        }

        public int Eliminar(object entity)
        {
            throw new NotImplementedException();
        }

        public int getIDfromCOD(string cod)
        {
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
            MessageBox.Show("entro al getORD..");
            OrdenCompraSQL eM = new OrdenCompraSQL();
            List<OrdenCompra> lstO = eM.Buscar(null, null, 4, null, null) as List<OrdenCompra>;
            MessageBox.Show("paso el eM.Buscar()..");

            for (int i = 0; i < lstO.Count; i++)
                if (lstO[i].IdOrden == idOrd)
                {
                    MessageBox.Show(lstO[i].IdOrden + " = " + idOrd + " ? ");
                    return lstO[i];
                }

            return null;
        }
    }
}
