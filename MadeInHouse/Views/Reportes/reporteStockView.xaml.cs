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

namespace MadeInHouse.Views.Reportes
{
    /// <summary>
    /// Lógica de interacción para reporteStockView.xaml
    /// </summary>
    public partial class reporteStockView : UserControl
    {
        public reporteStockView()
        {
            InitializeComponent();
        }
        private void pasarListBox(ListBox origen, ListBox destino)
        {
            String text = origen.SelectedItem.ToString();
            text = text.Replace("System.Windows.Controls.ListBoxItem: ", "");
            destino.Items.Add(text);
            origen.Items.Remove(origen.SelectedItem);
        }

        private void ListBoxItem_DoubleClicked(object sender, RoutedEventArgs e)
        {
            if (listBoxCategorias1.SelectedItem != null)
                pasarListBox(listBoxCategorias1, listBoxCategorias2);
            if (listBoxCategorias2.SelectedItem != null)
                pasarListBox(listBoxCategorias2, listBoxCategorias1);
                        
            if (listBoxProductos1.SelectedItem != null)
                pasarListBox(listBoxProductos1, listBoxProductos2);
            if (listBoxProductos2.SelectedItem != null)
                pasarListBox(listBoxProductos2, listBoxProductos1);

            if (listBoxAlmacen1.SelectedItem != null)
                pasarListBox(listBoxAlmacen1, listBoxAlmacen2);
            if (listBoxAlmacen2.SelectedItem != null)
                pasarListBox(listBoxAlmacen2, listBoxAlmacen1);}

        private void Unselect(object sender, RoutedEventArgs e)
        {
            listBoxCategorias1.UnselectAll();
            listBoxCategorias2.UnselectAll();
            listBoxProductos1.UnselectAll();
            listBoxProductos2.UnselectAll();
            listBoxAlmacen1.UnselectAll();
            listBoxAlmacen2.UnselectAll();
        }
    }
    

}
