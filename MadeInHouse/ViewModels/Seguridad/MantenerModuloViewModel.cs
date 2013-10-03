using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Seguridad
{
    class MantenerModuloViewModel
    {
        public void AbrirRegistrarModulo()
        {
            WindowManager win = new WindowManager();
            Seguridad.RegistrarModuloViewModel obj = new Seguridad.RegistrarModuloViewModel {  };
            win.ShowWindow(obj);
        }
    }

}
