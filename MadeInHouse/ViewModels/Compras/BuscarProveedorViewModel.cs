using Caliburn.Micro;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.Models.Compras;
using MadeInHouse.ViewModels.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MadeInHouse.ViewModels.Compras
{
    class BuscarProveedorViewModel:Screen
    {

        List<PrecioProductoProveedor> lstProveedores;

        public List<PrecioProductoProveedor> LstProveedores
        {
            get { return lstProveedores; }
            set { lstProveedores = value; NotifyOfPropertyChange("LstProveedores"); }
        }

        SeleccionDeProveedoresViewModel model = null;

        public BuscarProveedorViewModel(int idProducto,SeleccionDeProveedoresViewModel model){
            this.model = model;
            LstProveedores = new ConsolidadoSQL().BuscarProveedores(idProducto);
        
        }

        PrecioProductoProveedor p;

        public void SelectedItemChanged(object sender)
        {
            p = ((sender as DataGrid).SelectedItem as PrecioProductoProveedor);

            if (model != null)
            {
                model.Prov = p.Prov;
                model.Costo = p.Costo;
                TryClose();
            }

           
        }

        private reporteComprasViewModel reporteComprasViewModel;

        public BuscarProveedorViewModel(reporteComprasViewModel reporteComprasViewModel, int accionVentana)
        {
            // TODO: Complete member initialization
            this.reporteComprasViewModel = reporteComprasViewModel;
            this.ventanaAccion = accionVentana;
            LstProveedores = new ConsolidadoSQL().BuscarProveedores();
        }

        private int ventanaAccion = 0;

        public void Acciones()
        {
            if (ventanaAccion == 0)
            {
                //Actualizar();
            }
            else if (ventanaAccion == 1)
            {

                if (this.reporteComprasViewModel != null)
                {
                    reporteComprasViewModel.Prov = p.Prov;
                    this.TryClose();
                }
            }
            else if (ventanaAccion == 2)
            {

            }
            else if (ventanaAccion == 3)
            {

            }
        }





    }
}
