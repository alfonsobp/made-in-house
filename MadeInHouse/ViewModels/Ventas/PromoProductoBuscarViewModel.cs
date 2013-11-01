using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;

namespace MadeInHouse.ViewModels.Ventas
{
    class PromoProductoBuscarViewModel:Screen
    {
        private MyWindowManager win = new MyWindowManager();

        public void AbrirNuevaPromoProducto()
        {
            win.ShowWindow(new Ventas.PromoProductoRegistrarViewModel());
        }

        public void AbrirEditarPromoProducto()
        {
            win.ShowWindow(new Ventas.PromoProductoEditarViewModel());
        }
    }
}
