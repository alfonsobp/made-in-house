using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Compras
{
    class ListadoServicioViewModel : PropertyChangedBase
    {
        private WindowManager win = new WindowManager();

        public void AbrirAgregarServicio()
        {
            win.ShowWindow(new Compras.agregarServicioViewModel { DisplayName = "Agregar Servicio" });
        }
        public void AbrirEditarServicio()
        {
            win.ShowWindow(new Compras.editarServicioViewModel { DisplayName = "Editar Servicio" });
        }
    }
}
