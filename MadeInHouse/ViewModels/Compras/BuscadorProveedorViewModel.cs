using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;

namespace MadeInHouse.ViewModels.Compras
{
    class BuscadorProveedorViewModel:Screen
    {
        private MyWindowManager win = new MyWindowManager();

        public void NuevoProveedor()
        {

            
            Compras.MantenerProveedorViewModel obj = new Compras.MantenerProveedorViewModel { DisplayName = "Mantener Proveedor" };
            win.ShowWindow(obj);
        }
        public void EditarProveedor()
        {


            Compras.MantenerProveedorViewModel obj = new Compras.MantenerProveedorViewModel { DisplayName = "Mantener Proveedor" };
            win.ShowWindow(obj);
        }


    }
}
