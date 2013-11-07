using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Views.Compras;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.Models;
using System.Data.OleDb;
using System.Data;
using MadeInHouse.Models.Compras;


namespace MadeInHouse.ViewModels.Compras
{
    class BuscadorProveedorViewModel : Screen
    {

        public BuscadorProveedorViewModel(){

            ActualizarProveedor();
        }

        generarOrdenCompraViewModel g;
        public BuscadorProveedorViewModel(generarOrdenCompraViewModel g)
        {

            this.g = g;
            ActualizarProveedor();
        }

        
        CatalogoProductoProveedorViewModel p;
        public BuscadorProveedorViewModel(CatalogoProductoProveedorViewModel p) {

            this.p = p;
            ActualizarProveedor();
        }

        NuevaCotizacionViewModel cp;

        public BuscadorProveedorViewModel(NuevaCotizacionViewModel cp)
        {
            this.cp = cp;
            ActualizarProveedor();
        }

        agregarServicioViewModel s;
        public BuscadorProveedorViewModel(agregarServicioViewModel s)
        {
            this.s = s;
            ActualizarProveedor();
        }


        generarOrdenCompraViewModel go;
        BuscarCotizacionViewModel bc;      
        public BuscadorProveedorViewModel(BuscarCotizacionViewModel bc)
        {
            this.bc = bc;
            ActualizarProveedor();
        }

        BuscarDocumentoViewModel bd;
        public BuscadorProveedorViewModel(BuscarDocumentoViewModel bd)
        {
            this.bd = bd;
            ActualizarProveedor();
        }        private MyWindowManager win = new MyWindowManager();

        ProveedorSQL eM = new ProveedorSQL();

        private string txtRuc;


        public string TxtRuc
        {
           
            get { return txtRuc; }
            set { txtRuc = value; NotifyOfPropertyChange(() => TxtRuc); }
        }

        private string txtRazonSocial;

        public string TxtRazonSocial
        {
            get { return txtRazonSocial; }
            set { txtRazonSocial = value; NotifyOfPropertyChange(() => TxtRazonSocial); }
        }

        private string txtCodigo;

        public string TxtCodigo
        {
            get { return txtCodigo; }
            set { txtCodigo = value; NotifyOfPropertyChange(() => TxtCodigo); }
        }



        private DateTime fechaIni=new DateTime(DateTime.Now.Year, 1,1);

        public DateTime FechaIni
        {
            get { return fechaIni; }
            set { fechaIni = value; NotifyOfPropertyChange(() => FechaIni); }
        }


        private DateTime fechaFin=new DateTime(DateTime.Now.Year,12,31);

        public DateTime FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; NotifyOfPropertyChange(() => FechaFin); }
        }

        

        private List<Proveedor> lstProveedor;

        public  List<Proveedor> LstProveedor
        {
            get { return lstProveedor; }
            set { lstProveedor = value; NotifyOfPropertyChange(() => LstProveedor); }
        }

        private Proveedor proveedorSeleccionado;

        public Proveedor ProveedorSeleccionado
        {
            get { return proveedorSeleccionado; }
            set { proveedorSeleccionado = value; NotifyOfPropertyChange("ProveedorSeleccionado"); }
        }


            
        public void SelectedItemChanged()
        {
            if (ProveedorSeleccionado != null)
            {

                if (g != null) {

                    g.Prov = ProveedorSeleccionado;
                    TryClose();
                
                
                }

                if (p != null)
                {
                    p.Prov = ProveedorSeleccionado;
                    TryClose();
                }

                if (s != null)
                {
                    s.Prov = ProveedorSeleccionado;
                    TryClose();
                }

                if (cp != null)
                {
                    cp.Prov = ProveedorSeleccionado;
                    TryClose();
                }

                if (go != null) {

                    go.Prov = ProveedorSeleccionado;
                    go.BuscarDispo = false;
                    TryClose();

                    
                }

                if (p != null)
                {
                    p.Prov = ProveedorSeleccionado;
                    TryClose();
                }

            }

            if (bc != null)
            {
                bc.Prov = proveedorSeleccionado;
                TryClose();
            }

            if (bd != null)
            {
                bd.Prov = proveedorSeleccionado;
                TryClose();
            }
        }


        public void Limpiar() {
            TxtCodigo = null;
            TxtRuc = null;
            TxtRazonSocial = null;
            LstProveedor = null;
            FechaIni = new DateTime(DateTime.Now.Year, 1, 1);
            FechaFin = new DateTime(DateTime.Now.Year, 12, 31);
        }
     
        public void NuevoProveedor()
        {        
            Compras.MantenerProveedorViewModel obj = new Compras.MantenerProveedorViewModel(this);   
            win.ShowWindow(obj);  
        }

        public void EditarProveedor()
        {
            Compras.MantenerProveedorViewModel obj = new Compras.MantenerProveedorViewModel(ProveedorSeleccionado,this);
            win.ShowWindow(obj);
        }

        public void EliminarProveedor()
        {
            MessageBox.Show("Proveedor Eliminado \n\nCodigo = " + ProveedorSeleccionado.CodProveedor + "\nRazon social = " + ProveedorSeleccionado.RazonSocial + 
                            "\nRuc = " + ProveedorSeleccionado.Ruc + "\nTelefono = " + ProveedorSeleccionado.Telefono + "\nFax = " + ProveedorSeleccionado.Fax + 
                            "\nContacto = " + ProveedorSeleccionado.Contacto + "\nTelefono contacto = " + ProveedorSeleccionado.TelefonoContacto + 
                            "\nDireccion = " + ProveedorSeleccionado.Direccion);
            eM.Eliminar(ProveedorSeleccionado);
            ActualizarProveedor();
        }

        public void BuscarProveedor() 
        {
            
            LstProveedor = eM.Buscar(TxtCodigo,TxtRuc,TxtRazonSocial,FechaIni,FechaFin) as List<Proveedor>;           

        }

        public void ActualizarProveedor()
        {
            LstProveedor = eM.Buscar() as List<Proveedor>;
           
        }

        string Path;

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

        public void Cargar()
        {

            if (Path != "")
            {

                List<ProveedorxProducto> lista = new List<ProveedorxProducto>();

                String name = "Proveedor";
                String constr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path + ";Extended Properties=Excel 12.0;Persist Security Info=False";
                OleDbConnection con = new OleDbConnection(constr);
                OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
                con.Open();

                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                DataTable data = new DataTable();
                sda.Fill(data);
                DataTableReader ds = data.CreateDataReader();

                while (ds.Read())
                {
                    Proveedor p = new Proveedor();
                    p.Contacto = ds["Contacto"].ToString();
                    p.Direccion = ds["Direccion"].ToString();
                    p.Email = ds["Email"].ToString();
                    p.RazonSocial = ds["RazonSocial"].ToString();
                    p.Fax = ds["Fax"].ToString();
                    p.Telefono = ds["Telefono"].ToString();
                    p.Ruc = ds["Ruc"].ToString();
                    p.TelefonoContacto = ds["TelefonoContacto"].ToString();
                    p.Email = ds["Email"].ToString();

                    new ProveedorSQL().Agregar(p);

                }



            }


        }

        public void Importar() {

       
            BuscarPath();

            MessageBoxResult r = MessageBox.Show("Desea Importar el Archivo ? \n"+Path, "Importar", MessageBoxButton.YesNo);

            if (r == MessageBoxResult.Yes)
            {

                Cargar();
                MessageBox.Show("Se importó satisfactoriamente los proveedores ...");
                LstProveedor = new ProveedorSQL().Buscar() as List<Proveedor>;
            }
        
         
        }

    }
}
