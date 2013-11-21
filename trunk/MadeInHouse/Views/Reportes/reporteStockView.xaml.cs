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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Caliburn.Micro;
using System.Diagnostics;
using MadeInHouse.Models;
using System.Collections.ObjectModel;
using MadeInHouse.Models.Almacen;
using MadeInHouse.DataObjects.Reportes;

namespace MadeInHouse.Views.Reportes
{

    public partial class reporteStockView : UserControl
    {
        public reporteStockView()
        {
            InitializeComponent();
            listBoxAlmacen1.Items.Add("ALMACEN CENTRAL");
            List<Tienda> lstTienda = new List<Tienda>();
            lstTienda = DataObjects.Reportes.ReporteVentasSQL.BuscarTienda();
            for (int i = 0; i < lstTienda.Count; i++) listBoxSede1.Items.Add(lstTienda[i].Nombre);
        }

        private void pasarListBox(ListBox origen, ListBox destino)
        {
            String text = origen.SelectedItem.ToString();
            text = text.Replace("System.Windows.Controls.ListBoxItem: ", "");
            destino.Items.Add(text);
            origen.Items.Remove(origen.SelectedItem);
        }
       
        private void todo_lista_a_lista(ListBox origen, ListBox destino)
        {
            while (origen.Items.Count != 0)
            {
                String text = origen.Items[0].ToString();
                text = text.Replace("System.Windows.Controls.ListBoxItem: ", "");
                destino.Items.Add(text);
                origen.Items.RemoveAt(0);
            }
        }
       
        private void ListBoxItem_DoubleClicked(object sender, RoutedEventArgs e)
        {
            List<Almacenes> lstAlmacenes;

            if (listBoxSede1.SelectedItem != null)
            {
                lstAlmacenes = new List<Almacenes>();
                String text = listBoxSede1.SelectedItem.ToString();
                pasarListBox(listBoxSede1, ListBoxSede2);
                text = text.Replace("System.Windows.Controls.ListBoxItem: ", "");
                lstAlmacenes = DataObjects.Reportes.reporteStock.BuscarAlmacenesxTienda(text);

                for (int i = 0; i < lstAlmacenes.Count;i++ )
                {
                    listBoxAlmacen1.Items.Add(lstAlmacenes[i].Nombre);
                }
                
            }
            if (ListBoxSede2.SelectedItem != null)
            {
                lstAlmacenes = new List<Almacenes>();
                String text = ListBoxSede2.SelectedItem.ToString();
                pasarListBox(ListBoxSede2, listBoxSede1);
                text = text.Replace("System.Windows.Controls.ListBoxItem: ", "");
                lstAlmacenes = DataObjects.Reportes.reporteStock.BuscarAlmacenesxTienda(text);

                for (int i = 0; i < lstAlmacenes.Count; i++)
                {
                    listBoxAlmacen1.Items.Remove(lstAlmacenes[i].Nombre);
                    listBoxAlmacen2.Items.Remove(lstAlmacenes[i].Nombre);
                }

            }
            if (listBoxAlmacen1.SelectedItem != null)
            {                
                pasarListBox(listBoxAlmacen1, listBoxAlmacen2);
            }
            if (listBoxAlmacen2.SelectedItem != null)
            {
                pasarListBox(listBoxAlmacen2, listBoxAlmacen1);
            }

        }
       
        private void Unselect(object sender, RoutedEventArgs e)
        {
            listBoxSede1.UnselectAll();
            ListBoxSede2.UnselectAll();

        }
        
        private void Pasar_Todo(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(derecha3))
            {
                List<Almacenes> lstAlmacenes = new List<Almacenes>();
                lstAlmacenes = DataObjects.Reportes.reporteStock.BuscarAlmacen();
                todo_lista_a_lista(listBoxSede1, ListBoxSede2);
                listBoxAlmacen2.Items.Clear();
                listBoxAlmacen1.Items.Clear();
                for (int i = 0; i < lstAlmacenes.Count;i++)
                {
                    listBoxAlmacen1.Items.Add(lstAlmacenes[i].Nombre);
                }

            }
            if (sender.Equals(izquierda3))
            {
                todo_lista_a_lista(ListBoxSede2, listBoxSede1);
                listBoxAlmacen2.Items.Clear();
                listBoxAlmacen1.Items.Clear();
                listBoxAlmacen1.Items.Add("ALMACEN CENTRAL");
            }

            if (sender.Equals(izquierda1))
            {
                todo_lista_a_lista(listBoxAlmacen2, listBoxAlmacen1);

            }
            if (sender.Equals(derecha1))
            {
                todo_lista_a_lista(listBoxAlmacen1, listBoxAlmacen2);
            }
            
        }

        private void generar(object sender, RoutedEventArgs e)
        {
            List<stock> lista = new List<stock>();
            List<stock> stocks = new List<stock>();

            for (int i = 0; i < ListBoxSede2.Items.Count; i++)
            {
                if (listBoxAlmacen2.Items[i].ToString() == "ALMACEN CENTRAL") lista = DataObjects.Reportes.reporteStock.BuscarStockCentral();
                else if (listBoxAlmacen2.Items[i].ToString().Contains("anaq")) lista = DataObjects.Reportes.reporteStock.BuscarStock(ListBoxSede2.Items[i].ToString());
                
                for (int j = 0; j < lista.Count; j++)
                {
                    stocks.Add(lista[j]);
                }
            }
            
            Reportes.reportViewerStock win = new reportViewerStock(stocks);
            win.Show();
        }

    }

}
