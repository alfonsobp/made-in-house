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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Cells[1, 1] = "2";
            xlWorkSheet.Cells[2, 1] = "7";
            xlWorkSheet.Cells[3, 1] = "15";
            xlWorkSheet.Cells[4, 1].Formula = "=SUM(A1:A3)";
          
            xlWorkBook.SaveAs("C:\\LALALA.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            MessageBox.Show("archivo excel creado , esta en c:\\LALALA.xls");
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


    }
}
