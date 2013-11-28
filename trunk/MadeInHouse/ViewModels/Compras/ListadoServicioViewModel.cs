using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;

namespace MadeInHouse.ViewModels.Compras
{
    class ListadoServicioViewModel : PropertyChangedBase
    {
        private MyWindowManager win = new MyWindowManager();

        public void AbrirAgregarServicio()
        {
            win.ShowWindow(new Compras.agregarServicioViewModel(win));
        }
        public void AbrirEditarServicio()
        {
            win.ShowWindow(new Compras.editarServicioViewModel());
        }
    }
}
