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
    /// Lógica de interacción para reporteVentasView.xaml
    /// </summary>
    public partial class reporteVentasView : UserControl
    {
        public reporteVentasView()
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
            if (listBoxProveedores1.SelectedItem != null)
                pasarListBox(listBoxProveedores1, listBoxProveedores2);
            if (listBoxProveedores2.SelectedItem != null)
                pasarListBox(listBoxProveedores2, listBoxProveedores1);

            if (listBoxCategorias1.SelectedItem != null)
                pasarListBox(listBoxCategorias1, listBoxCategorias2);
            if (listBoxCategorias2.SelectedItem != null)
                pasarListBox(listBoxCategorias2, listBoxCategorias1);

            if (listBoxSede1.SelectedItem != null)
                pasarListBox(listBoxSede1, listBoxSede2);
            if (listBoxSede2.SelectedItem != null)
                pasarListBox(listBoxSede2, listBoxSede1);


        }
        private void Unselect(object sender, RoutedEventArgs e)
        {
            listBoxCategorias1.UnselectAll();
            listBoxCategorias2.UnselectAll();
            listBoxSede1.UnselectAll();
            listBoxSede2.UnselectAll();
            listBoxProveedores1.UnselectAll();
            listBoxProveedores2.UnselectAll();
        }

        private void Pasar_Todo(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(derecha1)) todo_lista_a_lista(listBoxProveedores1, listBoxProveedores2);
            if (sender.Equals(izquierda1)) todo_lista_a_lista(listBoxProveedores2, listBoxProveedores1);

            if (sender.Equals(derecha2)) todo_lista_a_lista(listBoxCategorias1, listBoxCategorias2);
            if (sender.Equals(izquierda2)) todo_lista_a_lista(listBoxCategorias2, listBoxCategorias1);

            if (sender.Equals(derecha3)) todo_lista_a_lista(listBoxSede1, listBoxSede2);
            if (sender.Equals(izquierda3)) todo_lista_a_lista(listBoxSede2, listBoxSede1);
        }


    }
}
