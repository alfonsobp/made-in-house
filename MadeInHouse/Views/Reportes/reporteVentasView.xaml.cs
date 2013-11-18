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
using Excel = Microsoft.Office.Interop.Excel;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Ventas;

namespace MadeInHouse.Views.Reportes
{
    /// <summary>
    /// Lógica de interacción para reporteVentasView.xaml
    /// </summary>
    public partial class reporteVentasView : UserControl
    {
        List<Venta> ventas;
        List<Cliente> lstCliente;
        public reporteVentasView()
        {

            InitializeComponent();
            FechaDesde.Text = "01/01/"+DateTime.Today.Year.ToString();
            FechaHasta.Text = "31/12/" + DateTime.Today.Year.ToString();
            List<Tienda> lstTienda = new List<Tienda>();
            lstCliente = new List<Cliente>();
            List<Producto> lstProducto = new List<Producto>();
            lstTienda = DataObjects.Reportes.ReporteVentasSQL.BuscarTienda();
            lstCliente = DataObjects.Reportes.ReporteVentasSQL.BuscarCliente();
            lstProducto = DataObjects.Reportes.ReporteVentasSQL.BuscarProducto();

            for (int i = 0; i < lstTienda.Count; i++) listBoxSede1.Items.Add(lstTienda[i].Nombre);
            for (int i = 0; i < lstCliente.Count; i++) listBoxCliente1.Items.Add(lstCliente[i].Nombre);
            for (int i = 0; i < lstProducto.Count; i++) listBoxProducto1.Items.Add(lstProducto[i].Nombre);

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
            DateTime fin =Convert.ToDateTime(FechaHasta.Text);

            List<Venta> ventaAux1 = new List<Venta>();
            List<Venta> ventaAux2 = new List<Venta>();
            List<Venta> ventaAux3 = new List<Venta>();
            List<Venta> ventaAux4 = new List<Venta>();
            List<string> tiendas = new List<string>();
            List<string> clientes = new List<string>();
            List<string> productos = new List<string>();
            List<int> lista = new List<int>();

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
                ventaAux3 = DataObjects.Reportes.ReporteVentasSQL.BuscarVentaxProducto(productos[i]);
                for (int j = 0; j < ventaAux3.Count; j++)
                {
                    ventas.Add(ventaAux3[j]);
                }
            }

            Venta vAux;
            Venta vAux2;
            for (int i = 0; i < ventas.Count; i++)
            {
                vAux = ventas[i];
                if (i != ventas.Count) ventas.RemoveAt(i);
                if ((vAux = ventas.Find(x => x.IdVenta == vAux.IdVenta)) != null)
                {
                    vAux2 = vAux;
                    if ((vAux2 = ventaAux4.Find(x => x.IdVenta == vAux2.IdVenta)) == null)
                        ventaAux4.Add(vAux);
                }
            }

           
            for (int i = 0; i < ventaAux4.Count; i++)
            {
                if (ventaAux4[i].FechaReg < inicio || ventaAux4[i].FechaReg > fin)
                {
                    ventaAux4.RemoveAt(i);
                    i = -1;
                }
            }
            for (int i = 0; i < ventaAux4.Count; i++)
            {
                
                for (int j = 0; j < lstCliente.Count; j++)
                {
                    if (lstCliente[j].Id == ventaAux4[i].IdCliente) ventaAux4[i].NombreCliente = lstCliente[j].Nombre;
                }
            }

            List<VentaAux> final = new List<VentaAux>();

            for (int i = 0; i < ventaAux4.Count; i++)
            {
                final.Add(new VentaAux());
                final[i].CodTarjeta = ventaAux4[i].CodTarjeta;
                final[i].Descuento = ventaAux4[i].Descuento;
                final[i].Estado  = ventaAux4[i].Estado;
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

    public class VentaAux
    {
        int idVenta;
        int codTarjeta;
        double descuento;
        int estado;
        DateTime fechaMod;

        string estadoS = "REALIZADA";

        public string EstadoS
        {
            get { return estadoS; }
            set { estadoS = value; }
        }

        string fechaRegS;

        public string FechaRegS
        {
            get { return fechaRegS; }
            set { fechaRegS = value; }
        }

        DateTime fechaReg;

        public DateTime FechaReg
        {
            get { return fechaReg; }
            set { fechaReg = value; }
        }

        DateTime fechaDespacho;



        string nombreCliente;

        public string NombreCliente
        {
            get { return nombreCliente; }
            set { nombreCliente = value; }
        }

        public int IdVenta
        {
            get { return idVenta; }
            set { idVenta = value; }
        }

        string tipoVenta;

        public string TipoVenta
        {
            get { return tipoVenta; }
            set { tipoVenta = value; }
        }

        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }


        public string TipoDocPago
        {
            get { return tipoDocPago; }
            set { tipoDocPago = value; }
        }

        public string NumDocPago
        {
            get { return numDocPago; }
            set { numDocPago = value; }
        }



        public double Monto
        {
            get { return monto; }
            set { monto = value; }
        }


        public DateTime FechaDespacho
        {
            get { return fechaDespacho; }
            set { fechaDespacho = value; }
        }

        int idUsuario;
        double igv;
        double monto;
        string numDocPago;
        string numDocPagoServicio;

        public string NumDocPagoServicio
        {
            get { return numDocPagoServicio; }
            set { numDocPagoServicio = value; }
        }

        private int idCliente;

        public int IdCliente
        {
            get { return idCliente; }
            set { idCliente = value; }
        }

        int ptosGanados;
        string tipoDocPago;

        public int CodTarjeta
        {
            get { return codTarjeta; }
            set { codTarjeta = value; }
        }


        public double Descuento
        {
            get { return descuento; }
            set { descuento = value; }
        }


        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }


        public DateTime FechaMod
        {
            get { return fechaMod; }
            set { fechaMod = value; }
        }


        public double Igv
        {
            get { return igv; }
            set { igv = value; }
        }
    
        public int PtosGanados
        {
            get { return ptosGanados; }
            set { ptosGanados = value; }
        }
        
    }
}
