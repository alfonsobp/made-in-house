using Caliburn.Micro;
using MadeInHouse.DataObjects;
using MadeInHouse.DataObjects.Compras;
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
           
           oSQL.Agregar(o);

           int idOrden = u.ObtenerMaximoID("OrdenCompra", "idOrden");

           foreach (Consolidado c in LstConsolidado) {

               if (c.Prov.IdProveedor == p.IdProveedor)
               {
                   ProductoxOrdenCompra po = new ProductoxOrdenCompra(c, idOrden);

                   opSQL.Agregar(po);
               }

           }


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

    }
}
