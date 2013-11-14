using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MadeInHouse.Views.Compras;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using MadeInHouse.DataObjects.Compras;
using MadeInHouse.Models;
using System.Data.OleDb;
using System.Data;
using MadeInHouse.Models.Compras;

namespace MadeInHouse.ViewModels.Compras
{
    class ControlServiciosViewModel: PropertyChangedBase
    {
        //Constructores
        public ControlServiciosViewModel()
        {
            ActualizarServicios();
            lstOpciones = new List<string>();
            lstOpciones.Add("ATENDIDO");
            lstOpciones.Add("PENDIENTE");
            lstOpciones.Add("CANCELADO");
        }

        //Atributos
        private List<string> lstOpciones;
        public List<string> LstOpciones
        {
            get { return lstOpciones; }
            set { lstOpciones = value; NotifyOfPropertyChange(() => LstOpciones); }
        }


        private string selectedEst;
        public string SelectedEst
        {
            get { return selectedEst; }
            set { selectedEst = value; NotifyOfPropertyChange(() => SelectedEst); }
        }

        private DateTime txtFechaIni = DateTime.Now;
        public DateTime TxtFechaIni
        {
            get { return txtFechaIni; }
            set { txtFechaIni = value; NotifyOfPropertyChange(() => TxtFechaIni); }
        }

        private DateTime txtFechaFin = DateTime.Now;
        public DateTime TxtFechaFin
        {
            get { return txtFechaFin; }
            set { txtFechaFin = value; NotifyOfPropertyChange(() => TxtFechaFin); }
        }

        //Funciones
        public void ActualizarServicios()
        {

        }

        public void BuscarServicio()
        {

        }

        public void CumplirServicio()
        {

        }

    }
}
