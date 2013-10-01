using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;

namespace MadeInHouse.ViewModels.Ventas
{
    class ListadoPromoServicioViewModel:Screen
    {
        private MyWindowManager win = new MyWindowManager();

        public void AbrirNuevaPromoServicio()
        {
            win.ShowWindow(new Ventas.RegistrarPromoServicioViewModel());
        }

        public void AbrirEditarPromoServicio()
        {
            win.ShowWindow(new Ventas.EditarPromoServicioViewModel());
        }
    }
}
