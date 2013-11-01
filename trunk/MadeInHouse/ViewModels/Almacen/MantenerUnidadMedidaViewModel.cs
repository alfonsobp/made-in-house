using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Models.Almacen;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;

namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerUnidadMedidaViewModel : PropertyChangedBase
    {
        private UnidadMedida unidadMedidaSeleccionada;

        public void SelectedItemChanged(object sender)
        {
            unidadMedidaSeleccionada = ((sender as DataGrid).SelectedItem as UnidadMedida);
        }

        private string txtNombre;

        public string TxtNombre
        {
            get { return txtNombre; }
            set { txtNombre = value; NotifyOfPropertyChange(() => TxtNombre); }
        }

        private List<UnidadMedida> lstUnidadesDeMedida=null;

        public List<UnidadMedida> LstUnidadesDeMedida
        {
            get { return lstUnidadesDeMedida; }
            set { lstUnidadesDeMedida = value; NotifyOfPropertyChange(() => LstUnidadesDeMedida); }
        }

        public void AgregarUnidadDeMedida()
        {
            int k;
            UnidadMedida u = new UnidadMedida();
            u.Nombre = TxtNombre;
            k = new DataObjects.Almacen.UnidadMedidaSQL().AgregarUnidadMedida(u);

            if (k == 0)
                MessageBox.Show("Ocurrio un error");
            else
                MessageBox.Show("Unidad: = " + txtNombre + " registrado con éxtio");
            refrescar();
        }

        private void refrescar()
        {
            LstUnidadesDeMedida = new DataObjects.Almacen.UnidadMedidaSQL().BuscarUnidadMedida();
          
        }
    }
}