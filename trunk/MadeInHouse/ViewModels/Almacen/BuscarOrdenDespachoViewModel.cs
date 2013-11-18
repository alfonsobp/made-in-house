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

namespace MadeInHouse.ViewModels.Almacen
{
    class BuscarOrdenDespachoViewModel:Screen
    {
        OrdenDespachoSQL odSQL = new OrdenDespachoSQL();
        public BuscarOrdenDespachoViewModel()
        {
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
        }


        public void BuscarOrdenDeDespacho()
        {
            lstOrdenDespacho = odSQL.BuscarOrdenDespacho(Int32.Parse(TxtIdOrdenDespacho), Int32.Parse(TxtIdVenta));
            NotifyOfPropertyChange("LstOrdenDespacho");
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
