using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;

namespace MadeInHouse.ViewModels.Almacen
{
    class BuscarZonaViewModel:Screen
    {

        private MyWindowManager win = new MyWindowManager();

        public void AbrirZonaDistribucion()
        {
            win.ShowWindow(new Almacen.ZonaDistribucionViewModel());

        }

    }
}
