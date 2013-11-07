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

        internal ProductoCant LstProductos
        {
            get { return lstProductos; }
            set { lstProductos = value; }
        }


    }
}
