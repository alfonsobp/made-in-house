using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;
using MadeInHouse.ReportViewer;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial  class ReporteMov : Window
    {
 
        private List<Producto> listaProducto = new List<Producto>();
        private List<Producto> SelectedProducto = new List<Producto>();
        ProductoSQL pSQL = new ProductoSQL();
       
       

        public ReportViewer.DataSetMov dataset;
        public ReportViewer.DataSetMovTableAdapters.DataTable1TableAdapter adapter;

        public ReporteMov()
        {
            InitializeComponent();
            _reportViewer.Load += ReportViewer_Load;

            List<Almacenes> lstAlmacenes = new AlmacenSQL().BuscarAlmacen();
            cmbAlmacen.ItemsSource = lstAlmacenes;
            cmbAlmacen.SelectedItem = (lstAlmacenes == null) ? null : lstAlmacenes[0];
            listaProducto = pSQL.BuscarProducto();
            this.lstProducto.ItemsSource = listaProducto;
            this.lstSelectedProducto.ItemsSource = SelectedProducto;
            this.fechaFin.SelectedDate = new DateTime(DateTime.Now.Year, 12, 31);
            this.fechaIni.SelectedDate = new DateTime(DateTime.Now.Year, 1, 1);
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
            
                adapter.Fill(dataset.DataTable1, fechaIni.SelectedDate, fechaFin.SelectedDate,1 ,1 );

                _reportViewer.RefreshReport();
            }
            
        }

        private void OnCbObjectCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (SelectableObject<Almacenes> cbObject in cmbAlmacen.Items)
                if (cbObject.IsSelected)
                    sb.AppendFormat("{0}, ", cbObject.ObjectData.Nombre);
            cmbAlmacen.Text = sb.ToString().Trim().TrimEnd(',');
        }

        private void OnCbObjectsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            comboBox.SelectedItem = null;
        }


        public bool Validar() {

        
            return true;
        }

       
        private void lstProducto_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.lstProducto.SelectedItem !=  null ) {

                listaProducto = lstProducto.ItemsSource as List<Producto>;
                SelectedProducto = lstSelectedProducto.ItemsSource as List<Producto>;

                SelectedProducto.Add(lstProducto.SelectedItem as Producto);
                listaProducto.Remove(lstProducto.SelectedItem as Producto);
                listaProducto = new List<Producto>(listaProducto);
                SelectedProducto = new List<Producto>(SelectedProducto);
                lstProducto.ItemsSource = listaProducto;
                lstSelectedProducto.ItemsSource = SelectedProducto;
            
            }

        }

        private void lstSelectedProducto_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (this.lstSelectedProducto.SelectedItem != null)
            {

                listaProducto = lstProducto.ItemsSource as List<Producto>;
                SelectedProducto = lstSelectedProducto.ItemsSource as List<Producto>;

                listaProducto.Add(lstSelectedProducto.SelectedItem as Producto);
                SelectedProducto.Remove(lstSelectedProducto.SelectedItem as Producto);
                
                
                listaProducto = new List<Producto>(listaProducto);
                SelectedProducto = new List<Producto>(SelectedProducto);
                lstProducto.ItemsSource = listaProducto;
                lstSelectedProducto.ItemsSource = SelectedProducto;

            }

        }

        private void AgregarProductos_Click(object sender, RoutedEventArgs e)
        {
            this.lstSelectedProducto.ItemsSource = pSQL.BuscarProducto() ;
            this.lstProducto.ItemsSource = new List<Producto>();
            

 
        }

        private void QuitarProductos_Click(object sender, RoutedEventArgs e)
        {
            this.lstSelectedProducto.ItemsSource =  new  List<Producto>() ;
            this.lstProducto.ItemsSource = pSQL.BuscarProducto();
            


        }

       
    }


    public class SelectableObject<T>
    {
        public bool IsSelected { get; set; }
        public T ObjectData { get; set; }

        public SelectableObject(T objectData)
        {
            ObjectData = objectData;
        }

        public SelectableObject(T objectData, bool isSelected)
        {
            IsSelected = isSelected;
            ObjectData = objectData;
        }
    }
}
