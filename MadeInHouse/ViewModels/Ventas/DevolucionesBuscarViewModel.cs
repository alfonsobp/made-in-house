using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;

namespace MadeInHouse.ViewModels.Ventas
{
    class DevolucionesBuscarViewModel : PropertyChangedBase
    {
        private MyWindowManager win = new MyWindowManager();
        private Almacen.MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel;
        private int accion;

        public int Accion
        {
            get { return accion; }
            set { accion = value; }
        }

        public DevolucionesBuscarViewModel() { 
        
        }
        public DevolucionesBuscarViewModel(Almacen.MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel, int accion):this()
        {
            // TODO: Complete member initialization
            this.mantenerNotaDeIngresoViewModel = mantenerNotaDeIngresoViewModel;
            this.Accion = accion;
        }

        public void AbrirRegistrarDevolucion()
        {
            win.ShowWindow(new Ventas.DevolucionesRegistrarViewModel());
        }
        public void AbrirEditarDevolucion()
        {
            win.ShowWindow(new Ventas.DevolucionesRegistrarViewModel());
        }
    }
}
