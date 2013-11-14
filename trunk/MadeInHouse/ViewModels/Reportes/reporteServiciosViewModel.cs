using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Reportes;
using MadeInHouse.Models;
using MadeInHouse.Views.Reportes;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using MadeInHouse.Models.Almacen;
using MadeInHouse.ViewModels.Almacen;
using MadeInHouse.Models.Ventas;
using MadeInHouse.ViewModels.Ventas;

namespace MadeInHouse.ViewModels.Reportes
{
    class reporteServiciosViewModel:Screen
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

        private List<Serviciorepor> lstServiciorepor;

        public List<Serviciorepor> LstServiciorepor 
        {
            get { return lstServiciorepor; }
            set { lstServiciorepor = value; NotifyOfPropertyChange(() => LstServiciorepor); }
        }

        private Serviciorepor serviciorepSeleccionado;

        public void SelectedItemChanged(object sender)
        {
            serviciorepSeleccionado = ((sender as DataGrid).SelectedItem as Serviciorepor);
        }


        private String txtTienda;

        public String TxtTienda
        {
            get { return txtTienda; }
            set { txtTienda = value; NotifyOfPropertyChange(() => TxtTienda); }
        }

        private Tienda tien;

        public Tienda Tien
        {
            get { return tien; }
            set { tien = value; NotifyOfPropertyChange(() => Tien); TxtTienda = tien.Nombre; }
        }

        public void BuscarTienda()
        {
            MyWindowManager w = new MyWindowManager();
            w.ShowWindow(new BuscarTiendaViewModel(this, 2));
        }

        private String txtCliente;

        public String TxtCliente
        {
            get { return txtCliente; }
            set { txtCliente = value; NotifyOfPropertyChange(() => TxtCliente); }
        }

        private Tarjeta cli;

        public Tarjeta Cli
        {
            get { return cli; }
            set { cli = value; NotifyOfPropertyChange(() => Cli); TxtCliente = cli.Cliente.NombreCompleto; }
        }

        public void BuscarCliente()
        {
            MyWindowManager w = new MyWindowManager();
            w.ShowWindow(new ClienteBuscarViewModel(this, 2));
        }

        private Double txtMonto;

        public Double TxtMonto
        {
            get { return txtMonto; }
            set { txtMonto = value; NotifyOfPropertyChange(() => TxtMonto); }
        }

        public void GenerarReporte()
        {
            double mt = 0;
            lstServiciorepor = DataObjects.ReportesSQL.GenerarReporServicios(txtTienda, txtCliente, fechaIni, fechaFin, ref mt);
            TxtMonto = mt;
            NotifyOfPropertyChange("LstServiciorepor");
        }

        public void ActualizarBusqueda()
        {
            this.GenerarReporte();
        }

        public void LimpiarCriterios()
        {
            TxtCliente="";
            TxtTienda="";
        }
    }
}
