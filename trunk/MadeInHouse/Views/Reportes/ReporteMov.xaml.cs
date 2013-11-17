using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;
using MadeInHouse.ReportViewer;
using Microsoft.Reporting.WinForms;
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

namespace MadeInHouse.Views.Reportes
{
    /// <summary>
    /// Lógica de interacción para ReporteStock.xaml
    /// </summary>
    public partial class ReporteMov : Window
    {
        public ReportViewer.DataSetMov dataset;
        public ReportViewer.DataSetMovTableAdapters.DataTable1TableAdapter adapter;

        public ReporteMov()
        {
            InitializeComponent();
            _reportViewer.Load += ReportViewer_Load;
           
            cmbProducto.DataContext = new ProductoSQL().BuscarProducto();
            cmbAlmacen.DataContext = new AlmacenSQL().BuscarAlmacen();
        }

        private bool _isReportViewerLoaded;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
             dataset = new ReportViewer.DataSetMov();

            dataset.BeginInit();

            reportDataSource1.Name = "DataSet1"; //Name of the report dataset in our .RDLC file
            reportDataSource1.Value = dataset.DataTable1;
            this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this._reportViewer.LocalReport.ReportEmbeddedResource = "MadeInHouse.ReportViewer.ReportMov.rdlc";

            dataset.EndInit();

            //fill data into adventureWorksDataSet

            adapter = new ReportViewer.DataSetMovTableAdapters.DataTable1TableAdapter();
            adapter.ClearBeforeFill = true;
            dataset.EnforceConstraints = false;

         
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (Validar())
            {
                Almacenes a = cmbAlmacen.SelectedItem as Almacenes;
                Producto p = cmbProducto.SelectedItem as Producto;

                adapter.Fill(dataset.DataTable1, fechaIni.SelectedDate, fechaFin.SelectedDate,Convert.ToInt32(a.IdAlmacen) , Convert.ToInt32(p.IdProducto ) );

                _reportViewer.RefreshReport();
            }
            
        }

        public bool Validar() {

            if (cmbAlmacen.SelectedItem == null)
                return false;

            if (cmbProducto.SelectedItem == null)
                return false;

            return true;
        }
    }
}
