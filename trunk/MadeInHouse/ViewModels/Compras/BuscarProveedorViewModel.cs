using Caliburn.Micro;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.Models.Compras;
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





    }
}
