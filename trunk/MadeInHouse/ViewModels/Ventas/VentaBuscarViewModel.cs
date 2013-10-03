using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Ventas
{
    class VentaBuscarViewModel : PropertyChangedBase
    {
        private WindowManager win = new WindowManager();

        public void AbrirRegistrarVenta()
        {
            win.ShowWindow(new Ventas.VentaRegistrarViewModel ());
        }
        public void AbrirEditarVenta()
        {
            win.ShowWindow(new Ventas.VentaEditarViewModel());
        }
    }
}
