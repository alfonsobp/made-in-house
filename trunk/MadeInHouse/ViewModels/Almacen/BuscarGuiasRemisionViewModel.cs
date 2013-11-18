using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models;
using MadeInHouse.Models.Almacen;
using MadeInHouse.Views.Almacen;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using MadeInHouse.DataObjects.Almacen;

namespace MadeInHouse.ViewModels.Almacen
{
    class BuscarGuiasRemisionViewModel : PropertyChangedBase
    {


        //Atributos
        private MyWindowManager win = new MyWindowManager();

        private string txtCodigo;
        public string TxtCodigo
        {
            get { return txtCodigo; }
            set { txtCodigo = value; NotifyOfPropertyChange(() => TxtCodigo); }
        }

        private DateTime txtFechaIni = new DateTime(DateTime.Now.Year, 1, 1);
        public DateTime TxtFechaIni
        {
            get { return txtFechaIni; }
            set { txtFechaIni = value; NotifyOfPropertyChange(() => TxtFechaIni); }
        }

        private DateTime txtFechaFin = new DateTime(DateTime.Now.Year, 12, 31);
        public DateTime TxtFechaFin
        {
            get { return txtFechaFin; }
            set { txtFechaFin = value; NotifyOfPropertyChange(() => TxtFechaFin); }
        }

        private List<string> cbTipo = new List<string>() { "GR-ORDEN DESPACHO", "GR-TRASLADO EXTERNO" };
        public List<string> CbTipo
        {
            get { return cbTipo; }
            set { cbTipo = value; NotifyOfPropertyChange(() => CbTipo); }
        }

        private string seleccionadoTipo;
        public string SeleccionadoTipo
        {
            get { return seleccionadoTipo; }
            set { seleccionadoTipo = value; NotifyOfPropertyChange(() => SeleccionadoTipo); }
        }

        private List<GuiaRemision> lstGuiaDeRemision;
        public List<GuiaRemision> LstGuiaDeRemision
        {
            get { return lstGuiaDeRemision; }
            set { lstGuiaDeRemision = value; NotifyOfPropertyChange(() => LstGuiaDeRemision); }
        }

        private GuiaRemision guiaSeleccionada;
        private MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel;
        private int accion;

        public int Accion
        {
            get { return accion; }
            set { accion = value; }
        }

        public BuscarGuiasRemisionViewModel() 
        {
            ActualizarGuiaRemision();
        }

        public BuscarGuiasRemisionViewModel(MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel, int p):this()
        {
            this.mantenerNotaDeIngresoViewModel = mantenerNotaDeIngresoViewModel;
            this.Accion = p;
        }

        public void SelectedItemChanged(object sender)
        {
            guiaSeleccionada = ((sender as DataGrid).SelectedItem as GuiaRemision);

        }


        public void AbrirMantenerGuiaDeRemision()
        {
            
            Almacen.MantenerGuiaDeRemisionViewModel abrirGuiaView = new Almacen.MantenerGuiaDeRemisionViewModel() ;
            win.ShowWindow(abrirGuiaView);

        }


        public void BuscarGuiaRemision()
        {
            LstGuiaDeRemision = new GuiaDeRemisionSQL().BuscarGuiaDeRemision(TxtCodigo, TxtFechaIni, TxtFechaFin, SeleccionadoTipo);
            
        }

        public void EditarGuiaDeRemision()
        {
            Almacen.MantenerGuiaDeRemisionViewModel abrirGuiaView = new Almacen.MantenerGuiaDeRemisionViewModel(guiaSeleccionada);
            win.ShowWindow(abrirGuiaView);
        }

        public void ActualizarGuiaRemision()
        {
            LstGuiaDeRemision = new GuiaDeRemisionSQL().BuscarGuiaDeRemision(null, TxtFechaIni, TxtFechaFin, null);
        }
    }
}
