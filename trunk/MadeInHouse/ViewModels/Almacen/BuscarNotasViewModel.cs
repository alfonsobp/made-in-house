using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Almacen
{
    class BuscarNotasViewModel:Screen
    {

        public void AbrirMantenerNotaDeIngreso()
        {
            WindowManager win = new WindowManager();
            Almacen.MantenerNotaDeIngresoViewModel abrirNotaIView = new Almacen.MantenerNotaDeIngresoViewModel { DisplayName = "Mantenimiento de notas de ingreso" };
            win.ShowWindow(abrirNotaIView);
            
        }

    }
}
