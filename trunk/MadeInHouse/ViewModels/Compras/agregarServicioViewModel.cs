using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Model;
using System.Data;
using MadeInHouse.Views.Compras;
using System.Windows;
using System.Data.OleDb;
using System.Collections.ObjectModel;
using MadeInHouse.Manager;

namespace MadeInHouse.ViewModels.Compras
{
    class agregarServicioViewModel : PropertyChangedBase
    {

        //Constructores de la clase

        public agregarServicioViewModel()
        {
            //Servicio agregado desde la ventana principal
            indicador = 1;
        }

        public agregarServicioViewModel(string codProveedor)
        {
            //Servicio agregado desde la ventana mantenimiento de proveedor
            txtProveedor = codProveedor;
            indicador = 1;
        }


        public agregarServicioViewModel(Servicio s, BuscadorServicioViewModel m)
        {

            //Servicio para editar del buscador
            txtCodigo = s.CodServicio;
            txtNombre = s.Nombre;
            txtProveedor = Manager.ServicioManager.getCODfromProv(s.IdProveedor);
            txtDescripcion = s.Descripcion;
            indicador = 2;
            model = m;
        }


        public agregarServicioViewModel(BuscadorServicioViewModel m)
        {
            //Servicio para insertar desde la ventana principal o del buscador
            indicador = 1;
            model = m;
        }



        //Atributos de la clase

        int Id;

        BuscadorServicioViewModel model;

        EntityManager eM = new TableManager().getInstance(EntityName.Servicio);

        private int indicador;



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

        List<ServicioxProducto> lstProducto;

        public List<ServicioxProducto> LstProducto
        {
            get { return lstProducto; }
            set { lstProducto = value; NotifyOfPropertyChange(() => LstProducto); }
        }




        //Funciones de la clase

        public Boolean validar(Servicio s)
        {

            if (s.Descripcion == null || s.Nombre == null)
            {
                MessageBox.Show("Tiene campos incompletos , rellenar porfavor");
                return false;
            }
            else
            {
                return true;
            }
        }

        public void GuardarServicio()
        {
            int k;
            Servicio s = new Servicio();

            s.IdServicio = Id;
            s.IdProveedor = Manager.ServicioManager.getIDfromProv(TxtProveedor);
            s.Nombre = TxtNombre;
            s.Descripcion = TxtDescripcion;
            s.CodServicio = TxtCodigo;


            if (validar(s) == true)
            {
                if (indicador == 1)
                {
                    k = eM.Agregar(s);

                    if (k == 0)
                        MessageBox.Show("Ocurrio un error");
                    else
                        MessageBox.Show("Servicio Registrado \n\nCodigo = " + txtCodigo + "\nNombre = " + txtNombre +
                                        "\nProveedor = " + txtProveedor + "\nDescripcion = " + txtDescripcion);
                }

                if (indicador == 2)
                {

                    k = eM.Actualizar(s);

                    if (k == 0)
                        MessageBox.Show("Ocurrio un error");
                    else
                        MessageBox.Show("Servicio Editado \n\nCodigo = " + txtCodigo + "\nNombre = " + txtNombre +
                                        "\nProveedor = " + txtProveedor + "\nDescripcion = " + txtDescripcion);

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
                    cs.Producto.CodProducto = ds["Codigo"].ToString();
                    cs.Producto.Nombre = ds["Nombre"].ToString();
                    cs.Precio = Convert.ToDouble(ds["Precio"].ToString());

                    ServicioxProductoManager sp = new ServicioxProductoManager();

                    lista.Add(cs);
                    

                    //int k = sp.Agregar(cs);
                    
                    //int k = pp.Insertar(cp);


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
