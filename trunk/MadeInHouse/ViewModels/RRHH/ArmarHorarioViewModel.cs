using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.RRHH
{
    class ArmarHorarioViewModel : Screen
    {
        public void AbrirArmarHorario()
        {
            WindowManager win = new WindowManager();
            RRHH.ArmarHorarioViewModel obj = new RRHH.ArmarHorarioViewModel { DisplayName = "Mantener Empleado" };
            win.ShowWindow(obj);
        }
    }
}




