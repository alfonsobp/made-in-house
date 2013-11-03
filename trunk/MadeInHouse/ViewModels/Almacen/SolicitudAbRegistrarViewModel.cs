using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;
using MadeInHouse.Models.Almacen;


namespace MadeInHouse.ViewModels.Almacen
{
    class SolicitudAbRegistrarViewModel : PropertyChangedBase
    {
        private MyWindowManager win = new MyWindowManager();

        #region atributos

        public int idAlmacen = 4;

        private List<ProductoxAlmacen> _productos;

        public List<ProductoxAlmacen> Productos
        {
            get
            {
                return _productos;
            }
            set
            {
                if (_productos == value)
                {
                    return;
                }
                _productos = value;
                NotifyOfPropertyChange(() => Productos);
            }
        }

        #endregion

        #region metodos

        public void SeleccionarProductos()
        {
            win.ShowDialog(new ProductoBuscarViewModel(this));
        }

        public void GuardarProductos()
        {

        }

        public void addProducto(ProductoxAlmacen prod)
        {
            List<ProductoxAlmacen> aux = new List<ProductoxAlmacen>();
            if (Productos != null)
            {
                foreach (ProductoxAlmacen item in Productos)
                {
                    if (prod.IdProducto != item.IdProducto)
                        aux.Add(item);
                }
            }
            aux.Add(prod);
            Productos = aux;
        }



        #endregion
    }
}