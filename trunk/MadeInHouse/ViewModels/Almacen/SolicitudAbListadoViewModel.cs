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
using MadeInHouse.Models;

namespace MadeInHouse.ViewModels.Almacen
{
    [Export(typeof(SolicitudAbListadoViewModel))]
    class SolicitudAbListadoViewModel : Screen
    {
        #region constructor

        [ImportingConstructor]
        public SolicitudAbListadoViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
            CmbEstados = new List<EstadosSolicitud>();
            SelectedIndex = 0;
            EstadosSolicitud est = new EstadosSolicitud();
            est.estado = -1;
            est.nomEstado = "Seleccionar estado";
            CmbEstados.Add(est);
            est = new EstadosSolicitud();
            est.estado = 0;
            est.nomEstado = "Anulado";
            CmbEstados.Add(est);
            est = new EstadosSolicitud();
            est.estado = 1;
            est.nomEstado = "Registrado";
            CmbEstados.Add(est);
            est = new EstadosSolicitud();
            est.estado = 2;
            est.nomEstado = "En revisión";
            CmbEstados.Add(est);
            est = new EstadosSolicitud();
            est.estado = 3;
            est.nomEstado = "Revisado";
            CmbEstados.Add(est);
            est = new EstadosSolicitud();
            est.estado = 4;
            est.nomEstado = "Consolidado";
            CmbEstados.Add(est);
            est = new EstadosSolicitud();
            est.estado = 5;
            est.nomEstado = "Atendido";
            CmbEstados.Add(est);
        }

        public SolicitudAbListadoViewModel(IWindowManager windowmanager, MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel, int acciones)
            : this(windowmanager)
        {
            // TODO: Complete member initialization
            this.MantenerNotaDeIngresoViewModel = mantenerNotaDeIngresoViewModel;
            this.Accion = acciones;
        }

        public SolicitudAbListadoViewModel(IWindowManager _windowManager, MantenerNotaDeSalidaViewModel mantenerNotaDeSalidaViewModel, int acciones)
        :this(_windowManager)
        {
            // TODO: Complete member initialization
            this.mantenerNotaDeSalidaViewModel = mantenerNotaDeSalidaViewModel;
            this.Accion = acciones;
        }

        #endregion

        #region atributos

        private readonly IWindowManager _windowManager;

        private string registroDesdeHide;
        private string registroHastaHide;
        private int estadoHide;
        public int SelectedEstado { get; set; }
        public int SelectedIndex { get; set; }

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

        private List<EstadosSolicitud> cmbEstados;
        public List<EstadosSolicitud> CmbEstados
        {
            get { return cmbEstados; }
            set
            {
                if (this.cmbEstados == value)
                {
                    return;
                }

                cmbEstados = value;
                NotifyOfPropertyChange(() => CmbEstados);
            }
        }

        private string registroDesde;
        public string RegistroDesde
        {
            get { return registroDesde; }
            set
            {
                if (this.registroDesde == value) return;
                registroDesde = value;
                NotifyOfPropertyChange(() => RegistroDesde);
            }
        }

        private string registroHasta;
        public string RegistroHasta
        {
            get { return registroHasta; }
            set
            {
                if (this.registroHasta == value) return;
                registroHasta = value;
                NotifyOfPropertyChange(() => RegistroHasta);
            }
        }

        private List<Abastecimiento> solicitudes;
        private MantenerNotaDeSalidaViewModel mantenerNotaDeSalidaViewModel;
        private int p;
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
            this.estadoHide = SelectedEstado;
            ActualizarTabla();
        }

        public void LimpiarBusqueda()
        {
            SelectedEstado = -1;
            SelectedIndex = 0;
            NotifyOfPropertyChange("SelectedIndex");
            RegistroDesde = "";
            RegistroHasta = "";
        }

        public void ActualizarTabla()
        {
            AbastecimientoSQL abaSQL = new AbastecimientoSQL();
            Solicitudes = abaSQL.buscarAbastecimientos(registroDesdeHide, registroHastaHide, estadoHide);
            NotifyOfPropertyChange("Solicitudes");
        }

        public void Acciones(object sender)
        {
            if (Accion == 1) {
                if (this.mantenerNotaDeIngresoViewModel != null)
                {
                   
//                    OrdenSelected.LstProducto = new OrdenCompraxProductoSQL().Buscar(OrdenSelected.IdOrden) as List<ProductoxOrdenCompra>;
                    mantenerNotaDeIngresoViewModel.TxtDoc ="SOLAB00000"+abastecimientoSel.idSolicitudAB.ToString();
                    mantenerNotaDeIngresoViewModel.TxtDocId = abastecimientoSel.idSolicitudAB;
                    mantenerNotaDeIngresoViewModel.SelectedSolicitud = abastecimientoSel;
                    this.TryClose();
                }
            }
            else
            {
                if (Accion == 2) {

                    if (this.mantenerNotaDeSalidaViewModel != null)
                    {

                        //OrdenSelected.LstProducto = new OrdenCompraxProductoSQL().Buscar(OrdenSelected.IdOrden) as List<ProductoxOrdenCompra>;
                        mantenerNotaDeSalidaViewModel.TxtDoc = "SOLAB00000" + abastecimientoSel.idSolicitudAB.ToString();
                        mantenerNotaDeSalidaViewModel.TxtDocId = abastecimientoSel.idSolicitudAB;
                        mantenerNotaDeSalidaViewModel.SelectedSolicitud = abastecimientoSel;
                        this.TryClose();
                    }

                }
                else AbrirDetalle();
            }
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

    class EstadosSolicitud
    {
        public int estado { get; set; }
        public string nomEstado { get; set; }
    }
}
