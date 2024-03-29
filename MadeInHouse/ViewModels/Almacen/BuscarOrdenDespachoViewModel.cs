﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;
using MadeInHouse.DataObjects.Almacen;
using MadeInHouse.Models;
using MadeInHouse.Models.Almacen;
using System.Windows;
using MadeInHouse.ViewModels.Seguridad;
using MadeInHouse.Models.Seguridad;

using System.Diagnostics;
using System.ComponentModel.Composition;
using MadeInHouse.ViewModels.Layouts;

namespace MadeInHouse.ViewModels.Almacen
{
    [Export(typeof(BuscarOrdenDespachoViewModel))]
    class BuscarOrdenDespachoViewModel:Screen
    {
        #region constructores

        [ImportingConstructor]
        public BuscarOrdenDespachoViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
            ActualizarListaOrdenDespacho();
        }

        MantenerGuiaDeRemisionViewModel m;
        public BuscarOrdenDespachoViewModel(IWindowManager windowmanager, MantenerGuiaDeRemisionViewModel m)
            : this(windowmanager)
        {
            // TODO: Complete member initialization

            Util util = new Util();
            LstEstado = util.ListarEstadosOrdenDespacho();
            EstadoValue = 1;
            ActualizarListaOrdenDespacho();
            this.m = m;

        }

        private MantenerNotaDeSalidaViewModel mantenerNotaDeSalidaViewModel;

        public BuscarOrdenDespachoViewModel(IWindowManager windowmanager, MantenerNotaDeSalidaViewModel mantenerNotaDeSalidaViewModel)
            : this(windowmanager)
        {
            // TODO: Complete member initialization

            Util util = new Util();
            LstEstado = util.ListarEstadosOrdenDespacho();
            EstadoValue = 1;
            ActualizarListaOrdenDespacho();
            this.mantenerNotaDeSalidaViewModel = mantenerNotaDeSalidaViewModel;

        }

        #endregion

        OrdenDespachoSQL odSQL = new OrdenDespachoSQL();

        private readonly IWindowManager _windowManager;
        private List<OrdenDespacho> lstOrdenDespacho;
        public List<OrdenDespacho> LstOrdenDespacho
        {
            get { return lstOrdenDespacho; }
            set { lstOrdenDespacho = value; NotifyOfPropertyChange(() => LstOrdenDespacho); }
        }

        private string txtIdOrdenDespacho;
        public string TxtIdOrdenDespacho
        {
            get { return txtIdOrdenDespacho; }
            set { txtIdOrdenDespacho = value; NotifyOfPropertyChange(() => txtIdOrdenDespacho); }
        }

        private string txtIdVenta;
        public string TxtIdVenta
        {
            get { return txtIdVenta; }
            set { txtIdVenta = value; NotifyOfPropertyChange(() => txtIdVenta); }
        }

        private List<EstadoHabilitado> lstEstado;
        public List<EstadoHabilitado> LstEstado
        {
            get { return lstEstado; }
            set
            {
                if (this.lstEstado == value)
                {
                    return;
                }
                this.lstEstado = value;
                this.NotifyOfPropertyChange(() => this.lstEstado);
            }
        }
        private int estadoValue;
        public int EstadoValue
        {
            get { return estadoValue; }
            set { estadoValue = value; NotifyOfPropertyChange(() => EstadoValue); }
        }

        private OrdenDespacho ordenDespachoSeleccionado;

        public void SelectedItemChanged(object sender)
        {
            ordenDespachoSeleccionado = ((sender as DataGrid).SelectedItem as OrdenDespacho);
            if (ordenDespachoSeleccionado != null)
            {
                Trace.WriteLine("Orden Despacho Seleccionado: " + ordenDespachoSeleccionado.IdOrdenDespacho);
            }
        }

