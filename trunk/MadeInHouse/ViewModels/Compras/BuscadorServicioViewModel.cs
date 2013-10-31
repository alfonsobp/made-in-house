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
using System.Data.OleDb;
using System.Data;
using MadeInHouse.Models.Compras;
using MadeInHouse.Models;
using MadeInHouse.DataObjects.Compras;


namespace MadeInHouse.ViewModels.Compras
{
    class BuscadorServicioViewModel : PropertyChangedBase
    {
        //Constructores de la clase

        public BuscadorServicioViewModel()
        {
            ActualizarServicio();
        }



        //Atributos de la clase

        private MyWindowManager win = new MyWindowManager();

        private Servicio servicioSeleccionado;

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

        private List<Servicio> lstServicio;

        public List<Servicio> LstServicio
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
            Compras.agregarServicioViewModel obj = new Compras.agregarServicioViewModel(this);
            win.ShowWindow(obj);
        }

        public void EditarServicio()
        {
            //Compras.agregarServicioViewModel obj = new Compras.agregarServicioViewModel(servicioSeleccionado, this);
            Compras.agregarServicioViewModel obj = null;
            win.ShowWindow(obj);
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
