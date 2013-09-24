using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MadeInHouse.ViewModels.Ventas
{
    class ListadoVentaViewModel : PropertyChangedBase
    {
        private WindowManager win = new WindowManager();

        public void AbrirRegistrarVenta()
        {
            win.ShowWindow(new Ventas.RegistrarVentaViewModel { DisplayName = "Registrar Venta" });
        }
        public void AbrirEditarVenta()
        {
            win.ShowWindow(new Ventas.EditarVentaViewModel { DisplayName = "Editar Venta" });
        }
        public void AbrirEliminarVenta()
        {
            win.ShowWindow(new Ventas.EliminarVentaViewModel { DisplayName = "Anular Venta" });
        }
    }
}
