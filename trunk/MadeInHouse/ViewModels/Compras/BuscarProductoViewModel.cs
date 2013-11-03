using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.ViewModels.Compras
{


    class BuscarProductoViewModel :PropertyChangedBase
    {
        string txtNombre;

        public string TxtNombre
        {
            get { return txtNombre; }
            set { txtNombre = value; NotifyOfPropertyChange("TxtNombre"); }
        }

        List<Producto> lstProductos;

        public List<Producto> LstProductos
        {
            get { return lstProductos; }
            set { lstProductos = value; NotifyOfPropertyChange("LstProductos"); }
        }


        public void Actualizar(){

            LstProductos = new ProductoSQL().BuscarProducto(txtNombre);
        
        }


    }
}
