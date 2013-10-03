using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

using System.Windows;
using System.Windows.Input;
using System.ComponentModel.Composition;
using MadeInHouse.Models;

namespace MadeInHouse.ViewModels.RRHH
{
    public class ControlarAsistenciaEmpleadoViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private MyWindowManager win = new MyWindowManager();

        public void AbrirConfirmarAsistenciaEmpleado()
        {
            win.ShowWindow(new RRHH.ConfirmarAsistenciaEmpleadoViewModel { });
        }


        public void CerrarConfirmarAsistenciaEmpleado()
        {
            this.TryClose();
        }
    }
}
