using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.RRHH
{
    class BuscadorEmpleadoViewModel : Screen
    {
        public void AbrirMantenerEmpleadoViewModel()
        {
            WindowManager win = new WindowManager();
            RRHH.MantenerEmpleadoViewModel obj = new RRHH.MantenerEmpleadoViewModel { DisplayName = "Mantener Empleado" };
            win.ShowWindow(obj);
        }
    }
}
