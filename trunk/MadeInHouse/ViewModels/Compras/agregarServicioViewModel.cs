using Caliburn.Micro;
using MadeInHouse.DataObjects;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Models.Compras;
using MadeInHouse.ViewModels.Layouts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Controls;

namespace MadeInHouse.ViewModels.Compras
{
    [Export(typeof(agregarServicioViewModel))]
    class agregarServicioViewModel : Screen
    {
        #region constructores

        //Constructores de la clase
        [ImportingConstructor]
        public agregarServicioViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
            //Servicio agregado desde la ventana principal
            indicador = 1;
            Id = usql.ObtenerMaximoID("Servicio", "idServicio");
            TxtCodigo = "SERV-" + (1000000 + Id + 1).ToString();

        }

        public agregarServicioViewModel(IWindowManager windowmanager, string codProveedor)
        {
            _windowManager = windowmanager;
            //Servicio agregado desde la ventana mantenimiento de proveedor
            txtProveedor = codProveedor;
            indicador = 1;
        }


        public agregarServicioViewModel(IWindowManager windowmanager, Servicio s, BuscadorServicioViewModel m)
        {
            _windowManager = windowmanager;
            //Servicio para editar del buscador
            txtCodigo = s.CodServicio;
            txtNombre = s.Nombre;
            txtProveedor = DataObjects.Compras.ServicioSQL.getCODfromProv(s.IdProveedor);
            txtDescripcion = s.Descripcion;

            LstProducto = sp.Buscar(s.IdServicio) as List<ServicioxProducto>;

            indicador = 2;
            model = m;
        }


        public agregarServicioViewModel(IWindowManager windowmanager, BuscadorServicioViewModel m)
        {
            _windowManager = windowmanager;
            //Servicio para insertar desde la ventana principal o del buscador
            indicador = 1;
            model = m;
            Id = usql.ObtenerMaximoID("Servicio", "idServicio");
            TxtCodigo = "SERV-" + (1000000 + Id + 1).ToString();
        }


        public agregarServicioViewModel(IWindowManager windowmanager, Servicio s, BuscadorServicioViewModel m, Ventas.VentaRegistrarViewModel ventaRegistrarViewModel, int ventanaAccion)
        {
            _windowManager = windowmanager;
            // TODO: Complete member initialization
            this.servicioSeleccionado = s;
            this.buscadorServicioViewModel = m;
            this.ventaRegistrarViewModel = ventaRegistrarViewModel;
            this.ventanaAccion = ventanaAccion;

            //Servicio para editar del buscador
            txtCodigo = s.CodServicio;
            txtNombre = s.Nombre;
            txtProveedor = DataObjects.Compras.ServicioSQL.getCODfromProv(s.IdProveedor);
            txtDescripcion = s.Descripcion;

            LstProducto = sp.Buscar(s.IdServicio) as List<ServicioxProducto>;

            indicador = 2;
            model = m;
        }

        public agregarServicioViewModel(IWindowManager windowmanager, Servicio s, BuscadorServicioViewModel m, Ventas.VentaCajeroRegistrarViewModel ventaCajeroRegistrarViewModel, int p)
        {
            _windowManager = windowmanager;
            // TODO: Complete member initialization
            this.servicioSeleccionado = s;
            this.buscadorServicioViewModel = m;
            this.ventaCajeroRegistrarViewModel = ventaCajeroRegistrarViewModel;
            this.ventanaAccion = p;

            //Servicio para editar del buscador
            txtCodigo = s.CodServicio;
            txtNombre = s.Nombre;
            txtProveedor = DataObjects.Compras.ServicioSQL.getCODfromProv(s.IdProveedor);
            txtDescripcion = s.Descripcion;

            LstProducto = sp.Buscar(s.IdServicio) as List<ServicioxProducto>;

            indicador = 2;
            model = m;
        }

        #endregion

        private readonly IWindowManager _windowManager;

        private int ventanaAccion = 0;

        public void Acciones(object sender)
        {
            if (ventanaAccion == 1)
            {
                Seleccionado = ((sender as DataGrid).SelectedItem as ServicioxProducto);
                if (ventaRegistrarViewModel != null)
                {
                    ventaRegistrarViewModel.Serv = Seleccionado;
                    this.TryClose();
                    this.buscadorServicioViewModel.TryClose();
                }
            }
            if (ventanaAccion == 2)
            {
                Seleccionado = ((sender as DataGrid).SelectedItem as ServicioxProducto);
                if (ventaCajeroRegistrarViewModel != null)
                {
                    ventaCajeroRegistrarViewModel.Serv = Seleccionado;
                    this.TryClose();
                    this.buscadorServicioViewModel.TryClose();
                }
            }
        }


        //Atributos de la clase

        int Id = 0;

