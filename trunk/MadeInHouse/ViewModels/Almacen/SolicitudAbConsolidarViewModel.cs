using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Ventas;
using MadeInHouse.DataObjects.Ventas;
using MadeInHouse.ViewModels.Layouts;

namespace MadeInHouse.ViewModels.Almacen
{
    [Export(typeof(SolicitudAbConsolidarViewModel))]
    class SolicitudAbConsolidarViewModel : PropertyChangedBase
    {
        #region constructores

        [ImportingConstructor]
        public SolicitudAbConsolidarViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
            TiendaSQL tSQL = new TiendaSQL();
            CmbTiendas = tSQL.BuscarTienda();
            Tienda ti = new Tienda();
            ti.IdTienda = -1;
            ti.Nombre = "Seleccionar tienda";
            CmbTiendas.Insert(0, ti);
        }

        #endregion

        #region atributos

        private readonly IWindowManager _windowManager;
        public Tienda tiendaSel { get; set; }
        public int SelectedIndex { get; set; }

        private List<Tienda> cmbTiendas;
        public List<Tienda> CmbTiendas
        {
            get { return cmbTiendas; }
            set
            {
                if (this.cmbTiendas == value) return;
                cmbTiendas = value;
                NotifyOfPropertyChange(() => CmbTiendas);
            }
        }

        private int selectedTienda;
        public int SelectedTienda
        {
            get { return selectedTienda; }
            set
            {
                if (this.selectedTienda == value) return;
                selectedTienda = value;
                ActualizarTabla();
                NotifyOfPropertyChange(() => SelectedTienda);
            }
        }

        private List<Abastecimiento> solicitudes;
        public List<Abastecimiento> Solicitudes
        {
            get { return solicitudes; }
            set
            {
                if (solicitudes == value) return;
                solicitudes = value;
                NotifyOfPropertyChange(() => Solicitudes);
            }
        }

        #endregion

        #region metodos

        public void ActualizarTabla()
        {
            AbastecimientoSQL abaSQL = new AbastecimientoSQL();
            if (SelectedTienda > 0)
                Solicitudes = abaSQL.buscarAbastecimientos(null, null, 3, SelectedTienda);
            NotifyOfPropertyChange("Solicitudes");
        }

        public void ConsolidarSolicitud()
        {
            AbastecimientoModel abaModel = new AbastecimientoModel();
            string message = abaModel.consolidarSolicitudes(Solicitudes);
            NotifyOfPropertyChange("Solicitudes");
            _windowManager.ShowDialog(new AlertViewModel(_windowManager, message));
            ActualizarTabla();
        }

        #endregion
    }
}
