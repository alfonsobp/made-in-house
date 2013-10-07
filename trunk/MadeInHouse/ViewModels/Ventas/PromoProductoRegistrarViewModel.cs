using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Ventas
{
    class PromoProductoRegistrarViewModel : Screen
    {
        private WindowManager win = new WindowManager();

        public void AbrirBuscarProducto()
        {
            win.ShowWindow(new Almacen.ProductoBuscarViewModel());
        }
    }
}
