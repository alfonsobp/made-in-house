using System;
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

namespace MadeInHouse.ViewModels.Almacen
{
    class BuscarOrdenDespachoViewModel:Screen
    {
        OrdenDespachoSQL odSQL = new OrdenDespachoSQL();
        public BuscarOrdenDespachoViewModel()
        {
            ActualizarListaOrdenDespacho();
        }

        MantenerGuiaDeRemisionViewModel m;
        public BuscarOrdenDespachoViewModel(MantenerGuiaDeRemisionViewModel m)
        {
            this.m = m;
            ActualizarListaOrdenDespacho();
        }
        
        private MyWindowManager win = new MyWindowManager();
        private List<OrdenDespacho> lstOrdenDespacho;
        public List<OrdenDespacho> LstOrdenDespacho
        {
            get { return lstOrdenDespacho; }
            set { lstOrdenDespacho = value; NotifyOfPropertyChange(() => LstOrdenDespacho); }
        }

        //textBox de Búsqueda con el código del usuario:
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


        private OrdenDespacho ordenDespachoSeleccionado;

        public void SelectedItemChanged(object sender)
        {
            ordenDespachoSeleccionado = ((sender as DataGrid).SelectedItem as OrdenDespacho);

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
                        MessageBox.Show("La ORDEN ya esta en una GUIA");
                    }
                }
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


        public void BuscarOrdenDeDespacho()
        {
            if ((!String.IsNullOrEmpty(TxtIdOrdenDespacho)) && (!String.IsNullOrEmpty(TxtIdVenta)))
            {
                lstOrdenDespacho = odSQL.BuscarOrdenDespacho(Int32.Parse(TxtIdOrdenDespacho), Int32.Parse(TxtIdVenta));
                NotifyOfPropertyChange("LstOrdenDespacho");
            }

            else
            {
                lstOrdenDespacho = odSQL.BuscarOrdenDespacho(-1, -1);
                MessageBox.Show("tam list = " + lstOrdenDespacho.Count);
                NotifyOfPropertyChange("LstOrdenDespacho");
            }
        }

        public void ActualizarListaOrdenDespacho()
        {
            BuscarOrdenDeDespacho();
        }

        public void AbrirEditarOrdenDespacho()
        {
            if (ordenDespachoSeleccionado != null)
                win.ShowWindow(new Almacen.MantenerOrdenDespachoViewModel(ordenDespachoSeleccionado));
        }

        public void Guardar()
        {

            //Chekear ordenes de despacho
            for (int i = 0; i < LstOrdenDespacho.Count; i++)
            {
                if (lstOrdenDespacho[i].Estado == 1)
                {
                    lstOrdenDespacho[i].Estado = 0;
                    odSQL.ActualizarOrdenDespacho(lstOrdenDespacho[i]);
                    //1: Agregar, 2: Editar, 3: Eliminar, 4: Recuperar, 5: Desactivar
                    DataObjects.Seguridad.LogSQL.RegistrarActividad("Mantenimiento Orden de Despacho", "" + lstOrdenDespacho[i].IdOrdenDespacho, 2);
                }
            }

            ActualizarListaOrdenDespacho();
        }

        public void Limpiar()
        {
            TxtIdOrdenDespacho = "";
            TxtIdVenta = "";
        }


    }
}
