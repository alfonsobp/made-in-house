using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Data.OleDb;
using System.Data;
using System.Windows;
using MadeInHouse.Models.Compras;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models;
using MadeInHouse.DataObjects.Compras;



namespace MadeInHouse.ViewModels.Compras
{
    class CatalogoProductoProveedorViewModel : PropertyChangedBase
    {
        List<ProveedorxProducto> lstProducto;

        public List<ProveedorxProducto> LstProducto
        {
            get { return lstProducto; }
            set { lstProducto = value; NotifyOfPropertyChange(() => LstProducto); }
        }

        private ProveedorxProducto seleccionado;

        public ProveedorxProducto Seleccionado
        {
            get { return seleccionado; }
            set { seleccionado = value; NotifyOfPropertyChange(() => Seleccionado); }
        }



        string path;

        public string Path
        {
            get { return path; }
            set { path = value; NotifyOfPropertyChange(() => Path); }
        }

        Proveedor prov;

        public Proveedor Prov
        {
            get { return prov; }
            set { prov = value; NotifyOfPropertyChange(() => Prov); }
        }

        ProveedorxProductoSQL eM = new ProveedorxProductoSQL();

        public void Refrescar() {

            if (prov != null)
                LstProducto = eM.Buscar(Prov.IdProveedor) as List<ProveedorxProducto>;
        }

        public void Eliminar() {

            if (prov != null) {
                new ProveedorxProductoSQL().Eliminar(seleccionado);
                Refrescar();
            }
        
        }
        

        public void Cargar()
        {

            if (path != "")
            {

                List<ProveedorxProducto> lista = new List<ProveedorxProducto>();

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
                   ProveedorxProducto cp = new ProveedorxProducto();
                    cp.IdProveedor = Prov.IdProveedor;
                    cp.Producto = new Producto();
                    cp.Producto.CodigoProd= ds["Codigo"].ToString();
                    cp.CodComercial = ds["Codigo Comercial"].ToString();
                    cp.Precio = Convert.ToDouble(ds["Precio"].ToString());
                    cp.Descripcion = ds["Descripcion"].ToString();
                    cp.FechaAct = DateTime.Now;
                    cp.FechaReg = DateTime.Now;
                    ProveedorxProductoSQL pp = new ProveedorxProductoSQL();
                     int k = pp.Insertar(cp);

                    
                }

              

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

        public void Importar(){

            if (prov != null)
            {

                BuscarPath();

                MessageBoxResult r = MessageBox.Show("Desea Importar el Archivo ? ", "Importar", MessageBoxButton.YesNo);

                if (r == MessageBoxResult.Yes)
                {

                    Cargar();
                    Refrescar();
                }
          
            }
        }

        public void BuscarProveedor() { 
        MyWindowManager w = new MyWindowManager();
        w.ShowWindow(new BuscadorProveedorViewModel(this)); 
        
        }

       

        public void EditarProducto() {
            if (prov != null  && seleccionado != null)
            {
                MyWindowManager win = new MyWindowManager();
                ProveedorxProducto pp;
                pp = seleccionado;
                win.ShowWindow(new Compras.ProductoViewModel(pp, this));
            }
        }

        public void NuevoProducto() {
            if (prov != null)
            {
                MyWindowManager win = new MyWindowManager();
                ProveedorxProducto pp = new ProveedorxProducto();
                pp.Producto = new Producto();
                pp.IdProveedor = Prov.IdProveedor;
                win.ShowWindow(new Compras.ProductoViewModel(pp, this));
            }
        }
     
    }
}
