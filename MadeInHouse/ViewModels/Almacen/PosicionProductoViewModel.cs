using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.ViewModels.Almacen
{
    class PosicionProductoViewModel : PropertyChangedBase
    {
        List<ProductoCant> lstProductos;

        public List<ProductoCant> LstProductos
        {
            get { return lstProductos; }
            set { lstProductos = value; }
        }
        private MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel;
        private int accion;

        public PosicionProductoViewModel(MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel, int accion):this()
        {
            // TODO: Complete member initialization
            this.mantenerNotaDeIngresoViewModel = mantenerNotaDeIngresoViewModel;
            this.accion = accion;
            this.LstProductos = mantenerNotaDeIngresoViewModel.LstProductos;
        }

        public PosicionProductoViewModel()
        {
            
            //mantenerNotaDeIngresoViewModel.Almacen.First();
            
            // TODO: Complete member initialization
        
        }



    }
}
