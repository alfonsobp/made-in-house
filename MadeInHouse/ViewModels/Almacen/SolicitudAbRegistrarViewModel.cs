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
using System.ComponentModel.Composition;
using MadeInHouse.ViewModels.Layouts;

namespace MadeInHouse.ViewModels.Almacen
{
	[Export(typeof(SolicitudAbRegistrarViewModel))]
    class SolicitudAbRegistrarViewModel : PropertyChangedBase
    {
		private readonly IWindowManager _windowManager;

        #region constructor

        [ImportingConstructor]
        public SolicitudAbRegistrarViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
        }

        #endregion
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
            _windowManager.ShowWindow(new ProductoBuscarViewModel(this));
        }

        public void GuardarSolicitud()
        {
            int idUsuario = 17;
            AbastecimientoModel solModel = new AbastecimientoModel();
            string message = solModel.registrarAbastecimiento(idUsuario, Productos);
            _windowManager.ShowDialog(new AlertViewModel(_windowManager, message));
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