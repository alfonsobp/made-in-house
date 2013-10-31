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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MadeInHouse.Views.Reportes
{
    /// <summary>
    /// Lógica de interacción para reporteServiciosView.xaml
    /// </summary>
    public partial class reporteServiciosView : UserControl
    {
        public reporteServiciosView()
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
                pasarListBox(listBoxProveedores1, LstProveedores);
            if (LstProveedores.SelectedItem != null)
                pasarListBox(LstProveedores, listBoxProveedores1);

            if (listBoxCategorias1.SelectedItem != null)
                pasarListBox(listBoxCategorias1, LstCategoria);
            if (LstCategoria.SelectedItem != null)
                pasarListBox(LstCategoria, listBoxCategorias1);

            if (listBoxSede1.SelectedItem != null)
                pasarListBox(listBoxSede1, LstSede);
            if (LstSede.SelectedItem != null)
                pasarListBox(LstSede, listBoxSede1);


        }
        private void Unselect(object sender, RoutedEventArgs e)
        {
            listBoxCategorias1.UnselectAll();
            LstCategoria.UnselectAll();
            listBoxSede1.UnselectAll();
            LstSede.UnselectAll();
            listBoxProveedores1.UnselectAll();
            LstProveedores.UnselectAll();
        }

        private void Pasar_Todo(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(derecha1)) todo_lista_a_lista(listBoxProveedores1, LstProveedores);
            if (sender.Equals(izquierda1)) todo_lista_a_lista(LstProveedores, listBoxProveedores1);

            if (sender.Equals(derecha2)) todo_lista_a_lista(listBoxCategorias1, LstCategoria);
            if (sender.Equals(izquierda2)) todo_lista_a_lista(LstCategoria, listBoxCategorias1);

            if (sender.Equals(derecha3)) todo_lista_a_lista(listBoxSede1, LstSede);
            if (sender.Equals(izquierda3)) todo_lista_a_lista(LstSede, listBoxSede1);
        }
    }
}
