using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Ventas
{
    class ListadoPromoServicioViewModel:Screen
    {
        private WindowManager win = new WindowManager();

        public void AbrirNuevaPromoServicio()
        {
            win.ShowWindow(new Ventas.RegistrarPromoServicioViewModel { DisplayName = "Nueva Promoción de Servicio" });
        }

        public void AbrirEditarPromoServicio()
        {
            win.ShowWindow(new Ventas.EditarPromoServicioViewModel { DisplayName = "Edición de Promociones" });
        }
    }
}
