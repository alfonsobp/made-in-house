using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;

namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerNotaDeIngresoViewModel:PropertyChangedBase
    {
        MyWindowManager win = new MyWindowManager();
        public void AbrirPosicionProducto()
        {

            
            Almacen.PosicionProductoViewModel abrirPosView = new Almacen.PosicionProductoViewModel();
            win.ShowWindow(abrirPosView);
        }


        public void AbrirListarOrdenesCompra()
        {

            Almacen.ListaOrdenCompraViewModel abrirListaOrden = new Almacen.ListaOrdenCompraViewModel();
            win.ShowWindow(abrirListaOrden);
        }

    }
}
