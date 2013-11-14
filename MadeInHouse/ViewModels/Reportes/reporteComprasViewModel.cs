using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;
using MadeInHouse.ViewModels.Compras;
using MadeInHouse.Models.Compras;
using System.Windows.Controls;

namespace MadeInHouse.ViewModels.Reportes
{
    class reporteComprasViewModel:Screen
    {

        private MyWindowManager win = new MyWindowManager();

        private DateTime fechaIni;

        public DateTime FechaIni
        {
            get { return fechaIni; }
            set { fechaIni = value; NotifyOfPropertyChange(() => FechaIni); }
        }

        private DateTime fechaFin;

        public DateTime FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; NotifyOfPropertyChange(() => FechaFin); }
        }

        private List<OrdenCompra> lstServiciorepor;

        public List<OrdenCompra> LstServiciorepor
        {
            get { return lstServiciorepor; }
            set { lstServiciorepor = value; NotifyOfPropertyChange(() => LstServiciorepor); }
        }

        private OrdenCompra ordenComprarepSeleccionado;

        public void SelectedItemChanged(object sender)
        {
            ordenComprarepSeleccionado = ((sender as DataGrid).SelectedItem as OrdenCompra);
        }

        private Proveedor prov;

        public Proveedor Prov
        {
            get { return prov; }
            set { prov = value; NotifyOfPropertyChange(() => Prov); TxtProveedor = prov.RazonSocial; }
        }

        public void BuscarProveedor()
        {
            MyWindowManager w = new MyWindowManager();
            w.ShowWindow(new BuscarProveedorViewModel(this, 1));
        }

        private String txtProveedor;

        public String TxtProveedor
        {
            get { return txtProveedor; }
            set { txtProveedor = value; NotifyOfPropertyChange(() => TxtProveedor); }
        }

        private OrdenCompra ordComp;

        public OrdenCompra OrdComp
        {
            get { return ordComp; }
            set { ordComp = value; NotifyOfPropertyChange(() => OrdComp); TxtOrdenCompra = ordComp.CodOrdenCompra; }
        }

        public void BuscarOrdenCompra()
        {
            MyWindowManager w = new MyWindowManager();
            w.ShowWindow(new BuscarOrdenCompraViewModel(this, 2));
        }

        private String txtOrdenCompra;

        public String TxtOrdenCompra
        {
            get { return txtOrdenCompra; }
            set { txtOrdenCompra = value; NotifyOfPropertyChange(() => TxtOrdenCompra); }
        }

/*
        public void ActualizarBusqueda()
        {
            this.GenerarReporte(cmbEstado);
        }*/

        public void LimpiarCriterios()
        {
            TxtOrdenCompra = "";
            TxtProveedor = "";
        }

        private Dictionary<string, int> estado = new Dictionary<string, int>()
        {
            { "Todos", -1 }, { "CANCELADA", 0 }, { "PRE EMITIDA", 1 }, { "EN EJECUCION", 2 }, { "FINALIZADA", 3 }
        };

        public BindableCollection<string> cmbEstado
        {
            get
            {
                return new BindableCollection<string>(estado.Keys);
            }
        }

        public void GenerarReporte(string cmbEstado)
        {
            lstServiciorepor = DataObjects.ReportesSQL.GenerarReporOrdenCompra(txtOrdenCompra, prov, estado[cmbEstado], fechaIni, fechaFin);
            NotifyOfPropertyChange("LstServiciorepor");
        }
    }
}
