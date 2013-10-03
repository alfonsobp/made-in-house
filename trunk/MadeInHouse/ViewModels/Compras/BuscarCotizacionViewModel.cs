using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.ViewModels.Compras
{
    class BuscarCotizacionViewModel:Screen
    {
        private WindowManager win = new WindowManager();

        public void NuevaCotizacion()
        {


            Compras.NuevaCotizacionViewModel obj = new Compras.NuevaCotizacionViewModel { DisplayName = "Nueva Cotizacion" };
            win.ShowWindow(obj);
        }
        public void EditarCotizacion()
        {


            Compras.NuevaCotizacionViewModel obj = new Compras.NuevaCotizacionViewModel { DisplayName = "Editar Cotizacion" };
            win.ShowWindow(obj);
        }

    }
}
