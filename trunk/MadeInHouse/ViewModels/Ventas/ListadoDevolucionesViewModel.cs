﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Ventas
{
    class ListadoDevolucionesViewModel : PropertyChangedBase
    {
        private WindowManager win = new WindowManager();

        public void AbrirRegistrarDevolucion()
        {
            win.ShowWindow(new Ventas.RegistrarDevolucionesViewModel());
        }
        public void AbrirEditarDevolucion()
        {
            win.ShowWindow(new Ventas.RegistrarDevolucionesViewModel());
        }
    }
}
