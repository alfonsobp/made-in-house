using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;

namespace MadeInHouse.ViewModels.Almacen
{
    class BuscarTipoZonaViewModel:Screen
    {
        private MyWindowManager win = new MyWindowManager();
        public void AbrirNuevaZona()
        {
            win.ShowWindow(new Almacen.MantenerTipoZonaViewModel());

        }

    }
}
