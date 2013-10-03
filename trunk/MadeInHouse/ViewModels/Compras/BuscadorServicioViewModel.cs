using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Compras
{
    class BuscadorServicioViewModel:Screen
    {
        private WindowManager win = new WindowManager();

        public void NuevoServicio()
        {


            Compras.agregarServicioViewModel obj = new Compras.agregarServicioViewModel { DisplayName = "Nuevo Servicio" };
            win.ShowWindow(obj);
        }
        public void EditarServicio()
        {


            Compras.agregarServicioViewModel obj = new Compras.agregarServicioViewModel { DisplayName = "Editar Servicio" };
            win.ShowWindow(obj);
        }

    }
}
