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

        private List<string> cbEst = new List<string>() { "EMITIDA", "RECIBIDA" };
        public List<string> CbEst
        {
            get { return cbEst; }
            set { cbEst = value; NotifyOfPropertyChange(() => CbEst); }
        }

        private List<string> cbTipo = new List<string>() { "GR-ORDEN DESPACHO", "GR-TRASLADO EXTERNO" };
        public List<string> CbTipo
        {
            get { return cbTipo; }
            set { cbTipo = value; NotifyOfPropertyChange(() => CbTipo); }
        }

        private string seleccionadoEst;
        public string SeleccionadoEst
        {
            get { return seleccionadoEst; }
            set { seleccionadoEst = value; NotifyOfPropertyChange(() => SeleccionadoEst); }
        }

        private string seleccionadoTipo;
        public string SeleccionadoTipo
        {
            get { return seleccionadoTipo; }
            set { seleccionadoTipo = value; NotifyOfPropertyChange(() => SeleccionadoTipo); }
        }

        private Almacenes alm;
        public Almacenes Alm
        {
            get { return alm; }
            set { alm = value; NotifyOfPropertyChange(() => Alm); }
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

        public void BuscarAlmacen()
        {

                MyWindowManager w = new MyWindowManager();
                w.ShowWindow(new BuscarAlmacenViewModel(this));
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

        public void Acciones(object sender)
        {
            if (accion == 1) {
                guiaSeleccionada = ((sender as DataGrid).SelectedItem as GuiaRemision);
                if (mantenerNotaDeIngresoViewModel != null)
                {
                    mantenerNotaDeIngresoViewModel.SelectedGuia = guiaSeleccionada;
                    mantenerNotaDeIngresoViewModel.TxtDocId = guiaSeleccionada.IdGuia;
                    mantenerNotaDeIngresoViewModel.TxtDoc = guiaSeleccionada.CodGuiaRem;
                }
            
            }
        }

        public void AbrirMantenerGuiaDeRemision()
        {
            
            Almacen.MantenerGuiaDeRemisionViewModel abrirGuiaView = new Almacen.MantenerGuiaDeRemisionViewModel() ;
            win.ShowWindow(abrirGuiaView);

        }


        public void BuscarGuiaRemision()
        {
            int estado = 0;
            List<GuiaRemision> list = new List<GuiaRemision>();
            List<GuiaRemision> NewList = new List<GuiaRemision>();

            if (!String.IsNullOrEmpty(SeleccionadoEst))
            {
                if (SeleccionadoEst.Equals("EMITIDA"))
                    estado = 1;

                if (SeleccionadoEst.Equals("RECIBIDA"))
                    estado = 2;
            }

            list = new GuiaDeRemisionSQL().BuscarGuiaDeRemision(TxtCodigo, estado, SeleccionadoTipo);

            if (!String.IsNullOrEmpty(Alm.CodAlmacen))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].AlmOrigen.CodAlmacen == Alm.CodAlmacen)
                        NewList.Add(list[i]);
                }

                if (NewList != null)
                    LstGuiaDeRemision = new List<GuiaRemision>(NewList);
                else
                    LstGuiaDeRemision = new List<GuiaRemision>(list);
            }

            else
            {
                LstGuiaDeRemision = new List<GuiaRemision>(list);
            }

        }

        public void EditarGuiaDeRemision()
        {
            Almacen.MantenerGuiaDeRemisionViewModel abrirGuiaView = new Almacen.MantenerGuiaDeRemisionViewModel(guiaSeleccionada);
            win.ShowWindow(abrirGuiaView);
            ActualizarGuiaRemision();
        }

        public void ActualizarGuiaRemision()
        {
            Alm = null;
            SeleccionadoEst = null;
            SeleccionadoTipo = null;
            TxtCodigo = null;
            LstGuiaDeRemision = new GuiaDeRemisionSQL().BuscarGuiaDeRemision(null, 0, null);
        }

        public void RecibirGuia()
        {

            if (guiaSeleccionada != null)
            {
                guiaSeleccionada.Estado = 2;
                Almacenes almDesp = new Almacenes(); 

                List<Almacenes> list = new AlmacenSQL().BuscarAlmacen();
                for (int i = 0; i < list.Count; i++)
                    if (list[i].IdAlmacen == guiaSeleccionada.AlmOrigen.IdAlmacen)
                        almDesp = list[i];

                int k = new GuiaDeRemisionSQL().editarGuiaDeRemision(guiaSeleccionada);

                if (k != 0)
                    MessageBox.Show("Guia RECIBIA satisfactoriamente \nCódigo Guia = " + guiaSeleccionada.CodGuiaRem + "\nAlmacen despachador = " + almDesp.Nombre +
                                    "\nAlmacen receptor = " + guiaSeleccionada.Almacen.Nombre);
            }

            else
            {
                MessageBox.Show("Seleccione una GUIA que fue RECIBIDA satisfactoriamente");
            }
        }
    }
}
