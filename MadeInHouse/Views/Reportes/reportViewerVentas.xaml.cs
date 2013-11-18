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
using MadeInHouse.Models.Ventas;
using Microsoft.ReportingServices;
using Microsoft.Reporting;
using Microsoft.Reporting.WinForms;

namespace MadeInHouse.Views.Reportes
{
    
    public partial class reportViewerVentas : Window
    {
        private System.Windows.Forms.BindingSource ProductBindingSource;
        private System.ComponentModel.IContainer mform_components = null;
        List<VentaAux> lista;

        public reportViewerVentas(List<VentaAux> aux)
        {
            InitializeComponent();
            PrepareReport();
            
            lista = aux;
            
        }

        private void PrepareReport()
        {
            this.mform_components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();

            this.ProductBindingSource = new System.Windows.Forms.BindingSource(this.mform_components);
            ((System.ComponentModel.ISupportInitialize)(this.ProductBindingSource)).BeginInit();



            reportDataSource1.Name = "DataSet2";
            reportDataSource1.Value = this.ProductBindingSource;

            this.viewerInstance.LocalReport.DataSources.Add(reportDataSource1);
            this.viewerInstance.LocalReport.ReportEmbeddedResource = "MadeInHouse.ReportViewer.Report1.rdlc";
            this.viewerInstance.Location = new System.Drawing.Point(0,0);

            ((System.ComponentModel.ISupportInitialize)(this.ProductBindingSource)).EndInit();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ProductBindingSource.DataSource = lista;
            this.viewerInstance.RefreshReport();
        } 
    }
}
