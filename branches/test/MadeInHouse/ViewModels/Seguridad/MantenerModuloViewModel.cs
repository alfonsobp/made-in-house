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

namespace MadeInHouse.ViewModels.Seguridad
{
    class MantenerModuloViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private MyWindowManager win = new MyWindowManager();
        public void AbrirRegistrarModulo()
        {
            //win.ShowWindow(new Seguridad.RegistrarModuloViewModel { });
        }
    }

}
