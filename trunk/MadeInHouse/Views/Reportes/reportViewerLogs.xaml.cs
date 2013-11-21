﻿using System;
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
using Microsoft.ReportingServices;
using Microsoft.Reporting;
using Microsoft.Reporting.WinForms;
using MadeInHouse.Models.Seguridad;
namespace MadeInHouse.Views.Reportes
{
    /// <summary>
    /// Lógica de interacción para reportViewerLogs.xaml
    /// </summary>
    public partial class reportViewerLogs : Window
    {
        List<Log> lista;
        private System.Windows.Forms.BindingSource ProductBindingSource;
        private System.ComponentModel.IContainer mform_components = null;

        public reportViewerLogs(List<Log> aux)
        {
            InitializeComponent();
            lista = aux;
            PrepareReport();
        }

        private void PrepareReport()
        {
            this.mform_components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();

            this.ProductBindingSource = new System.Windows.Forms.BindingSource(this.mform_components);
            ((System.ComponentModel.ISupportInitialize)(this.ProductBindingSource)).BeginInit();



            reportDataSource1.Name = "DataSet5";
            reportDataSource1.Value = this.ProductBindingSource;

            this.viewerInstance2.LocalReport.DataSources.Add(reportDataSource1);
            this.viewerInstance2.LocalReport.ReportEmbeddedResource = "MadeInHouse.ReportViewer.Report4.rdlc";
            this.viewerInstance2.Location = new System.Drawing.Point(0, 0);

            ((System.ComponentModel.ISupportInitialize)(this.ProductBindingSource)).EndInit();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ProductBindingSource.DataSource = lista;
            this.viewerInstance2.RefreshReport();
        } 


    }
}
