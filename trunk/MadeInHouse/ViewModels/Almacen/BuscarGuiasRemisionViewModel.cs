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

namespace MadeInHouse.ViewModels.Almacen
{
    class BuscarGuiasRemisionViewModel : PropertyChangedBase

    {

        private MyWindowManager win = new MyWindowManager();

        private string txtCodigo;

        public string TxtCodigo
        {
            get { return txtCodigo; }
            set { txtCodigo = value; NotifyOfPropertyChange(() => TxtCodigo); }
        }

        private string txtFecha;

        public string TxtFecha
        {
            get { return txtFecha; }
            set { txtFecha = value; NotifyOfPropertyChange(() => TxtFecha); }
        }

        private string cbTipo;

        public string CbTipo
        {
            get { return cbTipo; }
            set { cbTipo = value; NotifyOfPropertyChange(() => CbTipo); }
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

        public BuscarGuiasRemisionViewModel() { 
        
        }
        public BuscarGuiasRemisionViewModel(MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel, int p):this()
        {
            // TODO: Complete member initialization
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
            LstGuiaDeRemision = DataObjects.Almacen.GuiaDeRemisionSQL.BuscarGuiaDeRemision(null, DateTime.Now, null);
            
        }

        public void EditarGuiaDeRemision()
        {
            Almacen.MantenerGuiaDeRemisionViewModel abrirGuiaView = new Almacen.MantenerGuiaDeRemisionViewModel(guiaSeleccionada);
            win.ShowWindow(abrirGuiaView);
        }
    }
}
