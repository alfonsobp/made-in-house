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
using MadeInHouse.Models.RRHH;
using System.Windows.Controls;

namespace MadeInHouse.ViewModels.RRHH
{
    class MantenerEmpleadoViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private MyWindowManager win = new MyWindowManager();
        
        public void AbrirVerEmpleado()
        {
            win.ShowWindow(new RRHH.VerEmpleadoViewModel { });
        }

        public void AbrirEditarEmpleado()
        {
            win.ShowWindow(new RRHH.EditarEmpleadoViewModel { });
        }

        public void AbrirRegistrarEmpleado()
        {
            win.ShowWindow(new RRHH.RegistrarEmpleadoViewModel { });
        }

        Empleado selectedEmpleado;
        private Almacen.MantenerNotaDeSalidaViewModel mantenerNotaDeSalidaViewModel;
        private int accion;


        }


    
}
