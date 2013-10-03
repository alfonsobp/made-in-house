using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Ventas
{
    class ProformaViewModel:Screen
    {
        private WindowManager win = new WindowManager();

        public void AbrirBuscarProducto()
        {
            win.ShowWindow(new Almacen.ProductoBuscarViewModel { DisplayName = "Buscar Producto" });
        }
    }
}
