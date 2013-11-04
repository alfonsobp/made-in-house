using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.Dictionary
{
    class ExcelUtiles
    {

        private static string Path;
        private static string constr;

        public static DataTableReader Importar(string name)
        {

            BuscarPath();
            MessageBoxResult r = MessageBox.Show("Desea Importar el Archivo ? \n" + Path, "Importar", MessageBoxButton.YesNo);
            if (r == MessageBoxResult.Yes)
            {
                if (Path != "")
                {
                    constr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path + ";Extended Properties=Excel 12.0;Persist Security Info=False";
                    OleDbConnection con = new OleDbConnection(constr);
                    OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
                    con.Open();

                    OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                    DataTable data = new DataTable();
                    sda.Fill(data);
                    DataTableReader ds = data.CreateDataReader();
                    return ds;
                    
                } 
            }
            return null;
        }


        private static void BuscarPath()
        {

            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.xlsx)|*.xlsx";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                Path = filename;
            }
        }

        

    }
}
