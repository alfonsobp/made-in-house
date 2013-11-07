using Caliburn.Micro;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.Models.Compras;
using MadeInHouse.Validacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.ViewModels.Compras
{
    class BuscarProductoProveedorViewModel:Screen
    {
        List<ProveedorxProducto> lst;

        public List<ProveedorxProducto> Lst
        {
            get { return lst; }
            set { lst = value; NotifyOfPropertyChange("Lst"); }
        }

        string cantidad="";

        public string Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; NotifyOfPropertyChange("Cantidad"); }
        }

        generarOrdenCompraViewModel model = null;
             
        public BuscarProductoProveedorViewModel(generarOrdenCompraViewModel model,int idProveedor ) {

            this.model = model;
            Lst = new ProveedorxProductoSQL().Buscar(idProveedor) as List<ProveedorxProducto>;
    
        }

        ProveedorxProducto productoSelected = null;

        public ProveedorxProducto ProductoSelected
        {
            get { return productoSelected; }
            set { productoSelected = value; NotifyOfPropertyChange("ProductoSelected"); }
        }


        public void Agregar() {

            if (ProductoSelected != null)
            {
                if (model.Validar(ProductoSelected))
                {

                    TryClose();
                }
                
                
            }
       }


    }
}
