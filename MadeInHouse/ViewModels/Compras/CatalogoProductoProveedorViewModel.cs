using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Data.OleDb;
using System.Data;
using MadeInHouse.Models.Compras;
using System.Windows;
using MadeInHouse.Models;


namespace MadeInHouse.ViewModels.Compras
{
    class CatalogoProductoProveedorViewModel : PropertyChangedBase
    {
        CatalogoProveedor seleccionado;

        public  CatalogoProveedor Seleccionado
        {
            get { return seleccionado; }
            set { seleccionado = value; NotifyOfPropertyChange(() => Seleccionado); }
        }


        List<CatalogoProveedor> tblCatalogo;

        public List<CatalogoProveedor> TblCatalogo
        {
            get { return tblCatalogo; }
            set { tblCatalogo = value; NotifyOfPropertyChange(() => TblCatalogo); }
        }



        string path;

        public string Path
        {
            get { return path; }
            set { path = value; NotifyOfPropertyChange(() => Path); }
        }


        public void Cargar()
        {

            if (path != "")
            {

                List<CatalogoProveedor> lista = new List<CatalogoProveedor>();

                String name = "Catalogo";
                String constr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;Persist Security Info=False";
                OleDbConnection con = new OleDbConnection(constr);
                OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
                con.Open();

                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                DataTable data = new DataTable();
                sda.Fill(data);
                DataTableReader ds = data.CreateDataReader();

                while (ds.Read())
                {
                    CatalogoProveedor cp = new CatalogoProveedor();


                    cp.CodigoProducto = ds["Codigo"].ToString();
                    cp.CodigoComercial = ds["Codigo Comercial"].ToString();
                    cp.Precio = Convert.ToDouble(ds["Precio"].ToString());
                    cp.Descripcion = ds["Descripcion"].ToString();
                    lista.Add(cp);

                }

                TblCatalogo = lista;


            }


        }

        public void BuscarPath()
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

        public  void BuscarCatalogo(){

            MessageBox.Show(Seleccionado.CodigoComercial);
        }

        public void EditarProducto() {
            MyWindowManager win = new MyWindowManager();
            seleccionado.Proveedor = new Proveedor();
            seleccionado.Proveedor.RazonSocial="CLIENTE ABC";
            win.ShowWindow(new Compras.ProductoViewModel(seleccionado) );
        }

        public void NuevoProducto() {

            MyWindowManager win = new MyWindowManager();
            Seleccionado = new CatalogoProveedor();
            seleccionado.Proveedor = new Proveedor();
            seleccionado.Proveedor.RazonSocial="CLIENTE ABC";
            win.ShowWindow(new Compras.ProductoViewModel(seleccionado));
        
        }
    }
}
