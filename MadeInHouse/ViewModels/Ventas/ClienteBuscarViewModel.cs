using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Windows;
using MadeInHouse.Models;

namespace MadeInHouse.ViewModels.Ventas
{
    class ClienteBuscarViewModel : PropertyChangedBase
    {
        private MyWindowManager win = new MyWindowManager();

        public void AbrirRegistrarcliente()
        {
            win.ShowWindow(new Ventas.ClienteRegistrarViewModel());
        }

        public void AbrirEditarcliente()
        {
            win.ShowWindow(new Ventas.EditarClienteViewModel { DisplayName = "Editar Cliente" });
        }
    }
}
