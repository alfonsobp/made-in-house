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
using MadeInHouse.Views.RRHH;
using MadeInHouse.Models.RRHH;

namespace MadeInHouse.ViewModels.RRHH 
{
    public class RegistrarEmpleadoViewModel:Screen
    {
        private MyWindowManager win = new MyWindowManager();

        public void AbrirArmarHorario()
        {
            win.ShowWindow(new RRHH.ArmarHorarioViewModel { DisplayName = "Armar Horario" });
        }

        
        

    }
}
