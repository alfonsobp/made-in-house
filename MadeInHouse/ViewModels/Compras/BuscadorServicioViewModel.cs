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
        public void AbrirBuscadorServicioViewModel()
        {
            WindowManager win = new WindowManager();
            Compras.agregarServicioViewModel obj = new Compras.agregarServicioViewModel { DisplayName = "Nuevo Servicio" };
            win.ShowWindow(obj);
        }
    }
}
