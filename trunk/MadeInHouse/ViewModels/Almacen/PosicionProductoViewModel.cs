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
        ProductoCant lstProductos;
        private MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel;
        private int p;

        public PosicionProductoViewModel(MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel, int p):this()
        {
            // TODO: Complete member initialization
            this.mantenerNotaDeIngresoViewModel = mantenerNotaDeIngresoViewModel;
            this.p = p;
        }

        public PosicionProductoViewModel()
        {
            
            mantenerNotaDeIngresoViewModel.Almacen.First();
   
            // TODO: Complete member initialization
        
        }

        internal ProductoCant LstProductos
        {
            get { return lstProductos; }
            set { lstProductos = value; }
        }


    }
}
