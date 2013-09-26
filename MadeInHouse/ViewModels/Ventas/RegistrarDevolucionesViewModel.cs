using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Ventas
{
    class RegistrarDevolucionesViewModel : PropertyChangedBase
    {
        private WindowManager win = new WindowManager();

        public void AbrirDetalleVenta()
        {
            win.ShowWindow(new Ventas.DetalleVentaViewModel());
        }
    }
}
