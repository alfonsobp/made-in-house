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
        public ReporteMov()
        {
            InitializeComponent();
            _reportViewer.Load += ReportViewer_Load;
        }

        private bool _isReportViewerLoaded;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                ReportViewer.DataSetMov dataset = new ReportViewer.DataSetMov();

                dataset.BeginInit();

                reportDataSource1.Name = "DataSet1"; //Name of the report dataset in our .RDLC file
                reportDataSource1.Value = dataset.DataTable1;
                this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                this._reportViewer.LocalReport.ReportEmbeddedResource = "MadeInHouse.ReportViewer.ReportMov.rdlc";

                dataset.EndInit();

                //fill data into adventureWorksDataSet
             
                ReportViewer.DataSetMovTableAdapters.DataTable1TableAdapter adapter = new ReportViewer.DataSetMovTableAdapters.DataTable1TableAdapter();
                adapter.ClearBeforeFill = true;
                dataset.EnforceConstraints = false;
                adapter.Fill(dataset.DataTable1);

                _reportViewer.RefreshReport();

                _isReportViewerLoaded = true;
            }
        }
    }
}
