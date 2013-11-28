using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Compras;
using MadeInHouse.Models.Ventas;
using Microsoft.ReportingServices;
using Microsoft.Reporting;
using Microsoft.Reporting.WinForms;
using MadeInHouse.DataObjects.Reportes;


namespace MadeInHouse.Views.Reportes
{
    /// <summary>
    /// Lógica de interacción para reportViewerServicios.xaml
    /// </summary>
    public partial class reportViewerServicios : Window
    {
        private System.Windows.Forms.BindingSource ProductBindingSource;
        private System.ComponentModel.IContainer mform_components = null;
        List<Servi> lista;


        public reportViewerServicios()
        {
            InitializeComponent();

            FechaDesde.Text = "01/01/" + DateTime.Today.Year;
            FechaHasta.Text = "31/12/" + DateTime.Today.Year;
            List<Servicio> lstServicio = DataObjects.Reportes.reporteServiciosSQL.BuscarServicio();
            cmbServicio.ItemsSource = lstServicio;
            List<Cliente> lstCliente = DataObjects.Reportes.ReporteVentasSQL.BuscarCliente();
            cmbCliente.ItemsSource = lstCliente;
            List<Tienda> lstTienda = DataObjects.Reportes.ReporteVentasSQL.BuscarTienda();
            cmbTienda.ItemsSource = lstTienda;

            cmbTienda.SelectedIndex = 0;
            cmbCliente.SelectedIndex = 0;
            cmbServicio.SelectedIndex = 0;

            PrepareReport();
        }

        private void PrepareReport()
        {
            this.mform_components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();

            this.ProductBindingSource = new System.Windows.Forms.BindingSource(this.mform_components);
            ((System.ComponentModel.ISupportInitialize)(this.ProductBindingSource)).BeginInit();



            reportDataSource1.Name = "DataSet9";
            reportDataSource1.Value = this.ProductBindingSource;

            this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "MadeInHouse.ReportViewer.Report8.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(0, 0);

            ((System.ComponentModel.ISupportInitialize)(this.ProductBindingSource)).EndInit();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //
            this.ProductBindingSource.DataSource = lista;
            this.reportViewer.RefreshReport();
        } 

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Tienda tienda = cmbTienda.SelectedItem as Tienda;
            Cliente cliente = cmbCliente.SelectedItem as Cliente;
            Servicio servicio = cmbServicio.SelectedItem as Servicio;
            lista = DataObjects.Reportes.reporteServiciosSQL.BuscarServi(tienda.IdTienda, cliente.Id,servicio.IdServicio);
            Window_Loaded(sender, e);
        }
    }
}
