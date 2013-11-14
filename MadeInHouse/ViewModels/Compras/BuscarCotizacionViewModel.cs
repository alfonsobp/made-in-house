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
    class BuscarCotizacionViewModel:PropertyChangedBase
    {
        
        //Costructores
        public BuscarCotizacionViewModel()
        {
            ActualizarCotizacion();
            lstOpciones = new List<string>();
            lstOpciones.Add("ATENDIDO");
            lstOpciones.Add("PENDIENTE");
            lstOpciones.Add("CANCELADO");
        }




        //Atributos de la clase
        private MyWindowManager win = new MyWindowManager();
        private Cotizacion cotizacionSeleccionada;
        CotizacionSQL eM = new CotizacionSQL();



        private List<string> lstOpciones;
        public List<string> LstOpciones
        {
            get { return lstOpciones; }
            set { lstOpciones = value; NotifyOfPropertyChange(() => LstOpciones); }
        }


        private string selectedEst;
        public string SelectedEst
        {
            get { return selectedEst; }
            set { selectedEst = value; NotifyOfPropertyChange(() => SelectedEst); }
        }

        private string txtCodigo;
        public string TxtCodigo
        {
            get { return txtCodigo; }
            set { txtCodigo = value; NotifyOfPropertyChange(() => TxtCodigo); }
        }

        private string txtProveedor;
        public string TxtProveedor
        {
            get { return txtProveedor; }
            set { txtProveedor = value; NotifyOfPropertyChange(() => TxtProveedor); }
        }

        private Proveedor prov;
        public Proveedor Prov
        {
            get { return prov; }
            set { prov = value; NotifyOfPropertyChange(() => Prov); }
        }


        private DateTime txtFechaRegistro = new DateTime(DateTime.Now.Year, 1,1);
        public DateTime TxtFechaRegistro
        {
            get { return txtFechaRegistro; }
            set { txtFechaRegistro = value; NotifyOfPropertyChange(() => TxtFechaRegistro); }
        }

        private DateTime txtFechaHasta = new DateTime(DateTime.Now.Year, 12, 31);
        public DateTime TxtFechaHasta
        {
            get { return txtFechaHasta; }
            set { txtFechaHasta = value; NotifyOfPropertyChange(() => TxtFechaHasta); }
        }


        private List<Cotizacion> lstCotizacion;
        public List<Cotizacion> LstCotizacion
        {
            get { return lstCotizacion; }
            set { lstCotizacion = value; NotifyOfPropertyChange(() => LstCotizacion); }
        }


        //Funciones de la clase
        public void SelectedItemChanged(object sender)
        {
            cotizacionSeleccionada = ((sender as DataGrid).SelectedItem as Cotizacion);   
        }

        public void NuevaCotizacion()
        {
            Compras.NuevaCotizacionViewModel obj = new Compras.NuevaCotizacionViewModel(this);
            win.ShowWindow(obj);
        }

        public void EditarCotizacion()
        {

            Compras.NuevaCotizacionViewModel obj = new Compras.NuevaCotizacionViewModel(cotizacionSeleccionada, this);
            win.ShowWindow(obj);
        }

        public void ActualizarCotizacion()
        {
            LstCotizacion = eM.Buscar() as List<Cotizacion>;
        }

        public void BuscarCotizacion()
        {
            if (Prov == null)
                Prov.CodProveedor = "";

            LstCotizacion = eM.Buscar(TxtCodigo, Prov.CodProveedor, SelectedEst, TxtFechaRegistro, TxtFechaHasta) as List<Cotizacion>;
        }

        public void BuscarProveedor()
        {
            MadeInHouse.Models.MyWindowManager w = new MadeInHouse.Models.MyWindowManager();
            w.ShowWindow(new BuscadorProveedorViewModel(this));
        }

        public void EliminarCotizacion()
        {
            int k = new CotizacionSQL().Eliminar(cotizacionSeleccionada);
        }

        public void Limpiar()
        {
            TxtCodigo = null;
            Prov = null;
            LstCotizacion = null;
        }

    }
}
