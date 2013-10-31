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
    class reporteVentasViewModel:Screen
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

        private List<String> listBoxSede2;

        public List<String> ListBoxSede2
        {
            get { return listBoxSede2; }
            set { listBoxSede2 = value; NotifyOfPropertyChange(() => ListBoxSede2); }
        }

        private List<String> listBoxProveedores2;

        public List<String> ListBoxProveedores2
        {
            get { return listBoxProveedores2; }
            set { listBoxProveedores2 = value; NotifyOfPropertyChange(() => ListBoxProveedores2); }
        }

        private List<String> listBoxCategorias2;

        public List<String> ListBoxCategorias2
        {
            get { return listBoxCategorias2; }
            set { listBoxCategorias2 = value; NotifyOfPropertyChange(() => ListBoxCategorias2); }
        }

        private List<Ventarepor> lstVentarepor;

        public List<Ventarepor> LstVentarepor
        {
            get { return lstVentarepor; }
            set { lstVentarepor = value; NotifyOfPropertyChange(() => LstVentarepor); }
        }

        private Ventarepor ventarepSeleccionado;

        public void SelectedItemChanged(object sender)
        {
            ventarepSeleccionado = ((sender as DataGrid).SelectedItem as Ventarepor);
        }

        public void test()
        {
            MessageBox.Show("La venta tiene Proveedor = " + ventarepSeleccionado.Proveedor + " , producto = " + ventarepSeleccionado.Producto +
                            " , MontovTotal = " + ventarepSeleccionado.MontoTotal);
        }

        public void GenerarReporte()
        {
            lstVentarepor = DataObjects.ReportesSQL.GenerarReporVentas(null, null, null, null, null);
            NotifyOfPropertyChange("LstVentarepor");
        }
    }
}
