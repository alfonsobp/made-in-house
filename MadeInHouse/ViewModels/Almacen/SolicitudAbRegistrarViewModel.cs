using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects.Almacen;
using System.Windows;


namespace MadeInHouse.ViewModels.Almacen
{
    class SolicitudAbRegistrarViewModel : Screen
    {
        private MyWindowManager win = new MyWindowManager();

        #region atributos

        public int idAlmacen = 4;

        private List<AbastecimientoProducto> _productos;

        public List<AbastecimientoProducto> Productos
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

        public void GuardarSolicitud()
        {
            int idUsuario = 17;
            AbastecimientoModel solModel = new AbastecimientoModel();
            if (solModel.registrarAbastecimiento(idUsuario, Productos))
            {
                TryClose();
                return;
            }
        }

        public void addProducto(AbastecimientoProducto prod)
        {
            List<AbastecimientoProducto> aux = new List<AbastecimientoProducto>();
            if (Productos != null)
            {
                foreach (AbastecimientoProducto item in Productos)
                {
                    if (prod.idProducto != item.idProducto)
                        aux.Add(item);
                }
            }
            aux.Add(prod);
            Productos = aux;
        }



        #endregion
    }
}