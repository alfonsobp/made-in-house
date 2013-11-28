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
using Microsoft.ReportingServices;
using Microsoft.Reporting;
using Microsoft.Reporting.WinForms;
using MadeInHouse.DataObjects.Reportes;
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
        List<notas> lista;
        private System.Windows.Forms.BindingSource ProductBindingSource;
        private System.ComponentModel.IContainer mform_components = null;

        public ReporteMov()
        {
            InitializeComponent();
            lista = new List<notas>();

            List<Almacenes> lstAlmacenes = new AlmacenSQL().BuscarAlmacen();
            cmbAlmacen.ItemsSource = lstAlmacenes;
            cmbAlmacen.SelectedItem = (lstAlmacenes == null) ? null : lstAlmacenes[0];
            listaProducto = pSQL.BuscarProducto();
            this.lstProducto.ItemsSource = listaProducto;
            this.lstSelectedProducto.ItemsSource = SelectedProducto;
            this.fechaFin.SelectedDate = new DateTime(DateTime.Now.Year, 12, 31);
            this.fechaIni.SelectedDate = new DateTime(DateTime.Now.Year, 1, 1);
            PrepareReport();
        }

        private void PrepareReport()
        {
            this.mform_components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();

            this.ProductBindingSource = new System.Windows.Forms.BindingSource(this.mform_components);
            ((System.ComponentModel.ISupportInitialize)(this.ProductBindingSource)).BeginInit();



            reportDataSource1.Name = "DataSet10";
            reportDataSource1.Value = this.ProductBindingSource;

            this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this._reportViewer.LocalReport.ReportEmbeddedResource = "MadeInHouse.ReportViewer.Report7.rdlc";
            this._reportViewer.Location = new System.Drawing.Point(0, 0);

            ((System.ComponentModel.ISupportInitialize)(this.ProductBindingSource)).EndInit();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ProductBindingSource.DataSource = lista;
            this._reportViewer.RefreshReport();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (Validar())
            {
                DateTime desde = Convert.ToDateTime(fechaIni.Text);
                DateTime hasta = Convert.ToDateTime(fechaFin.Text);

                List<Producto> lst = lstSelectedProducto.ItemsSource as List<Producto>;
                Almacenes alm = cmbAlmacen.SelectedItem as Almacenes;
                List<notas> aux = new List<notas>();
                lista = new List<notas>();

                for (int i = 0; i < lst.Count; i++)
                {
                    aux = DataObjects.Reportes.reporteKardexSQL.BuscarEntradaSalida(alm.IdAlmacen, lst[i].IdProducto);
                    for (int j = 0; j < aux.Count; j++)
                    {
                        DateTime fecha = Convert.ToDateTime(aux[j].FechaReg);
                        
                        if (fecha > desde && fecha < hasta)
                        lista.Add(aux[j]);
                    }
                }
                             

                Window_Loaded(sender, e);
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
