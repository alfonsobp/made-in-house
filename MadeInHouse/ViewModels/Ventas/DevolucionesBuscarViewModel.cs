using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;
using System.ComponentModel.Composition;
using MadeInHouse.ViewModels.Almacen;
using MadeInHouse.Models.Ventas;
using MadeInHouse.DataObjects.Ventas;

namespace MadeInHouse.ViewModels.Ventas
{
    [Export(typeof(DevolucionesRegistrarViewModel))]
    class DevolucionesBuscarViewModel : PropertyChangedBase
    {
        #region constructores

        [ImportingConstructor]
        public DevolucionesBuscarViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
            CmbEstados = new List<EstadosDevolucion>();
            EstadosDevolucion est = new EstadosDevolucion();
            est.estado = 1;
            est.nomEstado = "Registrado";
            CmbEstados.Add(est);
            est = new EstadosDevolucion();
            est.estado = 2;
            est.nomEstado = "Ingresado";
            CmbEstados.Add(est);
            est = new EstadosDevolucion();
            est.estado = 3;
            est.nomEstado = "Anulado";
            CmbEstados.Add(est);
            est = new EstadosDevolucion();
            est.estado = 0;
            est.nomEstado = "Seleccionar estado";
            CmbEstados.Add(est);
        }

        public DevolucionesBuscarViewModel(IWindowManager windowmanager, MantenerNotaDeIngresoViewModel window, int accion) : this (windowmanager)
        {
            this.window = window;
            this.accion = accion;
        }

        /*public DevolucionesBuscarViewModel(Almacen.MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel, int accion):this()
        {
            // TODO: Complete member initialization
            this.mantenerNotaDeIngresoViewModel = mantenerNotaDeIngresoViewModel;
            this.Accion = accion;
        }*/

        #endregion

        #region atributos

        private readonly IWindowManager _windowManager;
        private object window;
        public Devolucion devolucionSel { get; set; }
        public int accion { get; set; }
        private int SelectedEstado { get; set; }

        //Filtros:
        private string notaCreditoHide;
        private string docPagoHide;
        private int estadoHide;
        private string registroDesdeHide;
        private string registroHastaHide;
        private string dniHide;

        private List<Devolucion> devoluciones;
        public List<Devolucion> Devoluciones
        {
            get { return devoluciones; }
            set
            {
                if (devoluciones == value) return;
                devoluciones = value;
                NotifyOfPropertyChange(() => Devoluciones);
            }
        }

        private List<EstadosDevolucion> cmbEstados;
        public List<EstadosDevolucion> CmbEstados
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

        #endregion

        #region metodos

        public void Acciones(object sender)
        {
            switch(accion){
                case 1:
                    AbrirDetalle();
                    break;
                default:
                    break;
            }
        }

        public void AbrirDetalle()
        {
            _windowManager.ShowWindow(new DevolucionesRegistrarViewModel(_windowManager, this, devolucionSel.IdDevolucion));
        }

        public void AbrirRegistrarDevolucion()
        {
            _windowManager.ShowWindow(new DevolucionesRegistrarViewModel(_windowManager, this));
        }

        public void RealizarBusqueda(string notaCredito, string docPago, int estado, string registroDesde, string registroHasta, string dni)
        {
            this.notaCreditoHide = notaCredito;
            this.docPagoHide = docPago;
            this.estadoHide = estado;
            this.registroDesdeHide = registroDesde;
            this.registroHastaHide = registroHasta;
            this.dniHide = dni;
            ActualizarTabla();
        }

        public void ActualizarTabla()
        {
            DevolucionSQL devSQL = new DevolucionSQL();
            Devoluciones = devSQL.buscarDevoluciones(notaCreditoHide, docPagoHide, estadoHide, registroDesdeHide, registroHastaHide, dniHide);
            NotifyOfPropertyChange("Devoluciones");
        }

        #endregion
    }

    class EstadosDevolucion
    {
        public int estado { get; set; }
        public string nomEstado { get; set; }
    }
}
