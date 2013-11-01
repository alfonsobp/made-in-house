﻿using System;
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
    public class MantenerEmpleadoViewModel : Conductor<IScreen>.Collection.OneActive
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




    }
}