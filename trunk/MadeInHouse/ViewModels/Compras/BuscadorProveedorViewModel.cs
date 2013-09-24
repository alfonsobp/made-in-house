using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Compras
{
    class BuscadorProveedorViewModel:Screen
    {
        public void AbrirMantenerProveedorViewModel()
        {

            WindowManager win = new WindowManager();
            Compras.MantenerProveedorViewModel obj = new Compras.MantenerProveedorViewModel { DisplayName = "Mantener Proveedor" };
            win.ShowWindow(obj);
        }


    }
}
