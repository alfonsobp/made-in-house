using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Ventas
{
    class ListadoPromoProductoViewModel:Screen
    {
        private WindowManager win = new WindowManager();

        public void AbrirNuevaPromoProducto()
        {
            win.ShowWindow(new Ventas.RegistrarPromoProductoViewModel { DisplayName = "Nueva Promoción de Producto" });
        }

        public void AbrirEditarPromoProducto()
        {
            win.ShowWindow(new Ventas.EditarPromoProductoViewModel { DisplayName = "Edición de Promociones" });
        }
    }
}
