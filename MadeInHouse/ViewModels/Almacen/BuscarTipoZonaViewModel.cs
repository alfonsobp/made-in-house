using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Almacen
{
    class BuscarTipoZonaViewModel:Screen
    {
        private WindowManager win = new WindowManager();
        public void AbrirNuevaZona()
        {
            win.ShowWindow(new Almacen.MantenerTipoZonaViewModel { DisplayName = "Nuevo Tipo de Zona" });

        }

    }
}
