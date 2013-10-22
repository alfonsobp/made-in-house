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

namespace MadeInHouse.ViewModels.Reportes
{
    class reporteServiciosViewModel:Screen
    {
        private MyWindowManager win = new MyWindowManager();

        private string fechaIni;

        public string FechaIni
        {
            get { return fechaIni; }
            set { fechaIni = value; NotifyOfPropertyChange(() => FechaIni); }
        }

        private string fechaFin;

        public string FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; NotifyOfPropertyChange(() => FechaFin); }
        }

        private List<String> lstSede;

        public List<String> LstSede
        {
            get { return lstSede; }
            set { lstSede = value; NotifyOfPropertyChange(() => LstSede); }
        }

        private List<String> lstProveedores;

        public List<String> LstProveedores
        {
            get { return lstProveedores; }
            set { lstProveedores = value; NotifyOfPropertyChange(() => LstProveedores); }
        }

        private List<String> lstCategoria;

        public List<String> LstCategoria
        {
            get { return lstCategoria; }
            set { lstCategoria = value; NotifyOfPropertyChange(() => LstCategoria); }
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

        public void test()
        {
            MessageBox.Show("El servicio tiene Proveedor = " + serviciorepSeleccionado.Proveedor + " , nombre = " + serviciorepSeleccionado.Nombre +
                            " , Categoria = " + serviciorepSeleccionado.Categoria);
        }

        public void GenerarReporte()
        {
            lstServiciorepor = DataObjects.ReportesSQL.GenerarReporServicios(null, null, null, null, null);
            NotifyOfPropertyChange("LstServiciorepor");
        }
    }
}
