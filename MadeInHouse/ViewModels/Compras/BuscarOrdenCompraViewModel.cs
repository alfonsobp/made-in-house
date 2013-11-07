using MadeInHouse.Models.Compras;
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
    class BuscarOrdenCompraViewModel:Screen
    {

        string idOrdenCompra;

        public string IdOrdenCompra
        {
            get { return idOrdenCompra; }
            set { idOrdenCompra = value; NotifyOfPropertyChange("IdOrdenCompra"); }
        }
        string rSProveedor;

        public string RSProveedor
        {
            get { return rSProveedor; }
            set { rSProveedor = value; NotifyOfPropertyChange("RSProveedor"); }
        }

        List<String> lstEstados = new List<String>() { "TODOS", "CANCELADA", "PRE EMITIDA", "EN EJECUCION", "FINALIZADA" };

        public List<String> LstEstados
        {
            get { return lstEstados; }
            set { lstEstados = value; NotifyOfPropertyChange("LstEstados"); }
        }
        string selectedEstado = "TODOS";

        public string SelectedEstado
        {
            get { return selectedEstado; }
            set { selectedEstado = value; NotifyOfPropertyChange("SelectedEstado"); }
        }

        DateTime fechaIni = new DateTime(DateTime.Now.Year, 1, 1);

        public DateTime FechaIni
        {
            get { return fechaIni; }
            set { fechaIni = value; NotifyOfPropertyChange("FechaIni"); }
        }
        DateTime fechaFin = new DateTime(DateTime.Now.Year, 12, 31);

        public DateTime FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; NotifyOfPropertyChange("FechaFin"); }
        }

        public int getEstado(string path) {

            if (path == "TODOS") return 4;
            if (path == "CANCELADA") return 0;
            if(path == "PRE EMITIDA") return 1;
            if(path == "EN EJECUCION") return 2;
            if (path == "FINALIZADA") return 3;

            return 4;
        }

        List<OrdenCompra> lstOrdenes;

        public List<OrdenCompra> LstOrdenes
        {
            get { return lstOrdenes; }
            set { lstOrdenes = value; NotifyOfPropertyChange("LstOrdenes"); }
        }


        public void Buscar() { 
        
        LstOrdenes =   new OrdenCompraSQL().Buscar(IdOrdenCompra, RSProveedor, getEstado(SelectedEstado), FechaIni, FechaFin) as List<OrdenCompra>;
        
        }

       
        
           
        private MyWindowManager win = new MyWindowManager();

        public BuscarOrdenCompraViewModel()
        {
           
        }

        registrarDocumentosViewModel r;
        public BuscarOrdenCompraViewModel(registrarDocumentosViewModel r)
        {
            this.r = r;
        }

        private OrdenCompra ordenSeleccionada;
        public void SelectedItemChanged(object sender)
        {
            ordenSeleccionada = ((sender as DataGrid).SelectedItem as OrdenCompra);

            if (r != null)
            {
                r.Ord = ordenSeleccionada;
                TryClose();
            }
        }


        public void NuevaOrden()
        {
            Compras.generarOrdenCompraViewModel obj = new Compras.generarOrdenCompraViewModel ();
            win.ShowWindow(obj);
        }
        public void EditarOrden()
        {

            Compras.generarOrdenCompraViewModel obj = new Compras.generarOrdenCompraViewModel { DisplayName = "Editar Orden de Compra" };
            win.ShowWindow(obj);
        }

        #region Busqueda desde Almacen
        private int ventanaAccion = 0;
        private Almacen.MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel;

        public BuscarOrdenCompraViewModel(Almacen.MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel, int accionVentana)
        {
            // TODO: Complete member initialization
            this.mantenerNotaDeIngresoViewModel = mantenerNotaDeIngresoViewModel;
            this.ventanaAccion = accionVentana;
        }

        
        
        #endregion

        #region Acciones Doble Click
        public void Acciones(object sender)
        {
            if (ventanaAccion == 0)
            {
                //Actualizar();
            }
            else if (ventanaAccion == 1)
            {

                /*productoSel = ((sender as DataGrid).SelectedItem as Producto);
                if (ventaRegistrarViewModel != null)
                {
                    ventaRegistrarViewModel.Prod = productoSel;
                    this.TryClose();
                }*/
            }
            else if (ventanaAccion == 2)
            {
                
            }
            else if (ventanaAccion == 3)
            {
                
            }
        }
        #endregion
    }
}
