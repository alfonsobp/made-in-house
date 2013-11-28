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
    class BuscadorServicioViewModel : Screen
    {
        //Constructores de la clase

        public BuscadorServicioViewModel()
        {
            ActualizarServicio();
        }

        private int ventanaAccion = 0;
        public BuscadorServicioViewModel(Ventas.VentaRegistrarViewModel ventaRegistrarViewModel, int ventanaAccion)
        {
            // TODO: Complete member initialization
            this.ventaRegistrarViewModel = ventaRegistrarViewModel;
            this.ventanaAccion = ventanaAccion;
            ActualizarServicio();
        }

        public BuscadorServicioViewModel(Ventas.VentaCajeroRegistrarViewModel ventaCajeroRegistrarViewModel, int p)
        {
            // TODO: Complete member initialization
            this.ventaCajeroRegistrarViewModel = ventaCajeroRegistrarViewModel;
            this.ventanaAccion = p;
            ActualizarServicio();
        }



        //Atributos de la clase

        private MyWindowManager win = new MyWindowManager();

        private MadeInHouse.Models.Compras.Servicio servicioSeleccionado;

        ServicioSQL eM = new ServicioSQL();
        


        private string txtProveedor;

        public string TxtProveedor
        {
            get { return txtProveedor; }
            set { txtProveedor = value; NotifyOfPropertyChange(() => TxtProveedor); }
        }

        private string txtNombre;

        public string TxtNombre
        {
            get { return txtNombre; }
            set { txtNombre = value; NotifyOfPropertyChange(() => TxtNombre); }
        }

        private string txtProducto;

        public string TxtProducto
        {
            get { return txtProducto; }
            set { txtProducto = value; NotifyOfPropertyChange(() => TxtProducto); }
        }

        private List<MadeInHouse.Models.Compras.Servicio> lstServicio;
        private Ventas.VentaRegistrarViewModel ventaRegistrarViewModel;
        private Ventas.VentaCajeroRegistrarViewModel ventaCajeroRegistrarViewModel;
        private int p;

        public List<MadeInHouse.Models.Compras.Servicio> LstServicio
        {
            get { return lstServicio; }
            set { lstServicio = value; NotifyOfPropertyChange(() => LstServicio); }
        }




        //Funciones de la clase
        
        public void SelectedItemChanged(object sender)
        {
            servicioSeleccionado = ((sender as DataGrid).SelectedItem as Servicio);

        }
        

        public void NuevoServicio()
        {
            Compras.agregarServicioViewModel obj = new Compras.agregarServicioViewModel(win, this);
            win.ShowWindow(obj);
        }

        public void ControlServicios()
        {
            Compras.ControlServiciosViewModel obj = new Compras.ControlServiciosViewModel();
            win.ShowWindow(obj);
        }

        public void EditarServicio()
        {
            if (ventanaAccion == 0)
            {
                Compras.agregarServicioViewModel obj = new Compras.agregarServicioViewModel(win, servicioSeleccionado, this);
                win.ShowWindow(obj);
            }
            else if (ventanaAccion == 1)
            {
                Compras.agregarServicioViewModel obj = new Compras.agregarServicioViewModel(win, servicioSeleccionado, this, ventaRegistrarViewModel, 1);
                win.ShowWindow(obj);
            }
            else
            {
                Compras.agregarServicioViewModel obj = new Compras.agregarServicioViewModel(win, servicioSeleccionado, this, ventaCajeroRegistrarViewModel, 2);
                win.ShowWindow(obj);
            }

        }


        public void EliminarServicio()
        {
            eM.Eliminar(servicioSeleccionado);
            ActualizarServicio();
        }
        
        public void BuscarServicio()
        {
            LstServicio = eM.Buscar(TxtProveedor, TxtNombre, TxtProducto) as List<Servicio>;

        }

        public void ActualizarServicio()
        {
            LstServicio = eM.Buscar() as List<Servicio>;
        }
        
    }
}
