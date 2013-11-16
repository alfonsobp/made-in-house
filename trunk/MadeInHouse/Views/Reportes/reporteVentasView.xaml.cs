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

        public reporteVentasView()
        {

            InitializeComponent();
            List<Tienda> lstTienda = new List<Tienda>();
            List<Cliente> lstCliente = new List<Cliente>();
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


        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void Generar(object sender, RoutedEventArgs e)
        {
            ventas = new List<Venta>();
            DateTime inicio = Convert.ToDateTime(FechaIni.Text);
            DateTime fin =Convert.ToDateTime(FechaFin.Text);

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

            double suma = 0;
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
                suma += ventaAux4[i].Monto;
            }

            
            Lista.ItemsSource = ventaAux4;
            montoTotal.Text = suma + "";


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;
            Excel.Workbook newWorkbook = excelApp.Workbooks.Add();
            string workbookPath = "c:/SomeWorkBook.xls";  // RUTA DEL EXCEL A IMPORTAR
            Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                    0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                    true, false, 0, true, false, false);
            Excel.Sheets excelSheets = excelWorkbook.Worksheets;
            Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelSheets.get_Item(1);

            Excel.Range excelCell = (Excel.Range)excelWorksheet.get_Range("A1", "A1"); // LA CELDA QUE QUEIRES OBTENER,
            // SE PUEDE TRABAJAR CON RANGOS COMPLETOS PERO AUN NO SE COMO
            MessageBox.Show(excelCell.Value);//MENSAJE CON EL VALOR DE LA CELDA A1,A1

            excelApp.Workbooks.Close();
            excelApp.Quit();

            releaseObject(excelWorksheet);
            releaseObject(newWorkbook);
            releaseObject(excelApp);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            

            xlApp = new Excel.Application();

            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Cells[1, 1] = "idVenta";
            xlWorkSheet.Cells[1, 2] = "Tipo Venta";
            xlWorkSheet.Cells[1, 3] = "tipo Doc Pago";
            xlWorkSheet.Cells[1, 4] = "Id Usuario";
            xlWorkSheet.Cells[1, 5] = "Numero Documento";
            xlWorkSheet.Cells[1, 6] = "   monto";
            xlWorkSheet.Cells[1, 6] = "   igv";

            for (int i = 2; i < ventas.Count;i++ )
            {
                xlWorkSheet.Cells[i, 1] = ventas[i].IdVenta;
                xlWorkSheet.Cells[i, 2] = ventas[i].TipoVenta;
                xlWorkSheet.Cells[i, 3] = ventas[i].TipoDocPago;
                xlWorkSheet.Cells[i, 4] = ventas[i].IdUsuario;
                xlWorkSheet.Cells[i, 5] = ventas[i].NumDocPago;
                xlWorkSheet.Cells[i, 6] = ventas[i].Monto;
                xlWorkSheet.Cells[i, 6] = ventas[i].Igv;
            }

            xlWorkBook.SaveAs("C:\\Reporte.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            MessageBox.Show("archivo excel creado , esta en c:\\Reporte.xls");
        }
    }
}
