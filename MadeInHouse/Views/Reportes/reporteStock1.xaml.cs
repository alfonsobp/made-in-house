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
using Microsoft.ReportingServices;
using Microsoft.Reporting;
using Microsoft.Reporting.WinForms;
using MadeInHouse.DataObjects.Reportes;
using MadeInHouse.Models.Almacen;

namespace MadeInHouse.Views.Reportes
{
    /// <summary>
    /// Lógica de interacción para reporteStock1.xaml
    /// </summary>
    public partial class reporteStock1 : Window
    {
        private System.Windows.Forms.BindingSource ProductBindingSource;
        private System.ComponentModel.IContainer mform_components = null;
        List<stock> lista;

        public reporteStock1()
        {
            InitializeComponent();
            cmbAlmacen.Items.Add("Todos");
            cmbAlmacen.Items.Add("Anaquel");
            cmbAlmacen.Items.Add("Deposito");
            cmbAlmacen.SelectedIndex = 0;
            cmbTienda.SelectedIndex = 0;
            List<Tienda> tiendas = DataObjects.Reportes.ReporteVentasSQL.BuscarTienda();
            Tienda central = new Tienda();
            central.Nombre = "ALMACEN CENTRAL";
            tiendas.Add(central);
            cmbTienda.ItemsSource = tiendas;
            PrepareReport();

        }

        private void PrepareReport()
        {
            this.mform_components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();

            this.ProductBindingSource = new System.Windows.Forms.BindingSource(this.mform_components);
            ((System.ComponentModel.ISupportInitialize)(this.ProductBindingSource)).BeginInit();



            reportDataSource1.Name = "DataSet6";
            reportDataSource1.Value = this.ProductBindingSource;

            this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "MadeInHouse.ReportViewer.Report5.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(0, 0);

            ((System.ComponentModel.ISupportInitialize)(this.ProductBindingSource)).EndInit();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ProductBindingSource.DataSource = lista;
            this.reportViewer.RefreshReport();
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Tienda i = cmbTienda.SelectedItem as Tienda;
            
            if(cmbAlmacen.SelectedItem.ToString() == "Anaquel")
            {
                lista = DataObjects.Reportes.reporteStock.BuscarStockAnaq(i.IdTienda);
            }
            if (cmbAlmacen.SelectedItem.ToString() == "Deposito")
            {
                List<stock> l1 = DataObjects.Reportes.reporteStock.BuscarStockAnaq(i.IdTienda);
                List<stock> l2 = DataObjects.Reportes.reporteStock.BuscarStockTienda(i.IdTienda);

                for (int j = 0; j < l1.Count; j++)
                {
                    for (int k = 0; k < l2.Count;k++ )
                    {
                        if (l1[j].IdProducto == l2[k].IdProducto) l2[k].Stockactual -= l1[j].Stockactual;
                    }
                }
                lista = l2;
            }
            if (cmbAlmacen.SelectedItem.ToString() == "Todos")
            {
                lista = DataObjects.Reportes.reporteStock.BuscarStockTienda(i.IdTienda);
            }
            if (i.Nombre == "ALMACEN CENTRAL")
            {
                lista = DataObjects.Reportes.reporteStock.BuscarStockCentral();
            }
            Window_Loaded(sender,e);

        }


    }
}
