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
using MadeInHouse.DataObjects.Reportes;
using MadeInHouse.Models.Compras;

namespace MadeInHouse.Views.Reportes
{
    /// <summary>
    /// Lógica de interacción para reporteComprasView.xaml
    /// </summary>
    public partial class reporteComprasView : UserControl
    {
        List<Proveedor> lstProveedor;

        public reporteComprasView()
        {
            InitializeComponent();
            FechaDesde.Text = "01/01/2013";
            FechaHasta.Text = "31/12/2013";
            lstProveedor = new List<Proveedor>();
            lstProveedor = DataObjects.Reportes.ReporteOrdenCompraSQL.BuscarProveedor();
            for (int i = 0; i < lstProveedor.Count; i++) ListBoxProveedor1.Items.Add(lstProveedor[i].RazonSocial);
            ListBoxEstado1.Items.Add("CANCELADA");
            ListBoxEstado1.Items.Add("BORRADOR");
            ListBoxEstado1.Items.Add("EMITIDA");
            ListBoxEstado1.Items.Add("ATENDIDA");

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
            if (ListBoxProveedor1.SelectedItem != null)
                pasarListBox(ListBoxProveedor1, ListBoxProveedor2);
            if (ListBoxProveedor2.SelectedItem != null)
                pasarListBox(ListBoxProveedor2, ListBoxProveedor1);

            if (ListBoxEstado1.SelectedItem != null)
                pasarListBox(ListBoxEstado1, ListBoxEstado2);
            if (ListBoxEstado2.SelectedItem != null)
                pasarListBox(ListBoxEstado2, ListBoxEstado1);
        }
        private void Unselect(object sender, RoutedEventArgs e)
        {
            ListBoxProveedor1.UnselectAll();
            ListBoxProveedor2.UnselectAll();
            ListBoxEstado1.UnselectAll();
            ListBoxEstado2.UnselectAll();
        }
        private void Pasar_Todo(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(derecha1)) todo_lista_a_lista(ListBoxProveedor1, ListBoxProveedor2);
            if (sender.Equals(izquierda1)) todo_lista_a_lista(ListBoxProveedor2, ListBoxProveedor1);

            if (sender.Equals(derecha2)) todo_lista_a_lista(ListBoxEstado1, ListBoxEstado2);
            if (sender.Equals(izquierda2)) todo_lista_a_lista(ListBoxEstado2, ListBoxEstado1);
        }
        private void generar(object sender, RoutedEventArgs e)
        {
            List<OrdenDeCompra> lstOrdenCompra = new List<OrdenDeCompra>();
            lstOrdenCompra = DataObjects.Reportes.ReporteOrdenCompraSQL.BuscarOrdenesCompra();

            for (int i = 0; i < lstOrdenCompra.Count;i++ )
            {
                if (lstOrdenCompra[i].Estado == "0") lstOrdenCompra[i].Estado = "CANCELADA";
                if (lstOrdenCompra[i].Estado == "1") lstOrdenCompra[i].Estado = "BORRADOR";
                if (lstOrdenCompra[i].Estado == "2") lstOrdenCompra[i].Estado = "EMITIDA";
                if (lstOrdenCompra[i].Estado == "3") lstOrdenCompra[i].Estado = "ATENDIDA";
                for (int j = 0; j < lstProveedor.Count; j++)
                {
                    if (lstProveedor[j].IdProveedor.ToString() == lstOrdenCompra[i].Proveedor) 
                        lstOrdenCompra[i].Proveedor = lstProveedor[j].RazonSocial;
                }
            }

            for (int i = 0; i < lstOrdenCompra.Count; i++)
            {
                for (int j = 0; j < ListBoxEstado1.Items.Count; j++)
                {
                    if (lstOrdenCompra[i].Estado == ListBoxEstado1.Items[j].ToString()) { lstOrdenCompra.RemoveAt(i); i = -1; break; }
                        
                }
                
            }
            for (int i = 0; i < lstOrdenCompra.Count; i++)
            {
                for (int j = 0; j < ListBoxProveedor1.Items.Count; j++)
                {
                    if (lstOrdenCompra[i].Proveedor == ListBoxProveedor1.Items[j].ToString()) { lstOrdenCompra.RemoveAt(i); i = -1; break; }

                }
            }
            DateTime inicio = Convert.ToDateTime(FechaDesde.Text);
            DateTime fin = Convert.ToDateTime(FechaHasta.Text);
            for (int i = 0; i < lstOrdenCompra.Count; i++)
            {
                if (Convert.ToDateTime(lstOrdenCompra[i].FechaReg) < inicio || Convert.ToDateTime(lstOrdenCompra[i].FechaReg) > fin)
                {
                    lstOrdenCompra.RemoveAt(i);
                    i = -1;
                }
            }
            Lista.ItemsSource = lstOrdenCompra;
        }
    }
}
