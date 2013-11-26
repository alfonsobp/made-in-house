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
using System.Windows;

namespace MadeInHouse.ViewModels.Almacen
{
    [Export(typeof(SolicitudAbListadoViewModel))]
    class SolicitudAbListadoViewModel : PropertyChangedBase
    {
        #region constructor

        [ImportingConstructor]
        public SolicitudAbListadoViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
        }

        public SolicitudAbListadoViewModel(MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel, int acciones)
        {
            // TODO: Complete member initialization
            this.MantenerNotaDeIngresoViewModel = mantenerNotaDeIngresoViewModel;
            this.Accion = acciones;
        }

        #endregion

        #region atributos

        private readonly IWindowManager _windowManager;

        private string registroDesdeHide;
        private string registroHastaHide;
        private int estadoHide;

        private List<Abastecimiento> solicitudes;
        private MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel;

        public MantenerNotaDeIngresoViewModel MantenerNotaDeIngresoViewModel
        {
            get { return mantenerNotaDeIngresoViewModel; }
            set { mantenerNotaDeIngresoViewModel = value; }
        }
        private int accion;

        public int Accion
        {
            get { return accion; }
            set { accion = value; }
        }
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

        public Abastecimiento abastecimientoSel { get; set; }

        #endregion

        #region metodos

        public void RealizarBusqueda(string registroDesde, string registroHasta)
        {
            this.registroDesdeHide = registroDesde;
            this.registroHastaHide = registroHasta;
            ActualizarTabla();
        }

        public void ActualizarTabla()
        {
            AbastecimientoSQL abaSQL = new AbastecimientoSQL();
            Solicitudes = abaSQL.buscarAbastecimientos(registroDesdeHide, registroHastaHide, estadoHide);
            NotifyOfPropertyChange("Solicitudes");
        }

        public void Acciones(object sender)
        {
            AbrirDetalle();
        }

        public void AbrirDetalle()
        {
            _windowManager.ShowWindow(new SolicitudAbRegistrarViewModel(_windowManager, this, abastecimientoSel.idSolicitudAB, abastecimientoSel.idTienda));
        }

        public void NuevaSolicitud()
        {
            _windowManager.ShowWindow(new SolicitudAbRegistrarViewModel(_windowManager, this, 0));
        }

        public void EditarSolicitud()
        {
            _windowManager.ShowWindow(new SolicitudAbRegistrarViewModel(_windowManager, this, abastecimientoSel.idSolicitudAB, abastecimientoSel.idTienda, true));
        }

        public void AnularSolicitud()
        {
            AbastecimientoSQL solSQL = new AbastecimientoSQL();
            int idUsuario = 17;
            string message = "No se pudo anular la solicitud";
            if (solSQL.eliminarAbastecimiento(abastecimientoSel.idSolicitudAB, idUsuario) > 0)
            {
                message = "La operacion fue exitosa";
            }
            _windowManager.ShowDialog(new AlertViewModel(_windowManager, message));
            ActualizarTabla();
        }

        #endregion
    }
}
