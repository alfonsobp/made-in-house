﻿using Caliburn.Micro;
using MadeInHouse.DataObjects;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.Dictionary;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.ViewModels.Compras
{
    class MedioPagoViewModel:Screen
    {

        List<Proveedor> lstProveedor;

        public List<Proveedor> LstProveedor
        {
            get { return lstProveedor; }
            set { lstProveedor = value; NotifyOfPropertyChange("LstProveedor"); }
        }

        List<Consolidado> lstConsolidado;

        public List<Consolidado> LstConsolidado
        {
            get { return lstConsolidado; }
            set { lstConsolidado = value; NotifyOfPropertyChange("LstConsolidado"); }
        }

        int idAlmacen;

        public int IdAlmacen
        {
            get { return idAlmacen; }
            set { idAlmacen = value; NotifyOfPropertyChange("IdAlmacen"); }
        }

        string codAlmacen;

        public string CodAlmacen
        {
            get { return codAlmacen; }
            set { codAlmacen = value; NotifyOfPropertyChange("CodAlmacen"); }
        }
        SeleccionDeProveedoresViewModel m;
        List<int> Solicitudes;

    
        public MedioPagoViewModel(List<Proveedor> lstProveedor, List<Consolidado> lstConsolidado, int idAlmacen,SeleccionDeProveedoresViewModel m) {
            
            this.IdAlmacen = idAlmacen;
            this.LstConsolidado = lstConsolidado;
            this.LstProveedor = lstProveedor;
            CodAlmacen = "ALM-" + (idAlmacen + 10000000);
            Solicitudes = m.Solicitudes;
            this.m = m;
        }

        public void Guardar(){

            OrdenCompraSQL oSQL = new OrdenCompraSQL();
            OrdenCompraxProductoSQL opSQL = new OrdenCompraxProductoSQL();
            UtilesSQL u = new UtilesSQL();
            SolicitudAdquisicionSQL sSQL = new SolicitudAdquisicionSQL();
       foreach (Proveedor p in LstProveedor){

           OrdenCompra o = new OrdenCompra(idAlmacen, p, "Entregar a la brevedad posible");
           o.Estado = 2;
           
           oSQL.Agregar(o);

           int idOrden = u.ObtenerMaximoID("OrdenCompra", "idOrden");
           o.IdOrden = idOrden;
           foreach (Consolidado c in LstConsolidado) {

               if (c.Prov.IdProveedor == p.IdProveedor)
               {
                   ProductoxOrdenCompra po = new ProductoxOrdenCompra(c, idOrden);                
                   opSQL.Agregar(po);
                   o.LstProducto.Add(po);
               }

           }

           Enviar(o);
           


           foreach (int idSol in Solicitudes)
           {
               oSQL.relacionarOrden(idOrden, idSol);
           }


       }

       foreach (int idSol in Solicitudes)
       {
           sSQL.TerminarSolicitudes(idAlmacen, idSol);
       }
       MessageBox.Show( "Fueron generadas satisfactoriamente las Orden de compra","OBSERVACION", MessageBoxButton.OK, MessageBoxImage.Information);
       m.TryClose();
         
            
        }

        public void Enviar(OrdenCompra o) {



            string numOC = "OC-" + 1000000 + o.IdOrden;
                GenerarPDF pdf = new GenerarPDF();
                Correo c = new Correo();
                //m.coloma@pucp.pe
                string path = "\\" + numOC + ".pdf";
               
                string body = formato(o).ToString();
                string msg = "<html><body>Estimados :<br> Se adjunta la Orden de compra , Atenderla porfavor. <br>Saludos.<br>MadeInHouse <br>Seccion Compras </body></html>"; ;
                pdf.createPDF(body, path, false);
                c.EnviarCorreo("ORDEN DE COMPRA AL " + DateTime.Now.ToString(), o.Proveedor.Email, msg, Environment.CurrentDirectory + path);
                pdf.Borrar(Environment.CurrentDirectory + path);
              
        }

        public string formato(OrdenCompra O)
        {
            OrdenCompraSQL osql = new OrdenCompraSQL();
            Almacenes a = osql.getAlmacenCentral();

            string numOC = "OC-" + 1000000 + O.IdOrden;
            string content = @"<HTML><BODY>";
            content += "<p align='center'> MadeInHouse  S.A. <br><br> ";
            content += "< Ruc. 99999999999 <br><br> ";
            content += "  Av. Priority N° xxx - San Miguel - Lima<br><br>";
            content += " Telf: 999-9999<br><br";
            content += "  www.MadeInHouse.com Email: info@mih.com<br><br>";
            content += "ORDEN DE COMPRA  Nro " +  numOC + " </p><br><br>";
            content += "<br><br>";
            content += "Proveedor : " + O.Proveedor.RazonSocial + "<br><br>";
            content += "Fecha de pedido : " + O.FechaSinAtencion.ToString() + "<br><br>";
            content += "Terminos de entrega : Entrega en Almacen central de la Empresa <br><br>";
            content += "Lugar de entrega : " + a.Direccion + "<br><br>";
            content += "Moneda de pago : Nuevos Soles <br><br>";
            content += "Terminos de entrega : Entrega en Almacen central de la Empresa <br><br>";
            content += "Sirvase por este medio suministrar los siguientes articulos <br><br>";
            content += "<table border = 1 ><tr><th>NRO</th><th>ARTICULO</th><th>PRECIO UNITARIO S/.</th>" +
                        "<th>CANTIDAD</th><th>PRECIO TOTAL S/.</th><tr>";
            double sumaAporte = 0;
            int i = 1;
            foreach (ProductoxOrdenCompra o in O.LstProducto)
            {

                int cantidad = Convert.ToInt32(o.Cantidad);
                double parcial = o.PrecioUnitario * cantidad;
                content += "<tr><td>" + i.ToString() + "</td>" +
                               "<td>" + o.Producto.Nombre + "</td>" +
                               "<td>" + o.PrecioUnitario.ToString() + "</td>" +
                               "<td>" + o.Cantidad.ToString() + "</td>" +
                "<td>" + parcial.ToString() + "</td></tr>";
                i++;
                sumaAporte += parcial;
            }

            content += "<tr><td colspan = 4 > TOTAL S/.</td><td>" + sumaAporte.ToString() + "</td> </tr></table>";
            content += "<br><br>";
            content += "Porfavor,Tomar en cuenta las cotizaciones respectivas a la fecha. <br><br>";
            content += "Observaciones :" + O.Observaciones;
            content += "</BODY></HTML>";

            return content;
        }

    }
}
