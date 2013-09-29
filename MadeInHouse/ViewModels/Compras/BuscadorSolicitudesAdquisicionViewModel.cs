using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;


namespace MadeInHouse.ViewModels.Compras
{
    class BuscadorSolicitudesAdquisicionViewModel:Screen
    {
        public void AbrirMantenerSolicitudesAdquisicionViewModel()
        {

            WindowManager win = new WindowManager();
            Compras.mantenerSolicitudesAdquisicionViewModel obj = new Compras.mantenerSolicitudesAdquisicionViewModel { DisplayName = "Mantenimiento de Solicitudes de Adquisicion"};
            win.ShowWindow(obj);
        }
    }
}
