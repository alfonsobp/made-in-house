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
        private WindowManager win = new WindowManager();

        public void NuevaSolicitud()
        {


            Compras.mantenerSolicitudesAdquisicionViewModel obj = new Compras.mantenerSolicitudesAdquisicionViewModel { DisplayName = "Nueva Solicitud" };
            win.ShowWindow(obj);
        }
        public void EditarSolicitud()
        {


            Compras.mantenerSolicitudesAdquisicionViewModel obj = new Compras.mantenerSolicitudesAdquisicionViewModel { DisplayName = "Editar Solicitud" };
            win.ShowWindow(obj);
        }
    }
}