        BuscadorServicioViewModel model;

        private int indicador;

        ServicioxProductoSQL sp = new ServicioxProductoSQL();

        UtilesSQL usql = new UtilesSQL();





        private ServicioxProducto seleccionado;

        public ServicioxProducto Seleccionado
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


        private string txtCodigo;

        public string TxtCodigo
        {
            get { return txtCodigo; }
            set { txtCodigo = value; NotifyOfPropertyChange(() => TxtCodigo); }
        }

        private string txtNombre;

        public string TxtNombre
        {
            get { return txtNombre; }
            set { txtNombre = value; NotifyOfPropertyChange(() => TxtNombre); }
        }

        private string txtProveedor;

        public string TxtProveedor
        {
            get { return txtProveedor; }
            set { txtProveedor = value; NotifyOfPropertyChange(() => TxtProveedor); }
        }

        private string txtDescripcion;

        public string TxtDescripcion
        {
            get { return txtDescripcion; }
            set { txtDescripcion = value; NotifyOfPropertyChange(() => TxtDescripcion); }
        }

        private MadeInHouse.Models.Compras.Proveedor prov;

        public MadeInHouse.Models.Compras.Proveedor Prov
        {
            get { return prov; }
            set { prov = value; NotifyOfPropertyChange(() => Prov); }
        }

        List<ServicioxProducto> lstProducto;
        private Servicio servicioSeleccionado;
        private BuscadorServicioViewModel buscadorServicioViewModel;
        private Ventas.VentaRegistrarViewModel ventaRegistrarViewModel;
        private Ventas.VentaCajeroRegistrarViewModel ventaCajeroRegistrarViewModel;
        private int p;

        public List<ServicioxProducto> LstProducto
        {
            get { return lstProducto; }
            set { lstProducto = value; NotifyOfPropertyChange(() => LstProducto); }
        }




        //Funciones de la clase


        public void BuscarProveedor()
        {
            _windowManager.ShowWindow(new BuscadorProveedorViewModel(_windowManager, this));
        }

        public Boolean validar(MadeInHouse.Models.Compras.Servicio s)
        {

            if ((s.Descripcion == null) || (s.Nombre == null))
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Tiene campos incompletos , rellenar porfavor"));
                return false;
            }
            else
            {
                return true;
            }
        }

        public void GuardarServicio()
        {
            int k, y, i;
            Servicio s = new Servicio();

            s.IdProveedor = Prov.IdProveedor;
            s.Nombre = TxtNombre;
            s.Descripcion = TxtDescripcion;
            s.CodServicio = TxtCodigo;

            if (validar(s) == true)
            {

                if (indicador == 1)
                {
                    k = new ServicioSQL().Agregar(s);
                    Id = usql.ObtenerMaximoID("Servicio", "idServicio");

                    if (Id == 0) Id = 1;

                    if (LstProducto != null)
                    {
                        for (i = 0; i < LstProducto.Count; i++)
                        {
                            LstProducto[i].IdServicio = Id;
                            y = sp.InsertarValidado(LstProducto[i]);
                        }
                    }

                    if (k == 0)
                        _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Ocurrio un error"));
                    else
                        _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Servicio Registrado \n\nCodigo = " + txtCodigo + "\nNombre = " + txtNombre +
                                        "\nProveedor = " + txtProveedor + "\nDescripcion = " + txtDescripcion));
                }

                if (indicador == 2)
                {

                    k = new ServicioSQL().Actualizar(s);

                    if (k == 0)
                        _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Ocurrio un error"));
                    else
                        _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Servicio Editado \n\nCodigo = " + txtCodigo + "\nNombre = " + txtNombre +
                                        "\nProveedor = " + txtProveedor + "\nDescripcion = " + txtDescripcion));
                }

                if (model != null)
                    model.ActualizarServicio();
            }

        }



        //Funciones de carga masiva de datos desde Excel

        public void Cargar()
        {

            if (path != "")
            {

                List<ServicioxProducto> lista = new List<ServicioxProducto>();
                MessageBox.Show(path);
                String name = "Catalogo-Serv";
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

                    ServicioxProducto cs = new ServicioxProducto();

                    cs.Producto = new Producto();
                    cs.Producto.CodigoProd = ds["Codigo"].ToString();
                    cs.Producto.Nombre = ds["Nombre"].ToString();
                    cs.Precio = Convert.ToDouble(ds["Precio"].ToString());

                    lista.Add(cs);

                }

                LstProducto = lista;
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

        public void Importar()
        {
            BuscarPath();

            MessageBoxResult r = MessageBox.Show("Desea Importar el Archivo ? ", "Importar", MessageBoxButton.YesNo);

            if (r == MessageBoxResult.Yes)
            {

                Cargar();
            }
        }
    }
}