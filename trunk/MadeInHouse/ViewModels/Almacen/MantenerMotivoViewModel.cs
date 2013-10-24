﻿using System;
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

        private List<Motivo> lstMotivos;

        public List<Motivo> LstMotivos
        {
            get { return lstMotivos; }
            set { lstMotivos = value; NotifyOfPropertyChange(() => LstMotivos); }
        }

        public void AgregarMotivo()
        {
            int k;
            Motivo m = new Motivo();
            m.motivo = TxtMotivo;
            k = DataObjects.Almacen.MotivoSQL.AgregarMotivo(m);

            if (k == 0)
                MessageBox.Show("Ocurrio un error");
            else
                MessageBox.Show("Motivo: = " + txtMotivo + " registrado con éxtio");

            refrescar();
        }

        private void refrescar()
        {
            LstMotivos = DataObjects.Almacen.MotivoSQL.BuscarMotivos();
            NotifyOfPropertyChange("LstMotivos");
        }
    }
}