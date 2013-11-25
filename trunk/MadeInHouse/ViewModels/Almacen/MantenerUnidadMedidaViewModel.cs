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
using System.ComponentModel;

namespace MadeInHouse.ViewModels.Almacen
{
    class MantenerUnidadMedidaViewModel : PropertyChangedBase,IDataErrorInfo
    {
        private UnidadMedida unidadMedidaSeleccionada=null;

        public void SelectedItemChanged(object sender)
        {
            unidadMedidaSeleccionada = ((sender as DataGrid).SelectedItem as UnidadMedida);
        }

        private string txtNombre=null;

        public string TxtNombre
        {
            get { return txtNombre; }
            set { txtNombre = value; NotifyOfPropertyChange(() => TxtNombre); }
        }

        private List<UnidadMedida> lstUnidadesDeMedida = new DataObjects.Almacen.UnidadMedidaSQL().BuscarUnidadMedida();

        public List<UnidadMedida> LstUnidadesDeMedida
        {
            get { return lstUnidadesDeMedida; }
            set { lstUnidadesDeMedida = value; NotifyOfPropertyChange(() => LstUnidadesDeMedida); }
        }

        public void AgregarUnidadDeMedida()
        {
            int k;
            UnidadMedida u = new UnidadMedida();
            if (string.IsNullOrWhiteSpace(TxtNombre)) {
                MessageBox.Show("La unidad de Medida no puede estar vacia");
                return;
            }
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
        public void Borrar()
        {

            UnidadMedida tz;
            tz = unidadMedidaSeleccionada;
            if (tz != null)
            {
                int a = new DataObjects.Almacen.UnidadMedidaSQL().Eliminar(tz);
                if (a > 0) LstUnidadesDeMedida.Remove(tz);
                else
                {
                    MessageBox.Show("No se pudo borrar borrar la unidad porque esta siendo usado");
                }
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ninguna unidad de medida");
            }
            refrescar();
        }

        public string Error
        {
            get
            {
                return "Error Test!!!!";
            }
        }

        public string this[string columName]
        {

            get
            {

                string result = string.Empty;
                switch (columName)
                {
                    case "TxtNombre": if (string.IsNullOrWhiteSpace(TxtNombre)) result = "Esta vacia"; break;
                };
                return result;
            }
        }

    }
}