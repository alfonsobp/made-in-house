using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Ventas;
using MadeInHouse.DataObjects.Ventas;
using MadeInHouse.DataObjects.Almacen;
using System.Windows;
using System.Threading;
using System.ComponentModel.Composition;
using MadeInHouse.ViewModels.Layouts;

namespace MadeInHouse.ViewModels.Ventas
{
    [Export(typeof(DevolucionesRegistrarViewModel))]
    class DevolucionesRegistrarViewModel : Screen
    {
        #region constantes

        //Mantenimientos:
        const int DETALLE = 0;
        const int REGISTRO = 1;

        #endregion

        #region constructores

        [ImportingConstructor]
        public DevolucionesRegistrarViewModel(IWindowManager windowmanager, DevolucionesBuscarViewModel window, int idDevolucion = -1)
        {
            _windowManager = windowmanager;
            this.window = window;
            if (idDevolucion > 0)
            {
                this.idDevolucion = idDevolucion;
                this.ObtenerProductosVenta();
                IndMantenimiento = DETALLE;
                IsReadOnly = true;
                if ((new DevolucionSQL()).puedeAnular(idDevolucion))
                    CanDelete = Visibility.Visible;
                else
                    CanDelete = Visibility.Collapsed;
                CanSave = Visibility.Collapsed;
                ViewDni = Visibility.Collapsed;
            }
            else
            {
                IndMantenimiento = REGISTRO;
                IsReadOnly = false;
                CanDelete = Visibility.Collapsed;
                CanSave = Visibility.Visible;
                ViewDni = Visibility.Visible;
            }
        }

        #endregion

        #region atributos

        private readonly IWindowManager _windowManager;
        public int idDevolucion { get; set; }
        public DevolucionesBuscarViewModel window { get; set; }
        private Devolucion dev = new Devolucion();

        private int indMantenimiento;
        public int IndMantenimiento
        {
            get { return indMantenimiento; }
            set
            {
                if (indMantenimiento == value) return;
                indMantenimiento = value;
                NotifyOfPropertyChange(() => IndMantenimiento);
            }
        }

        private bool isReadOnly;
        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set
            {
                if (isReadOnly == value) return;
                isReadOnly = value;
                NotifyOfPropertyChange(() => IsReadOnly);
            }
        }

        private Visibility canDelete;
        public Visibility CanDelete
        {
            get { return canDelete; }
            set
            {
                if (canDelete == value) return;
                canDelete = value;
                NotifyOfPropertyChange(() => CanDelete);
            }
        }

        private Visibility canSave;
        public Visibility CanSave
        {
            get { return canSave; }
            set
            {
                if (canSave == value) return;
                canSave = value;
                NotifyOfPropertyChange(() => CanSave);
            }
        }

        private Visibility viewDni;
        public Visibility ViewDni
        {
            get { return viewDni; }
            set
            {
                if (viewDni == value) return;
                viewDni = value;
                NotifyOfPropertyChange(() => ViewDni);
            }
        }

        private List<DevolucionProducto> lstProductos;
        public List<DevolucionProducto> LstProductos
        {
            get { return lstProductos; }
            set
            {
                lstProductos = value;
                NotifyOfPropertyChange(() => LstProductos);
            }
        }

        private string docPago;
        public string DocPago
        {
            get { return docPago; }
            set
            {
                docPago = value;
                NotifyOfPropertyChange(() => DocPago);
            }
        }

        private string dni;
        public string DNI
        {
            get { return dni; }
            set
            {
                dni = value;
                NotifyOfPropertyChange(() => DNI);
            }
        }

        #endregion

        #region metodos
        
        public void ObtenerProductosVenta()
        {
            DevolucionSQL dSQL = new DevolucionSQL();
            if (idDevolucion > 0)
            {
                LstProductos = dSQL.BuscarProductos(-1, -1, null, idDevolucion);
                DocPago = LstProductos.First().DocPago;
            }
            else
            {
                dev.venta = dSQL.BuscarVenta(this.DocPago);
                DNI = dev.venta == null ? null : dev.venta.dni;
                if (dev.venta != null && dev.venta.IdVenta > 0)
                    LstProductos = dSQL.BuscarProductos(-1, dev.venta.IdVenta);
                else
                    LstProductos = null;
            }
        }

        public void AnularDevolucion()
        {
            DevolucionSQL dSQL = new DevolucionSQL();
            if (dSQL.puedeAnular(idDevolucion))
            {
                if (!dSQL.cambiarEstado(idDevolucion, 0))
                {
                    _windowManager.ShowDialog(new AlertViewModel(_windowManager, "No se pudo anular la devolución"));
                }
                if (window != null)
                    window.ActualizarTabla();
                ((Window)this.GetView()).Owner.Focus();
                TryClose();
            }
            else
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "La devolución ya se encuentra anulada"));
            }
        }

        public void GuardarDevolucion()
        {
            if (DocPago == null || DocPago == "" || lstProductos == null || lstProductos.Count == 0)
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "No ingreso documento de pago"));
                return;
            }
            foreach (DevolucionProducto prod in lstProductos)
            {
                if (prod.Devuelve + prod.Devuelto > prod.Cantidad)
                {
                    _windowManager.ShowDialog(new AlertViewModel(_windowManager, "No puede devolver mas de lo que se adquirió en la venta."));
                    return;
                }
            }
            DevolucionModel dModel = new DevolucionModel();
            DevolucionSQL dSQL = new DevolucionSQL();
            int num = dSQL.numDevoluciones();
            dev.archivo = dModel.GenerarNotaCredito(dev, num, lstProductos);
            dev.idUsuario = Int32.Parse(Thread.CurrentPrincipal.Identity.Name);
            string message = dModel.registrarDevolucion(dev, lstProductos);
            _windowManager.ShowDialog(new AlertViewModel(_windowManager, message));
        }

        #endregion
    }
}
