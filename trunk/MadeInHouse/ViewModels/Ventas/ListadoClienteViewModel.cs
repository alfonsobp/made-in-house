using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Ventas
{
    class ListadoClienteViewModel:Screen
    {
        private WindowManager win = new WindowManager();

        public void AbrirRegistrarcliente()
        {
            win.ShowWindow(new Ventas.RegistrarClienteViewModel { DisplayName = "Registrar Cliente" });
        }

        public void AbrirEditarcliente()
        {
            win.ShowWindow(new Ventas.EditarClienteViewModel { DisplayName = "Editar Cliente" });
        }
    }
}
