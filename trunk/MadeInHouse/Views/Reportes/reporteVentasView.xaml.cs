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

        private void ListBoxItem_DoubleClicked(object sender, RoutedEventArgs e)
        {
            if(listBoxProveedores1.SelectedItem !=null)
            pasarListBox(listBoxProveedores1,listBoxProveedores2);
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
    }
}