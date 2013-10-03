using Caliburn.Micro;
using MadeInHouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.ViewModels.Compras
{
    class BuscarOrdenCompraViewModel:Screen
    {

        private MyWindowManager win = new MyWindowManager();

        public void NuevaOrden()
        {


            Compras.generarOrdenCompraViewModel obj = new Compras.generarOrdenCompraViewModel { DisplayName = "Nueva Orden de Compra" };
            win.ShowWindow(obj);
        }
        public void EditarOrden()
        {

            Compras.generarOrdenCompraViewModel obj = new Compras.generarOrdenCompraViewModel { DisplayName = "Editar Orden de Compra" };
            win.ShowWindow(obj);
        }


    }
}
