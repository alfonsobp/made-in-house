using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Seguridad;
using MadeInHouse.ViewModels.Seguridad;
using System.ComponentModel.Composition;
using MadeInHouse.ViewModels.Layouts;

namespace MadeInHouse.ViewModels.Almacen
{
    [Export(typeof(MantenerOrdenDespachoViewModel))]
    class MantenerOrdenDespachoViewModel:Screen
    {
        #region constructor

        [ImportingConstructor]
        public MantenerOrdenDespachoViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
        }

        public MantenerOrdenDespachoViewModel(IWindowManager windowmanager, OrdenDespacho od) :this(windowmanager)
        {
            //IPHostEntry IPHost = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            TxtIdOrdenDespacho = od.IdOrdenDespacho.ToString();
            TxtIdVenta = od.Venta.IdVenta.ToString();
            TxtIdCliente = od.Venta.IdCliente.ToString();
            TxtNumDoc = od.Venta.NumDocPago;

            indicador = 2;

            Util util = new Util();
            LstEstado = util.ListarEstadosOrdenDespacho();
            LstEstadoValue = od.Estado;
            IsEnabledCodigo = false;

            ordenDespachoSeleccionado = od;

        }

        public void Guardar()
        {
            int k = 0;
            if (ordenDespachoSeleccionado != null)
            {
                ordenDespachoSeleccionado.Estado = LstEstadoValue;
                OrdenDespachoSQL odSQL = new OrdenDespachoSQL();
                k = odSQL.EditarOrdenDespacho(ordenDespachoSeleccionado);
                if (k == 1)
                    _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Orden de despacho actualizado con éxito"));
            }

        }

        private readonly IWindowManager _windowManager;
        #endregion

        #region atributos
        private OrdenDespacho ordenDespachoSeleccionado;
        private int indicador = 0;

        private string txtIdOrdenDespacho;
        public string TxtIdOrdenDespacho
        {
            get { return txtIdOrdenDespacho; }
            set { txtIdOrdenDespacho = value; NotifyOfPropertyChange(() => TxtIdOrdenDespacho); }
        }
        private string txtIdVenta;
        public string TxtIdVenta
        {
            get { return txtIdVenta; }
            set { txtIdVenta = value; NotifyOfPropertyChange(() => TxtIdVenta); }
        }

        private string txtIdCliente;
        public string TxtIdCliente
        {
            get { return txtIdCliente; }
            set { txtIdCliente = value; NotifyOfPropertyChange(() => TxtIdCliente); }
        }

        private string txtNumDoc;
        public string TxtNumDoc
        {
            get { return txtNumDoc; }
            set { txtNumDoc = value; NotifyOfPropertyChange(() => TxtNumDoc); }
        }

        private bool isEnabledCodigo;
        public bool IsEnabledCodigo
        {
            get { return isEnabledCodigo; }
            set
            {
                if (isEnabledCodigo == value)
                    return;
                isEnabledCodigo = value;
                NotifyOfPropertyChange("IsEnabledCodigo");
            }
        }

        private List<EstadoHabilitado> lstEstado;
        public List<EstadoHabilitado> LstEstado
        {
            get { return lstEstado; }
            set
            {
                if (this.lstEstado == value)
                {
                    return;
                }
                this.lstEstado = value;
                this.NotifyOfPropertyChange(() => this.lstEstado);
            }
        }

        private int lstEstadoValue;
        public int LstEstadoValue
        {
            get { return lstEstadoValue; }
            set { lstEstadoValue = value; NotifyOfPropertyChange(() => LstEstadoValue); }
        }

        #endregion


    }
}
