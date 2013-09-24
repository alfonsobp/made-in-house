using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerAlmacenViewModel:Screen
    {
        
            public void AbrirMantenerZona(){

            WindowManager win = new WindowManager();
            Almacen.MantenerZonaViewModel  abrirZonaView = new Almacen.MantenerZonaViewModel();
            win.ShowWindow(abrirZonaView);
        }

    }
}
