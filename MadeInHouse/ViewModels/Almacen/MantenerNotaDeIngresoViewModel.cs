using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerNotaDeIngresoViewModel:Screen
    {

        public void AbrirPosicionProducto()
        {

            WindowManager win = new WindowManager();
            Almacen.PosicionProductoViewModel abrirPosView = new Almacen.PosicionProductoViewModel();
            win.ShowWindow(abrirPosView);
        }


        public void AbrirListarOrdenesCompra()
        {

            WindowManager win = new WindowManager();
            Almacen.ListaOrdenCompraViewModel abrirListaOrden = new Almacen.ListaOrdenCompraViewModel ();
            win.ShowWindow(abrirListaOrden);
        }

    }
}
