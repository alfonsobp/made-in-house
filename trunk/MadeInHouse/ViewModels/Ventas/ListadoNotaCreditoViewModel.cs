using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Ventas
{
    class ListadoNotaCreditoViewModel:Screen
    {
        private WindowManager win = new WindowManager();

        public void AbrirRegistrarNotaCredito()
        {
            win.ShowWindow(new Ventas.RegistrarNotaCreditoViewModel { DisplayName = "Registrar Nota de Crédito" });
        }

        public void AbrirEditarNotaCredito()
        {
            win.ShowWindow(new Ventas.EditarNotaCreditoViewModel { DisplayName = "Editar Nota de Crédito" });
        }
    }
}
