using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Seguridad
{
    class BuscadorUsuarioViewModel : Screen
    {
        public void AbrirMantenerUsuarioViewModel()
        {
            WindowManager win = new WindowManager();
            Seguridad.MantenerUsuarioViewModel obj = new Seguridad.MantenerUsuarioViewModel { DisplayName = "Mantener usuario" };
            win.ShowWindow(obj);
        }
    }
}