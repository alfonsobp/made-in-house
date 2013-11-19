using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.ViewModels.Layouts;
using System.Threading;

namespace MadeInHouse.ViewModels.Almacen
{
    [Export(typeof(SolicitudAbRegistrarViewModel))]
    class SolicitudAbAtenderViewModel : Screen
    {
        #region constructor

        [ImportingConstructor]
        public SolicitudAbAtenderViewModel(IWindowManager windowmanager, SolicitudAbRegistrarViewModel window, int idSolicitud)
        {
            _windowManager = windowmanager;
            this.idSolicitud = idSolicitud;
            this.window = window;
            AbastecimientoSQL abSQL = new AbastecimientoSQL();
            TiendaSQL tiendaSQL = new TiendaSQL();
            idTienda = tiendaSQL.obtenerTienda(Int32.Parse(Thread.CurrentPrincipal.Identity.Name));
            Productos = abSQL.buscarProductosAbastecimiento(idSolicitud,idTienda);
        }

        #endregion

        #region atributos

        private readonly IWindowManager _windowManager;

        public int idSolicitud { get; set; }
        public int idTienda { get; set; }
        public SolicitudAbRegistrarViewModel window { get; set; }

        private List<AbastecimientoProducto> _productos;
        public List<AbastecimientoProducto> Productos
        {
            get { return _productos; }
            set
            {
                if (_productos == value) return;
                _productos = value;
                NotifyOfPropertyChange(() => Productos);
            }
        }

        #endregion

        #region metodos

        public void GuardarAtencion()
        {
            AbastecimientoModel solModel = new AbastecimientoModel();
            int idUsuario = Int32.Parse(Thread.CurrentPrincipal.Identity.Name);
            string message = solModel.atenderAbastecimiento(idUsuario, idSolicitud, Productos);
            _windowManager.ShowDialog(new AlertViewModel(_windowManager, message));
            this.TryClose();
        }

        #endregion
    }
}