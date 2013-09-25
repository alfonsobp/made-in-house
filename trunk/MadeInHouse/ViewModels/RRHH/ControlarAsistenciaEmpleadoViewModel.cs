using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.RRHH
{
    class ControlarAsistenciaEmpleadoViewModel:Screen
    {
        public void AbrirConfirmarAsistenciaEmpleado()
        {
            WindowManager win = new WindowManager();
            RRHH.ConfirmarAsistenciaEmpleadoViewModel obj = new RRHH.ConfirmarAsistenciaEmpleadoViewModel {};
            win.ShowWindow(obj);
        }


        public void CerrarConfirmarAsistenciaEmpleado()
        {
            this.TryClose();
        }
    }
}