        public Boolean EstaEnOrden(OrdenDespacho ord)
        {
            List<GuiaRemision> list = new GuiaDeRemisionSQL().BuscarGuiaDeRemision(null, 0, null);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Orden != null)
                {
                    if (list[i].Orden.IdOrdenDespacho == ord.IdOrdenDespacho)
                        return true;
                }
            }

            return false;
        }

        public void EnviarAlmacenCentral()
        {
            ordenDespachoSeleccionado.AlmOrigen.IdAlmacen = 2;//ALMACEN CENTRAL: idAlmacen=2
            int k = odSQL.EditarOrdenDespacho(ordenDespachoSeleccionado);
            if (k == 1)
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Orden de despacho enviada a ALMACEN CENTRAL"));
        }

        public void BuscarOrdenDespacho()
        {
            Trace.WriteLine("Estado: "+EstadoValue);
            if ((!String.IsNullOrEmpty(TxtIdOrdenDespacho)) && (!String.IsNullOrEmpty(TxtIdVenta)))
            {
                lstOrdenDespacho = odSQL.BuscarOrdenDespacho(Int32.Parse(TxtIdOrdenDespacho), Int32.Parse(TxtIdVenta), EstadoValue);
                NotifyOfPropertyChange("LstOrdenDespacho");
            }
            else if((String.IsNullOrEmpty(TxtIdOrdenDespacho)) && (String.IsNullOrEmpty(TxtIdVenta)))
            {
                //MessageBox.Show("tam list = " + lstOrdenDespacho.Count);
                lstOrdenDespacho = odSQL.BuscarOrdenDespacho(-1, -1, EstadoValue);
                NotifyOfPropertyChange("LstOrdenDespacho");
            }
            else if(String.IsNullOrEmpty(TxtIdOrdenDespacho))
            {
                lstOrdenDespacho = odSQL.BuscarOrdenDespacho(-1, Int32.Parse(TxtIdVenta.ToString()), EstadoValue);
                NotifyOfPropertyChange("LstOrdenDespacho");
            }
            else
            {
                lstOrdenDespacho = odSQL.BuscarOrdenDespacho(Int32.Parse(TxtIdOrdenDespacho.ToString()), -1, EstadoValue);
                NotifyOfPropertyChange("LstOrdenDespacho");
            }
            
        }

        public void ActualizarListaOrdenDespacho()
        {
            BuscarOrdenDespacho();
        }

        public void AbrirEditarOrdenDespacho()
        {
            if (ordenDespachoSeleccionado != null)
                _windowManager.ShowWindow(new MantenerOrdenDespachoViewModel(_windowManager, ordenDespachoSeleccionado));
        }

        public void SeleccionarOrdenDespacho()
        {
            if (ordenDespachoSeleccionado != null)
            {
                ordenDespachoSeleccionado.CodOrden = "OD-" + (1000000 + ordenDespachoSeleccionado.IdOrdenDespacho).ToString();
                if (m != null)
                {
                    if (!(new GuiaDeRemisionSQL().BuscarIDOrden(ordenDespachoSeleccionado.IdOrdenDespacho)))
                    {
                        m.Orden = ordenDespachoSeleccionado;
                        TryClose();
                    }
                    else
                    {
                        _windowManager.ShowDialog(new AlertViewModel(_windowManager, "La ORDEN ya esta en una GUIA"));
                    }
                }

                if (mantenerNotaDeSalidaViewModel != null)
                {
                    mantenerNotaDeSalidaViewModel.TxtDoc = ordenDespachoSeleccionado.CodOrden;
                    mantenerNotaDeSalidaViewModel.TxtDocId = ordenDespachoSeleccionado.IdOrdenDespacho;
                    mantenerNotaDeSalidaViewModel.SelectedDespacho = ordenDespachoSeleccionado;
                    this.TryClose();
                }
            }
        }

        public void Guardar()
        {

            ////Chekear ordenes de despacho
            //for (int i = 0; i < LstOrdenDespacho.Count; i++)
            //{
            //    if (lstOrdenDespacho[i].Estado == 1)
            //    {
            //        lstOrdenDespacho[i].Estado = 0;
            //        odSQL.ActualizarOrdenDespacho(lstOrdenDespacho[i]);
            //        //1: Agregar, 2: Editar, 3: Eliminar, 4: Recuperar, 5: Desactivar
            //        DataObjects.Seguridad.LogSQL.RegistrarActividad("Mantenimiento Orden de Despacho", "" + lstOrdenDespacho[i].IdOrdenDespacho, 2);
            //    }
            //}

            ActualizarListaOrdenDespacho();
        }

        public void Limpiar()
        {
            TxtIdOrdenDespacho = "";
            TxtIdVenta = "";
        }


    }
}
