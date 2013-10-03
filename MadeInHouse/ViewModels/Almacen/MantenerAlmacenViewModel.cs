using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;

namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerAlmacenViewModel:Screen
    {

        MyWindowManager win = new MyWindowManager();
            public void AbrirMantenerZona(){

            
            Almacen.MantenerZonaViewModel  abrirZonaView = new Almacen.MantenerZonaViewModel();
            win.ShowWindow(abrirZonaView);
        }

    }
}
