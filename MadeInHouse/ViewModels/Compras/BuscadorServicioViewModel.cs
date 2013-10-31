using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Caliburn.Micro;
using MadeInHouse.Manager;
using MadeInHouse.Model;
using MadeInHouse.Views.Compras;


namespace MadeInHouse.ViewModels.Compras
{
    class BuscadorServicioViewModel:Screen
    {
        private MyWindowManager win = new MyWindowManager();

        private string proveedor;

        public string Proveedor
        {
            get { return proveedor; }
            set { proveedor = value; NotifyOfPropertyChange(() => Proveedor); }
        }

        private string producto;

        public string Producto
        {
            get { return producto; }
            set { producto = value; NotifyOfPropertyChange(() => Producto); }
        }

        private List<Servicio> lstServicio;

        public List<Servicio> LstServicio
        {
            get { return lstServicio; }
            set { lstServicio = value; NotifyOfPropertyChange(() => LstServicio); }
        }

        private Servicio servicioSeleccionado;

        public void SelectedItemChanged(object sender)
        {
            servicioSeleccionado = ((sender as DataGrid).SelectedItem as Servicio);

        }

        EntityManager eM = new TableManager().getInstance(EntityName.Servicio);

        public void NuevoServicio()
        {
            Compras.agregarServicioViewModel obj = new Compras.agregarServicioViewModel { DisplayName = "Nuevo Servicio" };
            win.ShowWindow(obj);
        }
        public void EditarServicio()
        {
            Compras.agregarServicioViewModel obj = new Compras.agregarServicioViewModel(servicioSeleccionado) { DisplayName = "Editar Servicio" };
            win.ShowWindow(obj);
        }

        public void EliminarServicio()
        {
            
        }

        public void BuscarServicio()
        {
            lstServicio = eM.Buscar(null) as List<Servicio>;
            NotifyOfPropertyChange("LstServicio");


        }

        public void ActualizarServicio()
        {
           
        }
    }
}
