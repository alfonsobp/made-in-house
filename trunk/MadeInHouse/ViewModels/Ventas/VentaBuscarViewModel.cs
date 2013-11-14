using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using MadeInHouse.Models;
using MadeInHouse.Models.Ventas;
using MadeInHouse.DataObjects.Ventas;

namespace MadeInHouse.ViewModels.Ventas
{
    class VentaBuscarViewModel : PropertyChangedBase
    {
        private MyWindowManager win = new MyWindowManager();

        public void AbrirRegistrarVenta()
        {
            win.ShowWindow(new Ventas.VentaRegistrarViewModel ());
        }
        public void AbrirEditarVenta()
        {
            win.ShowWindow(new Ventas.VentaEditarViewModel());
        }
        public void AbrirBuscarCliente()
        {
            win.ShowWindow(new Ventas.ClienteBuscarViewModel());
        }

        private Venta ventaSeleccionada;

        public void SelectedItemChanged(object sender)
        {
            ventaSeleccionada = ((sender as DataGrid).SelectedItem as Venta);
        }

        private DateTime fechaInicio = new DateTime(DateTime.Now.Year, 1, 1);

        public DateTime FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; NotifyOfPropertyChange(() => FechaInicio); }
        }


        private DateTime fechaFin = new DateTime(DateTime.Now.Year, 12, 31);

        public DateTime FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; NotifyOfPropertyChange(() => FechaFin); }
        }

        private string txtDocPago;

        public string TxtDocPago
        {
            get { return txtDocPago; }
            set { txtDocPago = value; NotifyOfPropertyChange(() => TxtDocPago); }
        }

        private string dniRuc;

        public string DniRuc
        {
            get { return dniRuc; }
            set { dniRuc = value; NotifyOfPropertyChange(() => DniRuc); }
        }

        private string txtCliente;

        public string TxtCliente
        {
            get { return txtCliente; }
            set { txtCliente = value; NotifyOfPropertyChange(() => TxtCliente); }
        }

        private string montoMin;

        public string MontoMin
        {
            get { return montoMin; }
            set { montoMin = value; NotifyOfPropertyChange(() => montoMin); }
        }

        private string montoMax;

        public string MontoMax
        {
            get { return montoMax; }
            set { montoMax = value; NotifyOfPropertyChange(() => montoMax); }
        }

        private List<Venta> lstVentas = null;

        public List<Venta> LstVentas
        {
            get { return lstVentas; }
            set { lstVentas = value; NotifyOfPropertyChange(() => LstVentas); }
        }

        public void BuscarVentas()
        {
            LstVentas = new VentaSQL().Buscar(TxtDocPago, DniRuc, MontoMin, MontoMax) as List<Venta>;
        }
    }
}
