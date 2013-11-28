using Caliburn.Micro;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.Models.Compras;
using MadeInHouse.ViewModels.Layouts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.OleDb;
using System.Windows;


namespace MadeInHouse.ViewModels.Compras
{
    [Export(typeof(BuscadorProveedorViewModel))]
    class BuscadorProveedorViewModel : Screen
    {

        #region ATRIBUTOS
        private readonly IWindowManager _windowManager;

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

        private DateTime fechaIni = new DateTime(DateTime.Now.Year, 1, 1);

        public DateTime FechaIni
        {
            get { return fechaIni; }
            set { fechaIni = value; NotifyOfPropertyChange(() => FechaIni); }
        }

        private DateTime fechaFin = new DateTime(DateTime.Now.Year, 12, 31);

        public DateTime FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; NotifyOfPropertyChange(() => FechaFin); }
        }

        private List<Proveedor> lstProveedor;

        public List<Proveedor> LstProveedor
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

        #endregion

        #region constructores

        [ImportingConstructor]
        public BuscadorProveedorViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
            ActualizarProveedor();
        }

        generarOrdenCompraViewModel g;
        public BuscadorProveedorViewModel(IWindowManager windowmanager, generarOrdenCompraViewModel g)
            : this(windowmanager)
        {
            this.g = g;
        }

        CatalogoProductoProveedorViewModel p;
        public BuscadorProveedorViewModel(IWindowManager windowmanager, CatalogoProductoProveedorViewModel p)
            : this(windowmanager)
        {
            this.p = p;
        }

        NuevaCotizacionViewModel cp;
        public BuscadorProveedorViewModel(IWindowManager windowmanager, NuevaCotizacionViewModel cp)
            : this(windowmanager)
        {
            this.cp = cp;
        }

        agregarServicioViewModel s;
        public BuscadorProveedorViewModel(IWindowManager windowmanager, agregarServicioViewModel s)
            : this(windowmanager)
        {
            this.s = s;
        }

        BuscarCotizacionViewModel bc;
        public BuscadorProveedorViewModel(IWindowManager windowmanager, BuscarCotizacionViewModel bc)
            : this(windowmanager)
        {
            this.bc = bc;
        }

        BuscarDocumentoViewModel bd;
        public BuscadorProveedorViewModel(IWindowManager windowmanager, BuscarDocumentoViewModel bd)
            : this(windowmanager)
        {
            this.bd = bd;
        }

        #endregion

        public void SelectedItemChanged()
        {
            if (ProveedorSeleccionado != null)
            {
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

                if (g != null)
                {

                    g.Prov = ProveedorSeleccionado;
                    g.BuscarDispo = false;
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
                bc.Prov = ProveedorSeleccionado;
                TryClose();
            }

            if (bd != null)
            {
                bd.Prov = ProveedorSeleccionado;
                TryClose();
            }
        }


        public void Limpiar()
        {
            TxtCodigo = null;
            TxtRuc = null;
            TxtRazonSocial = null;
            LstProveedor = null;
            FechaIni = new DateTime(DateTime.Now.Year, 1, 1);
            FechaFin = new DateTime(DateTime.Now.Year, 12, 31);
        }

        public void NuevoProveedor()
        {
            _windowManager.ShowWindow(new MantenerProveedorViewModel(this));
        }

        public void EditarProveedor()
        {
            if (ProveedorSeleccionado != null)
            {
                _windowManager.ShowWindow(new MantenerProveedorViewModel(ProveedorSeleccionado, this));
            }
            else
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "No ha seleccionado ningun proveedor.."));
            }
        }

        public void EliminarProveedor()
        {

            if (ProveedorSeleccionado != null)
            {

                MessageBoxResult r = MessageBox.Show("Esta seguro que desea deshabilitar a este proveedor?", "AVISO", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

                if (r == MessageBoxResult.Yes)
                {
                    eM.Eliminar(ProveedorSeleccionado);
                    _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Ha sido deshabilitado el proveedor."));
                    ActualizarProveedor();
                }

            }
            else
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "No ha seleccionado ningun proveedor.."));
            }
        }

        public void BuscarProveedor()
        {
            LstProveedor = eM.Buscar(TxtCodigo, TxtRuc, TxtRazonSocial, FechaIni, FechaFin) as List<Proveedor>;
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

                try
                {
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
                catch (Exception e)
                {
                    _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Revisar la plantilla utilizada, ocurrio un error."));
                }
            }
        }

        public void Importar()
        {


            BuscarPath();

            MessageBoxResult r = MessageBox.Show("Desea Importar desde la ruta " + Path + " ? ", "Importación", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (r == MessageBoxResult.Yes)
            {

                Cargar();
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Se Importaron Satisfactoriamente los Proveedores ingresados.."));
                LstProveedor = new ProveedorSQL().Buscar() as List<Proveedor>;
            }
        }
    }
}