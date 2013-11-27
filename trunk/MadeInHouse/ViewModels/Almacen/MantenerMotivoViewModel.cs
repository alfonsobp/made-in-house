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
using System.ComponentModel.Composition;
using MadeInHouse.ViewModels.Layouts;

namespace MadeInHouse.ViewModels.Almacen
{
    [Export(typeof(MantenerMotivoViewModel))]
    class MantenerMotivoViewModel : PropertyChangedBase, IDataErrorInfo
    {
        [ImportingConstructor]
        public MantenerMotivoViewModel(IWindowManager windowmanager)
        {
            _windowManager = windowmanager;
            LstMotivos = DataObjects.Almacen.MotivoSQL.BuscarMotivos();
        }
        private Motivo motivoSeleccionado;

        public void SelectedItemChanged(object sender)
        {
            motivoSeleccionado = ((sender as DataGrid).SelectedItem as Motivo);
        }

        private readonly IWindowManager _windowManager;

        private string txtMotivo=null;

        public string TxtMotivo
        {
            get { return txtMotivo; }
            set { txtMotivo = value; NotifyOfPropertyChange(() => TxtMotivo); }
        }

        private List<Motivo> lstMotivos=null;

        public List<Motivo> LstMotivos
        {
            get { return lstMotivos; }
            set { lstMotivos = value; NotifyOfPropertyChange(() => LstMotivos); }
        }

        public void AgregarMotivo()
        {
            int k;
            Motivo m = new Motivo();
            if ( string.IsNullOrWhiteSpace(TxtMotivo)) {
                MessageBox.Show("El nombre del motivo no puede ser vacio");
                return;
            }
            m.NombreMotivo = TxtMotivo;
            m.Nombre = TxtMotivo;
            k = DataObjects.Almacen.MotivoSQL.AgregarMotivo(m);
            if (k == 0)
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Ocurrio un error"));
            }
            else
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "Motivo: = " + txtMotivo + " registrado con éxtio"));
            }
            refrescar();
        }
        public void Borrar() {

            Motivo tz;
            tz = motivoSeleccionado;
            if (tz != null)
            {
                int a = DataObjects.Almacen.MotivoSQL.Eliminar(tz);
                if (a > 0) LstMotivos.Remove(tz);
                else
                {
                    _windowManager.ShowDialog(new AlertViewModel(_windowManager, "No se pudo borrar borrar el motivo porque esta siendo usado"));
                }
            }
            else
            {
                _windowManager.ShowDialog(new AlertViewModel(_windowManager, "No se ha seleccionado ninguna Motivo"));
            }
            refrescar();
        }
        private void refrescar()
        {
            LstMotivos = DataObjects.Almacen.MotivoSQL.BuscarMotivos();
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
                    case "TxtMotivo": if (string.IsNullOrEmpty(TxtMotivo)) result = "Esta vacia"; break;
                };
                return result;
            }
        }

    }
}
