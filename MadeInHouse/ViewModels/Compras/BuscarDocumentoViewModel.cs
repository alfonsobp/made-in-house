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
    class BuscarDocumentoViewModel:PropertyChangedBase
    {
        //Costructores

        public BuscarDocumentoViewModel()
        {
            ActualizarDocumentos();
            lstOpciones = new List<string>();
            lstOpciones.Add("COMPLETO");
            lstOpciones.Add("PENDIENTE");
            lstOpciones.Add("CANCELADO");
        }




        //Atributos de la clase

        private MyWindowManager win = new MyWindowManager();

        private DocPagoProveedor docSeleccionado;

        DocPagoProveedorSQL eM = new DocPagoProveedorSQL();



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


        private DateTime txtFechaDesde = new DateTime(DateTime.Now.Year, 1,1);

        public DateTime TxtFechaDesde
        {
            get { return txtFechaDesde; }
            set { txtFechaDesde = value; NotifyOfPropertyChange(() => TxtFechaDesde); }
        }

        private DateTime txtFechaHasta = new DateTime(DateTime.Now.Year, 12, 31);

        public DateTime TxtFechaHasta
        {
            get { return txtFechaHasta; }
            set { txtFechaHasta = value; NotifyOfPropertyChange(() => TxtFechaHasta); }
        }

        private double txtPago;

        public double TxtPago
        {
            get { return txtPago; }
            set { txtPago = value; NotifyOfPropertyChange(() => TxtPago); }
        }


        private List<DocPagoProveedor> lstDocsPago;

        public List<DocPagoProveedor> LstDocsPago
        {
            get { return lstDocsPago; }
            set { lstDocsPago = value; NotifyOfPropertyChange(() => LstDocsPago); }
        }


        //Funciones de la clase

        public void SelectedItemChanged(object sender)
        {
            //cotizacionSeleccionada.Proveedor = new Proveedor();
            docSeleccionado = ((sender as DataGrid).SelectedItem as DocPagoProveedor);
            //MessageBox.Show("id = " + cotizacionSeleccionada.IdCotizacion + " prov = " + cotizacionSeleccionada.Proveedor.IdProveedor);
        }



        public void NuevoDocumento()
        {

            Compras.registrarDocumentosViewModel obj = new Compras.registrarDocumentosViewModel();
            win.ShowWindow(obj);
        }
        public void EditarDocumento()
        {


            Compras.registrarDocumentosViewModel obj = new Compras.registrarDocumentosViewModel();
            win.ShowWindow(obj);
        }

        public void ActualizarDocumentos()
        {
            LstDocsPago = eM.Buscar() as List<DocPagoProveedor>;
        }

        public void BuscarDocsPago()
        {
            if (TxtProveedor != null)
                TxtProveedor = Prov.CodProveedor;

            LstDocsPago = eM.Buscar(TxtCodigo, TxtProveedor, SelectedEst, TxtFechaDesde, TxtFechaHasta) as List<DocPagoProveedor>;
        }

        public void BuscarProveedor()
        {
            MadeInHouse.Models.MyWindowManager w = new MadeInHouse.Models.MyWindowManager();
            w.ShowWindow(new BuscadorProveedorViewModel(this));
        }

        public void pagoParcial()
        {

        }

    }
}
