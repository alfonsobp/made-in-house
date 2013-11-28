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
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Compras;
using MadeInHouse.Models.Ventas;

namespace MadeInHouse.Views.Reportes
{
    /// <summary>
    /// Lógica de interacción para reporteServiciosView.xaml
    /// </summary>
    public partial class reporteServiciosView : UserControl
    {
        List<Venta> ventas;
        List<Cliente> lstCliente;

        public reporteServiciosView()
        {
            InitializeComponent();
            FechaDesde.Text = "01/01/" + DateTime.Today.Year.ToString();
            FechaHasta.Text = "31/12/" + DateTime.Today.Year.ToString();
            List<Tienda> lstTienda = new List<Tienda>();
            lstCliente = new List<Cliente>();
            List<Servicio> lstServicio = new List<Servicio>();
            lstTienda = DataObjects.Reportes.ReporteVentasSQL.BuscarTienda();
            lstCliente = DataObjects.Reportes.ReporteVentasSQL.BuscarCliente();
            lstServicio = DataObjects.Reportes.ReporteVentasSQL.BuscarServicio();

            for (int i = 0; i < lstTienda.Count; i++) listBoxSede1.Items.Add(lstTienda[i].Nombre);
            for (int i = 0; i < lstCliente.Count; i++) listBoxCliente1.Items.Add(lstCliente[i].Nombre);
            for (int i = 0; i < lstServicio.Count; i++) listBoxProducto1.Items.Add(lstServicio[i].Nombre);

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
            if (listBoxCliente1.SelectedItem != null)
                pasarListBox(listBoxCliente1, ListBoxCliente2);
            if (ListBoxCliente2.SelectedItem != null)
                pasarListBox(ListBoxCliente2, listBoxCliente1);

            if (listBoxProducto1.SelectedItem != null)
                pasarListBox(listBoxProducto1, ListBoxProducto2);
            if (ListBoxProducto2.SelectedItem != null)
                pasarListBox(ListBoxProducto2, listBoxProducto1);

            if (listBoxSede1.SelectedItem != null)
                pasarListBox(listBoxSede1, ListBoxSede2);
            if (ListBoxSede2.SelectedItem != null)
                pasarListBox(ListBoxSede2, listBoxSede1);
        }
        private void Unselect(object sender, RoutedEventArgs e)
        {
            listBoxProducto1.UnselectAll();
            ListBoxProducto2.UnselectAll();
            listBoxSede1.UnselectAll();
            ListBoxSede2.UnselectAll();
            listBoxCliente1.UnselectAll();
            ListBoxCliente2.UnselectAll();
        }
        private void Pasar_Todo(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(derecha1)) todo_lista_a_lista(listBoxCliente1, ListBoxCliente2);
            if (sender.Equals(izquierda1)) todo_lista_a_lista(ListBoxCliente2, listBoxCliente1);

            if (sender.Equals(derecha2)) todo_lista_a_lista(listBoxProducto1, ListBoxProducto2);
            if (sender.Equals(izquierda2)) todo_lista_a_lista(ListBoxProducto2, listBoxProducto1);

            if (sender.Equals(derecha3)) todo_lista_a_lista(listBoxSede1, ListBoxSede2);
            if (sender.Equals(izquierda3)) todo_lista_a_lista(ListBoxSede2, listBoxSede1);
        }
        private void Generar(object sender, RoutedEventArgs e)
        {
            ventas = new List<Venta>();
            DateTime inicio = Convert.ToDateTime(FechaDesde.Text);
            DateTime fin = Convert.ToDateTime(FechaHasta.Text);

            List<Venta> ventaAux1 = new List<Venta>();
            List<Venta> ventaAux2 = new List<Venta>();
            List<Venta> ventaAux3 = new List<Venta>();
            List<Venta> ventaAux4 = new List<Venta>();
            List<string> tiendas = new List<string>();
            List<string> clientes = new List<string>();
            List<string> productos = new List<string>();
            List<int> lista = new List<int>();

            ventaAux4 = DataObjects.Reportes.ReporteVentasSQL.BuscarVenta();

            for (int i = 0; i < ListBoxSede2.Items.Count; i++)
            {
                tiendas.Add(ListBoxSede2.Items[i].ToString());
                ventaAux1 = DataObjects.Reportes.ReporteVentasSQL.BuscarVentaxTienda(tiendas[i]);
                for (int j = 0; j < ventaAux1.Count; j++)
                {
                    ventas.Add(ventaAux1[j]);
                }
            }

            for (int i = 0; i < ListBoxCliente2.Items.Count; i++)
            {
                clientes.Add(ListBoxCliente2.Items[i].ToString());
                ventaAux2 = DataObjects.Reportes.ReporteVentasSQL.BuscarVentaxCliente(clientes[i]);
                for (int j = 0; j < ventaAux2.Count; j++)
                {
                    ventas.Add(ventaAux2[j]);
                }
            }

            for (int i = 0; i < ListBoxProducto2.Items.Count; i++)
            {
                productos.Add(ListBoxProducto2.Items[i].ToString());
                ventaAux3 = DataObjects.Reportes.ReporteVentasSQL.BuscarVentaxServicio(productos[i]);
                for (int j = 0; j < ventaAux3.Count; j++)
                {
                    ventas.Add(ventaAux3[j]);
                }
            }

           

            List<VentaAux> final = new List<VentaAux>();

            for (int i = 0; i < ventaAux4.Count; i++)
            {
                final.Add(new VentaAux());
                final[i].CodTarjeta = ventaAux4[i].CodTarjeta;
                final[i].Descuento = ventaAux4[i].Descuento;
                final[i].Estado = ventaAux4[i].Estado;
                final[i].EstadoS = ventaAux4[i].EstadoS;
                final[i].FechaDespacho = ventaAux4[i].FechaDespacho;
                final[i].FechaMod = ventaAux4[i].FechaMod;
                final[i].FechaReg = ventaAux4[i].FechaReg;
                final[i].FechaRegS = ventaAux4[i].FechaRegS;
                final[i].IdCliente = ventaAux4[i].IdCliente;
                final[i].IdUsuario = ventaAux4[i].IdUsuario;
                final[i].IdVenta = ventaAux4[i].IdVenta;
                final[i].Igv = ventaAux4[i].Igv;
                final[i].Monto = ventaAux4[i].Monto;
                final[i].NombreCliente = ventaAux4[i].NombreCliente;
                final[i].NumDocPago = ventaAux4[i].NumDocPago;
                final[i].NumDocPagoServicio = ventaAux4[i].NumDocPagoServicio;
                final[i].PtosGanados = ventaAux4[i].PtosGanados;
                final[i].TipoDocPago = ventaAux4[i].TipoDocPago;
                final[i].TipoVenta = ventaAux4[i].TipoVenta;

            }
            Reportes.reportViewerVentas neva = new reportViewerVentas(final);
            neva.Show();
        }

    }
}
