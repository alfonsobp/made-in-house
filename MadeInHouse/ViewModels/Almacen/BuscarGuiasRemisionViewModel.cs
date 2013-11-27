using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models.Almacen;
using MadeInHouse.ViewModels.Layouts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace MadeInHouse.ViewModels.Almacen
{
    [Export(typeof(BuscarGuiasRemisionViewModel))]
    class BuscarGuiasRemisionViewModel : PropertyChangedBase
    {
        #region constructores

        [ImportingConstructor]
        public BuscarGuiasRemisionViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
            //ActualizarGuiaRemision();
        }

        public BuscarGuiasRemisionViewModel(IWindowManager windowmanager, MantenerNotaDeIngresoViewModel mantenerNotaDeIngresoViewModel, int p)
            : this(windowmanager)
        {
            this.mantenerNotaDeIngresoViewModel = mantenerNotaDeIngresoViewModel;
            this.Accion = p;
        }

        #endregion

        #region atributos

        private readonly IWindowManager _windowManager;

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

        #endregion

        public void BuscarAlmacen()
        {
            _windowManager.ShowWindow(new BuscarAlmacenViewModel(_windowManager, this));
        }

        public void SelectedItemChanged(object sender)
        {
            guiaSeleccionada = ((sender as DataGrid).SelectedItem as GuiaRemision);
        }

        public void Acciones(object sender)
        {
            if (accion == 1)
            {
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
            _windowManager.ShowWindow(new MantenerGuiaDeRemisionViewModel(_windowManager));
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

            if (Alm != null)
            {
                if (!String.IsNullOrEmpty(Alm.CodAlmacen))
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].NombOrigen.Equals(Alm.Nombre))
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
            else
                LstGuiaDeRemision = new List<GuiaRemision>(list);

        }

        public void EditarGuiaDeRemision()
        {
            _windowManager.ShowWindow(new MantenerGuiaDeRemisionViewModel(_windowManager, guiaSeleccionada));
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

                int k = new GuiaDeRemisionSQL().editarGuiaDeRemision(guiaSeleccionada);

                if (k != 0)
                {
                    if (guiaSeleccionada.Nota != null)
                    {
                        _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Guia RECIBIA satisfactoriamente \nCódigo Guia = " + guiaSeleccionada.CodGuiaRem + "\nAlmacen despachador = " + guiaSeleccionada.NombOrigen +
                                        "\nAlmacen receptor = " + guiaSeleccionada.Almacen.Nombre));
                    }
                    else
                        _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Guia RECIBIA satisfactoriamente \nCódigo Guia = " + guiaSeleccionada.CodGuiaRem + "\nTienda despachadora = " + guiaSeleccionada.NombOrigen +
                                        "\nDireccion de despacho = " + guiaSeleccionada.Destino));
                }

            }

            else
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Seleccione una GUIA que fue RECIBIDA satisfactoriamente"));
            }
        }
    }
}