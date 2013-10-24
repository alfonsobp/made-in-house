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
    class MantenerMotivoViewModel : Screen
    {
        private WindowManager win = new WindowManager();

        public void AbrirMantenerMotivo()
        {
            win.ShowWindow(new Almacen.MantenerMotivoViewModel());
        }

        private Motivo motivoSeleccionado;

        public void SelectedItemChanged(object sender)
        {
            motivoSeleccionado = ((sender as DataGrid).SelectedItem as Motivo);
        }

        private string txtMotivo;

        public string TxtMotivo
        {
            get { return txtMotivo; }
            set { txtMotivo = value; NotifyOfPropertyChange(() => TxtMotivo); }
        }

        private string lstMotivos;

        public string LstMotivos
        {
            get { return lstMotivos; }
            set { lstMotivos = value; NotifyOfPropertyChange(() => LstMotivos); }
        }

        public void AgregarMotivo()
        {
            int k;
            Motivo m = new Motivo();
            m.motivo = txtMotivo;
            k = DataObjects.Almacen.MotivoSQL.AgregarMotivo(m);

            if (k == 0)
                MessageBox.Show("Ocurrio un error");
            else
                MessageBox.Show("Motivo: = " + txtMotivo + " registrado con éxtio");

            NotifyOfPropertyChange("LstMotivos");
        }

    }
}
